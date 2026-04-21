using UnityEngine;

public class EnemyTraceController : Health
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float attackDelay = 0.25f;
    [SerializeField] private float traceDistance = 5f;
    [SerializeField] private float PatrolRangeX = 3f;
    [SerializeField] private float PatrolRangeY = 3f;

    private float startPosX;
    private float startPosY;


    private Transform playerTransform;
    private Player player;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private Vector2 targetPosition;
    private bool isAttacking = false;
    private float attackTimer = 0f;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
            player = playerObj.GetComponent<Player>();
        }

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        currentHealth = maxHP;
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        SetRandomPosition();
    }

    private void Update()
    {
        if (playerTransform == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < traceDistance)
        {
            Vector2 direction = ((Vector2)playerTransform.position - (Vector2)transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed;
        }
        else
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
            HandleAttackLogic();
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
        if (rb.linearVelocity.x > 0) spriteRenderer.flipX = false;
        else if (rb.linearVelocity.x < 0) spriteRenderer.flipX = true;
    }

    private void HandleAttackLogic()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            if (player != null) player.TakeDamage(damage);
            attackTimer = attackDelay;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            attackTimer = 0f;
            isAttacking = true;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            SetRandomPosition();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttacking = false;
            attackTimer = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall")) SetRandomPosition();
    }
}