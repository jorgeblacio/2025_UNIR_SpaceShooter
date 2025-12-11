using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpaceShip : MonoBehaviour
{
    [SerializeField] float maxSpeed = 100f;
    [SerializeField] float acceleration = 300f;
    [SerializeField] float deceleration = 200f; 
    [SerializeField] float friction = 0.9f; 

    [Header("Sprite Animation")]
    [SerializeField] private SpriteRenderer shipSpriteRenderer;
    [SerializeField] private Sprite[] shipSprites;
    [SerializeField] private float spriteChangeThreshold = 0.1f;

    [Header("Engine Particles")]
    [SerializeField] private ParticleSystem engineParticles;
    [SerializeField] private float forwardThreshold = 0.1f;

    [Header("Weapon System")]
    [SerializeField] private WeaponManager weaponManager;

    [Header("Input")]
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference shoot;

    private Vector2 rawMove;
    private Vector2 currentVelocity = Vector2.zero;
    private bool isShooting = false;

    private void OnEnable()
    {
        move.action.Enable();
        shoot.action.Enable();

        move.action.started += OnMove;
        move.action.performed += OnMove;
        move.action.canceled += OnMove;

        shoot.action.started += OnShoot;
        shoot.action.canceled += OnShootRelease;
    }

    void Update()
    {
        if (rawMove.magnitude > 0.01f)
        {
            currentVelocity += rawMove * acceleration * Time.deltaTime;
        }
        else
        {
            float currentSpeed = currentVelocity.magnitude;
            if (currentSpeed > 0)
            {
                float deceleratedSpeed = currentSpeed - deceleration * Time.deltaTime;
                deceleratedSpeed = Mathf.Max(deceleratedSpeed, 0);
                currentVelocity = currentVelocity.normalized * deceleratedSpeed;
            }
        }

        currentVelocity *= friction;

        float linearVelocity = currentVelocity.magnitude;
        linearVelocity = Mathf.Clamp(linearVelocity, 0, maxSpeed);
        currentVelocity = currentVelocity.normalized * linearVelocity;

        transform.Translate(currentVelocity * Time.deltaTime);

        UpdateShipSprite();
        UpdateEngineParticles();
        
        // Handle shooting
        if (isShooting && weaponManager != null)
        {
            weaponManager.Fire();
        }
    }

    private void UpdateShipSprite()
    {
        if (shipSpriteRenderer == null || shipSprites == null || shipSprites.Length < 3)
            return;

        if (currentVelocity.y < -spriteChangeThreshold)
        {
            shipSpriteRenderer.sprite = shipSprites[1];
        }
        else if (currentVelocity.y > spriteChangeThreshold)
        {
            shipSpriteRenderer.sprite = shipSprites[2];
        }
        else
        {
            shipSpriteRenderer.sprite = shipSprites[0];
        }
    }

    private void UpdateEngineParticles()
    {
        if (engineParticles == null)
            return;

        if (currentVelocity.x > forwardThreshold)
        {
            if (!engineParticles.isPlaying)
            {
                engineParticles.Play();
            }
        }
        else
        {
            if (engineParticles.isPlaying)
            {
                engineParticles.Stop();
            }
        }
    }

    private void OnDisable()
    {
        move.action.Disable();
        shoot.action.Disable();

        move.action.started -= OnMove;
        move.action.performed -= OnMove;
        move.action.canceled -= OnMove;

        shoot.action.started -= OnShoot;
        shoot.action.canceled -= OnShootRelease;
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        isShooting = true;
    }
    
    private void OnShootRelease(InputAction.CallbackContext context)
    {
        isShooting = false;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        rawMove = context.ReadValue<Vector2>();
    }
}