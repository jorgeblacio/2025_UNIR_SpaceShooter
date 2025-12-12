using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] private float lifetime = 1f;
    
    private ParticleSystem particles;
    
    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
        if (particles != null)
        {
            particles.Play();
        }
        
        Destroy(gameObject, lifetime);
    }
}