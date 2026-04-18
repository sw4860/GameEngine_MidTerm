using UnityEngine;

public class EnemyController : Health
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float damage = 3f;
    [SerializeField] private float attackDelay = 0.25f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private Player player;
    private string currentState;
    private bool isMovingRight = true;
    private bool isAttacking = false;
    private float attackTimer = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        currentHealth = maxHP;
    }

    private void Update()
    {
        if (isMovingRight)
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);

        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                player.TakeDamage(damage);
                attackTimer = attackDelay;
            }
        }
        
        UpdateSprite();
    }

    public override void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateSprite()
    {
        if (rb.linearVelocity.x > 0) spriteRenderer.flipX = false;
        else if (rb.linearVelocity.x < 0) spriteRenderer.flipX = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            isMovingRight = !isMovingRight;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        attackTimer = 0f;
        isAttacking = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isAttacking = false;
        attackTimer = 0f;
    }
}