using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] private float lifetime = 1f;
    [SerializeField] private bool useParticles = true;
    [SerializeField] private bool useAnimation = false;
    
    private ParticleSystem particles;
    private Animator animator;
    
    private void Start()
    {
        if (useParticles)
        {
            particles = GetComponent<ParticleSystem>();
            if (particles != null)
            {
                particles.Play();
            }
        }
        
        if (useAnimation)
        {
            animator = GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("Explode");
            }
        }
        
        Destroy(gameObject, lifetime);
    }
}