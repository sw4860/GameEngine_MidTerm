using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Health
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int maxJumpCount = 2;
    [SerializeField] private float jumpPower = 5f;
    [SerializeField] private float jumpPressTime = 0.1f;
    [SerializeField] private float dashPower = 5f;
    [SerializeField] private float dashCooldown = 3f;
    [SerializeField] private float dashTime = 0.2f;
    
    [Header("Ground Check Settings")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Ghost Settings")]
    [SerializeField] private int ghostPoolSize = 7;
    [SerializeField] private float ghostSpawnRate = 0.03f;
    [SerializeField] private float ghostFadeTime = 0.3f;

    private Vector2 moveInput;

    private bool isGrounded;
    private int _jumpCount;
    private bool isJumping;
    private float jumpTimer;
    private bool isDashing;
    private float dashTimer;
    private float lastDashTime;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private SpriteRenderer[] ghostSprites;
    private float[] ghostAlpha;
    private float ghostSpawnTimer;
    private int currentGhostIndex;

    const string PLAYER_IDLE = "Player-Idle";
    const string PLAYER_RUN = "Player-Run";
    const string PLAYER_JUMP = "Player-Jump";
    const string PLAYER_FALL = "Player-Fall";

    private string currentState;

    void Awake()
    {
        lastDashTime = -dashCooldown;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        currentHealth = maxHP;
        EventManager.OnPlayerHPChanged();

        ghostSprites = new SpriteRenderer[ghostPoolSize];
        ghostAlpha = new float[ghostPoolSize];
        for (int i = 0; i < ghostPoolSize; i++)
        {
            GameObject ghostObj = new GameObject("DashGhost_" + i);
            ghostSprites[i] = ghostObj.AddComponent<SpriteRenderer>();
            ghostSprites[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {

        if (!isDashing)
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        else
            HandleDash();

        if (isJumping)
            HandleJump();

        UpdateSprite();
        UpdateGhosts();
        OnGrounded();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnGrounded()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer))
        {
            isGrounded = true;
            if (!isJumping) _jumpCount = 0;
        }
        else
        {
            isGrounded = false;
        }
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && _jumpCount < maxJumpCount)
        {
            isJumping = true;
            jumpTimer = 0f;
            ++_jumpCount;
        }
        else if (!value.isPressed)
            isJumping = false;
    }

    void HandleJump()
    {
        jumpTimer += Time.deltaTime;
        if (jumpTimer < jumpPressTime)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
        else
            isJumping = false;
    }
    
    void OnDash(InputValue value)
    {
        if (value.isPressed && !isDashing && Time.time >= lastDashTime + dashCooldown)
        {
            isDashing = true;
            lastDashTime = Time.time;
            ghostSpawnTimer = 0f;
        }
    }
    
    void HandleDash()
    {
        dashTimer += Time.deltaTime;

        float dashDirection = spriteRenderer.flipX ? -1 : 1;
        rb.linearVelocity = new Vector2(dashDirection * dashPower, 0);

        ghostSpawnTimer -= Time.deltaTime;
        if (ghostSpawnTimer <= 0f)
        {
            SpawnGhost();
            ghostSpawnTimer = ghostSpawnRate;
        }

        if (dashTimer >= dashTime)
        {
            isDashing = false;
            dashTimer = 0f;
        }
    }

    void UpdateGhosts()
    {
        for (int i = 0; i < ghostPoolSize; i++)
        {
            if (ghostSprites[i].gameObject.activeSelf)
            {
                ghostAlpha[i] -= Time.deltaTime / ghostFadeTime;
                if (ghostAlpha[i] <= 0f)
                {
                    ghostSprites[i].gameObject.SetActive(false);
                }
                else
                {
                    ghostSprites[i].color = new Color(1f, 1f, 1f, ghostAlpha[i]);
                }
            }
        }
    }

    void SpawnGhost()
    {
        SpriteRenderer ghost = ghostSprites[currentGhostIndex];
        
        ghost.sprite = spriteRenderer.sprite;
        ghost.transform.position = transform.position;
        ghost.flipX = spriteRenderer.flipX;
        ghost.color = new Color(1f, 1f, 1f, 1f);
        
        ghost.gameObject.SetActive(true);
        ghostAlpha[currentGhostIndex] = 1f;

        currentGhostIndex = (currentGhostIndex + 1) % ghostPoolSize;
    }

    void UpdateSprite()
    {
        if (moveInput.x > 0) spriteRenderer.flipX = false;
        else if (moveInput.x < 0) spriteRenderer.flipX = true;

        if (isGrounded)
        {
            if (moveInput.x != 0)
            {
                ChangeAnimation(PLAYER_RUN);
            }
            else
            {
                ChangeAnimation(PLAYER_IDLE);
            }
        }
        else
        {
            if (rb.linearVelocity.y > 0)
            {
                ChangeAnimation(PLAYER_JUMP);
            }
            else
            {
                ChangeAnimation(PLAYER_FALL);
            }
        }
    }

    void ChangeAnimation(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);
        currentState = newState;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        EventManager.OnPlayerHPChanged?.Invoke();
    }

    public List<int> GetHealth()
    {
        return new List<int> {currentHealth, maxHP};
    }
}
