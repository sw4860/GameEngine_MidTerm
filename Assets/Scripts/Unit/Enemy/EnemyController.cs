using UnityEngine;

public enum EnemyType
{
    Ground,
    Fly
}

public class EnemyController : Health
{
    [SerializeField] private EnemyType EnemyType = EnemyType.Ground;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float damage = 3f;
    [SerializeField] private float attackDelay = 0.25f;
    [SerializeField] private float PatrolRangeX = 3f;
    [SerializeField] private float PatrolRangeY = 3f;
    [SerializeField] private bool isReversed = false;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private Player player;
    private string currentState;
    private bool isMovingRight = true;
    private bool isAttacking = false;
    private float attackTimer = 0f;
    private float startPosX;
    private float startPosY;
    private Vector2 targetPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        currentHealth = maxHP;
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        SetRandomPosition();

        if (isReversed && EnemyType == EnemyType.Ground)
        {
            rb.gravityScale *= -1;
            transform.Rotate(180f, 0f, 0f);
        }
    }

    private void Update()
    {
        if (EnemyType == EnemyType.Ground)
        {
            if (transform.position.x > startPosX + PatrolRangeX)
                isMovingRight = false;
            else if (transform.position.x < startPosX - PatrolRangeX)
                isMovingRight = true;

            if (isMovingRight)
                rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
            else
                rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
        }
        else if (EnemyType == EnemyType.Fly)
        {
            if (Vector2.Distance(transform.position, targetPosition) < 0.2f)
            {
                SetRandomPosition();
            }

            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed;
        }

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
        base.TakeDamage(damage);
    }

    private void SetRandomPosition()
    {
        targetPosition = new Vector2(startPosX, startPosY) + new Vector2(Random.Range(-PatrolRangeX, PatrolRangeX), Random.Range(-PatrolRangeY, PatrolRangeY));
    }

    void UpdateSprite()
    {
        if (!isReversed)
        {
            if (rb.linearVelocity.x > 0) spriteRenderer.flipX = false;
            else if (rb.linearVelocity.x < 0) spriteRenderer.flipX = true;
        }
        else
        {
            if (rb.linearVelocity.x < 0) spriteRenderer.flipX = true;
            else if (rb.linearVelocity.x > 0) spriteRenderer.flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            if (EnemyType == EnemyType.Ground)
            {
                isMovingRight = !isMovingRight;
            }
            else if (EnemyType == EnemyType.Fly)
            {
                SetRandomPosition();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") && EnemyType == EnemyType.Fly)
            SetRandomPosition();
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