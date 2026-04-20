using UnityEngine;

public class FallingTrap : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    
    private Rigidbody2D rb;
    private Animator animator;
    
    private bool isTriggered = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        rb.gravityScale = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTriggered) return;

        if (collision.CompareTag("Player"))
        {
            isTriggered = true;
            rb.gravityScale = 1;
            animator.SetTrigger("Triggerd");
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
            player.TakeDamage(damage);
        else if (collision.gameObject.TryGetComponent(out Health health))
            health.TakeDamage(damage);
        
        animator.SetTrigger("Boom");
        Destroy(gameObject, 0.5f);
    }
}
