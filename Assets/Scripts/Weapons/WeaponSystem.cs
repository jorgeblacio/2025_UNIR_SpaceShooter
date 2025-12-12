using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float fireRate = 0.2f;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected Transform firePoint;

    [Header("Audio")]
    [SerializeField] protected AudioClip shootSound;
    [SerializeField] protected float shootVolume = 0.3f;
    
    protected float nextFireTime = 0f;
    protected AudioSource audioSource;

    protected virtual void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0;
    }
    
    public virtual void Fire()
    {
        if (Time.time >= nextFireTime)
        {
            ShootProjectile();
            PlayShootSound();
            nextFireTime = Time.time + fireRate;
        }
    }

    protected virtual void PlayShootSound()
    {
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound, shootVolume);
        }
    }
    
    protected abstract void ShootProjectile();
    
    public virtual void PowerUp()
    {
        
    }
}