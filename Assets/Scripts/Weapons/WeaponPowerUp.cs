using UnityEngine;

public class WeaponPowerUp : MonoBehaviour
{
    private const string PlayerTag = "Player";
    
    [Header("Audio")]
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private float volume = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PlayerTag))
        {
            PlayerSpaceShip player = other.GetComponent<PlayerSpaceShip>();

            if (player != null && GameManager.Instance != null)
            {
                GameManager.Instance.HandleWeaponPowerUpPickup();
                
                if (pickupSound != null)
                {
                    AudioSource.PlayClipAtPoint(pickupSound, transform.position, volume);
                }
                
                Destroy(gameObject);
            }
        }
    }
}