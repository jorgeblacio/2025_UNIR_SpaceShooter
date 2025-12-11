using UnityEngine;

public class SpreadShot : Weapon
{
    [SerializeField] private float projectileSpeed = 15f;
    [SerializeField] private int projectileCount = 3;
    [SerializeField] private float spreadAngle = 15f;
    
    protected override void ShootProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            float startAngle = -(spreadAngle * (projectileCount - 1)) / 2f;
            
            for (int i = 0; i < projectileCount; i++)
            {
                float angle = startAngle + (spreadAngle * i);
                Quaternion rotation = Quaternion.Euler(0, 0, angle);
                
                GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation * rotation);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 direction = rotation * Vector2.right;
                    rb.linearVelocity = direction * projectileSpeed;
                }
            }
        }
    }
}