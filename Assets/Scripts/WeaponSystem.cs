using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float fireRate = 0.2f;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected Transform firePoint;
    
    protected float nextFireTime = 0f;
    
    public virtual void Fire()
    {
        if (Time.time >= nextFireTime)
        {
            ShootProjectile();
            nextFireTime = Time.time + fireRate;
        }
    }
    
    protected abstract void ShootProjectile();
    
    public virtual void PowerUp()
    {
        
    }
}