using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpaceShip : MonoBehaviour
{
    public event Action OnPlayerDeath;

    [Header("Movement Settings")]
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
    public WeaponManager weaponManager;

    [Header("Death")]
    [SerializeField] private GameObject deathExplosion;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private float deathSoundVolume = 0.5f;
    
    private bool isDead = false;

    [HideInInspector] public InputActionReference move;
    [HideInInspector] public InputActionReference shoot;

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
        
        if (isShooting && weaponManager != null)
        {
            weaponManager.Fire();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }

    public void SetInputActions(InputActionReference moveAction, InputActionReference shootAction)
    {
        this.move = moveAction;
        this.shoot = shootAction;
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

    private void Die()
    {
        if (isDead) return;
        
        isDead = true;
        
        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position, deathSoundVolume);
        }
        
        if (deathExplosion != null)
        {
            Instantiate(deathExplosion, transform.position, Quaternion.identity);
        }
        
        if (engineParticles != null && engineParticles.isPlaying)
        {
            engineParticles.Stop();
        }
        
        OnPlayerDeath?.Invoke();
        
        if (shipSpriteRenderer != null)
        {
            shipSpriteRenderer.enabled = false;
        }
        
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
        }
        
        Destroy(gameObject, 0.5f);
    }
}