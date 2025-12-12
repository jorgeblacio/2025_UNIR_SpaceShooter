using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private bool destroyOnHit = true;
    
    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            
            if (destroyOnHit)
            {
                Destroy(gameObject);
            }
        }
    }
    
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}