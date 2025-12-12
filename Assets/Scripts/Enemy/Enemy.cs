using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected int maxHealth = 3;
    [SerializeField] protected int scoreValue = 100;
    
    [Header("Audio")]
    [SerializeField] protected AudioClip hitSound;
    [SerializeField] protected AudioClip deathSound;
    [SerializeField] protected float audioVolume = 0.5f;
    
    [Header("Visual Feedback")]
    [SerializeField] protected GameObject explosionEffect;
    [SerializeField] protected float hitFlashDuration = 0.1f;
    
    protected int currentHealth;
    protected SpriteRenderer spriteRenderer;
    protected Color originalColor;
    protected AudioSource audioSource;
    
    protected virtual void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0; 
    }
    
    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound, audioVolume);
        }
        
        if (currentHealth > 0)
        {
            StartCoroutine(FlashRed());
        }
        else
        {
            Die();
        }
    }
    
    protected virtual void Die()
    {
        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position, audioVolume);
        }
        
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
        
        Destroy(gameObject);
    }
    
    protected System.Collections.IEnumerator FlashRed()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(hitFlashDuration);
            spriteRenderer.color = originalColor;
        }
    }
}