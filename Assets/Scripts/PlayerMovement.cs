using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpPower = 5f;
    [SerializeField] private float dashPower = 5f;
    [SerializeField] private float dashCooldown = 3f;
    [SerializeField] private float dashTime = 0.2f;
    
    [Header("Ground Check Settings")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Sprite Settings")]
    [SerializeField] Sprite jumpingSprite;
    [SerializeField] Sprite fallingSprite;
    
    [Header("Ghost Settings")]
    [SerializeField] private int ghostPoolSize = 7; // 미리 만들어둘 고스트 개수
    [SerializeField] private float ghostSpawnRate = 0.03f; // 고스트 생성 간격
    [SerializeField] private float ghostFadeTime = 0.3f; // 투명해지는 데 걸리는 시간


    private Vector2 moveInput;

    private bool isGrounded;
    private bool isJumping;
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

    void Awake()
    {
        lastDashTime = -dashCooldown;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

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
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isJumping = !isGrounded;

        if (!isDashing)
        {
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            HandleDash();
        }

        UpdateSprite();
        UpdateGhosts();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
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

    void OnDash(InputValue value)
    {
        if (value.isPressed && !isDashing && Time.time >= lastDashTime + dashCooldown)
        {
            isDashing = true;
            lastDashTime = Time.time;
            ghostSpawnTimer = 0f;
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
            animator.enabled = true;
            animator.SetBool("isMoving", moveInput.x != 0);
        }
        else
        {
            animator.enabled = false;
            
            if (rb.linearVelocity.y > 0)
                spriteRenderer.sprite = jumpingSprite;
            else
                spriteRenderer.sprite = fallingSprite;
        }
    }

}
