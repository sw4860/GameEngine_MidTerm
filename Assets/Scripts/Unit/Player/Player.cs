using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Health
{
    [SerializeField] private WeaponLogic weapon;

    [Header("Movement Settings")]
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] private int maxJumpCount = 2;
    [SerializeField] public float jumpPower = 5f;
    [SerializeField] private float jumpPressTime = 0.1f;
    [SerializeField] private float coyoteTime = 0.1f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float dashPower = 5f;
    [SerializeField] public float dashCooldown = 3f;
    [SerializeField] private float dashTime = 0.2f;
    
    [Header("Ground Check Settings")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Ghost Settings")]
    [SerializeField] private int ghostPoolSize = 7;
    [SerializeField] private float ghostSpawnRate = 0.03f;
    [SerializeField] private float ghostFadeTime = 0.3f;

    public bool isInvincible { get; set; }
    private Vector2 moveInput;

    private bool isGrounded;
    private bool isRealGrounded;
    private int _jumpCount;
    private bool isJumping;
    private float jumpTimer;
    private float coyoteTimer;
    private bool isDashing;
    private float dashTimer;
    public float lastDashTime { get; set; }
    private float lastAttackTime;

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
        lastAttackTime = 0;

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        currentHealth = maxHP;
        EventManager.OnPlayerHPChanged?.Invoke();

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
        if (!isGrounded)
        {
            coyoteTimer += Time.deltaTime;
            if (coyoteTimer >= coyoteTime)
            {
                isRealGrounded = false;
            }
        }
        else
        {
            coyoteTimer = 0f;
            isRealGrounded = true;
        }

        if (weapon != null && Mouse.current.leftButton.isPressed && Time.time >= lastAttackTime + weapon.attackCooldown && StaticValues.CanAttack)
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            weapon.Attack(transform, mouseWorldPos);
            lastAttackTime = Time.time;
        }

        if (Keyboard.current.leftCtrlKey.wasPressedThisFrame && StaticValues.CanGravityChange)
        {
            rb.gravityScale *= -1;
            transform.Rotate(180f, 0f, 0f);
        }

        UpdateSprite();
        UpdateGhosts();
        OnGrounded();
    }

    void FixedUpdate()
    {
        if (!isDashing)
            rb.linearVelocity = new Vector2(Mathf.Round(moveInput.x) * moveSpeed, rb.linearVelocity.y);
        else
            HandleDash();

        if (isJumping) 
            HandleJump();

        if (rb.linearVelocity.y * Mathf.Sign(rb.gravityScale) < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * rb.gravityScale * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    void OnEsc(InputValue value)
    {
        EventManager.OnEscPressed?.Invoke();
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
        if (value.isPressed && (isRealGrounded || _jumpCount < maxJumpCount))
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
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Lerp(jumpPower / 2, jumpPower, jumpTimer / jumpPressTime) * transform.up.y);
        else
            isJumping = false;
    }
    
    void OnDash(InputValue value)
    {
        if (value.isPressed && !isDashing && Time.time >= lastDashTime + dashCooldown)
        {
            isDashing = true;
            isJumping = false;
            lastDashTime = Time.time;
            ghostSpawnTimer = 0f;
        }
    }

    void HandleDash()
    {
        dashTimer += Time.deltaTime;

        float dashDirection = math.round(moveInput.x);
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
        ghost.transform.rotation = transform.rotation;
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

        float verticalVelocity = rb.linearVelocity.y * Mathf.Sign(rb.gravityScale);

        bool actuallyGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (actuallyGrounded)
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
            if (verticalVelocity > 0)
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

    public override void TakeDamage(float damage)
    {
        if (isInvincible) return;

        CreateDamageText(damage);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            base.Die();
            EventManager.OnPlayerDeath?.Invoke();
        }
        EventManager.OnPlayerHPChanged?.Invoke();
    }
    
    public List<float> GetHealth()
    {
        return new List<float> {currentHealth, maxHP};
    }
}