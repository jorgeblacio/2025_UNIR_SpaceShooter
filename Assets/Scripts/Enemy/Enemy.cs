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
        
        // Setup audio source
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0; // 2D sound
    }
    
    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        // Play hit sound
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound, audioVolume);
        }
        
        // Visual feedback
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
        // Play death sound
        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position, audioVolume);
        }
        
        // Spawn explosion effect
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
        
        // Add score (you can implement a score manager later)
        // ScoreManager.Instance.AddScore(scoreValue);
        
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