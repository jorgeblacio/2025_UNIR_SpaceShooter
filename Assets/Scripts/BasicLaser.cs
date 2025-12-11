using UnityEngine;

public class BasicLaser : Weapon
{
    [SerializeField] private float projectileSpeed = 15f;
    
    protected override void ShootProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.right * projectileSpeed; // Shoots right
            }
        }
    }
}
