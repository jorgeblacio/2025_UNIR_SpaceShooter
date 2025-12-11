using UnityEngine;

public class DoubleLaser : Weapon
{
    [SerializeField] private float projectileSpeed = 15f;
    [SerializeField] private float spreadDistance = 0.3f;
    
    protected override void ShootProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            Vector3 topPos = firePoint.position + Vector3.up * spreadDistance;
            GameObject topProjectile = Instantiate(projectilePrefab, topPos, firePoint.rotation);
            Rigidbody2D topRb = topProjectile.GetComponent<Rigidbody2D>();
            if (topRb != null)
            {
                topRb.linearVelocity = Vector2.right * projectileSpeed;
            }
            
            Vector3 bottomPos = firePoint.position + Vector3.down * spreadDistance;
            GameObject bottomProjectile = Instantiate(projectilePrefab, bottomPos, firePoint.rotation);
            Rigidbody2D bottomRb = bottomProjectile.GetComponent<Rigidbody2D>();
            if (bottomRb != null)
            {
                bottomRb.linearVelocity = Vector2.right * projectileSpeed;
            }
        }
    }
}