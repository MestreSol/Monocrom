using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    public float horizontalSpeed;
    public float verticalSpeed;
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float groundCheckRadius;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    public float jumpTime;
    public float jumpTimeCounter;
    public bool isJumping;
    public bool canDoubleJump;
    public bool isRunning;

    [Header("Attack")]
    public bool isAttacking;
    public Transform attackPosition;
    public float attackRange;
    public LayerMask whatIsPlayer;
    public float attackRate;
    public float attackRateCounter;
    public float attackDamage;
    public float knockbackForce;
    public float knockbackDuration;

    [Header("Invincibility")]
    public float invincibilityDuration;
    public float invincibilityCounter;
    public bool isInvincible;
    public SpriteRenderer enemySprite;

    [Header("Wall Jump")]
    public Transform wallCheck;
    public bool isTouchingWall;
    public float wallCheckDistance;
    public float wallSlideSpeed;
    public float wallJumpForce;
    public float wallJumpTime;
    public float wallJumpTimeCounter;
    public bool canWallJump;
    public bool isWallJumping;
    public LayerMask whatIsWall;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveInput;
    private bool facingRight = true;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, whatIsWall) || Physics2D.Raycast(wallCheck.position, Vector2.left, wallCheckDistance, whatIsWall);
        moveInput = new Vector2(horizontalSpeed, verticalSpeed);
        if (isGrounded)
        {
            canDoubleJump = true;
            isWallJumping = false;
        }
        if (isTouchingWall && !isGrounded && rb.velocity.y < 0)
        {
            isWallJumping = true;
        }
    }
    public void TakeDamage(float damage)
    {
        if (!isInvincible)
        {
            isInvincible = true;
            invincibilityCounter = invincibilityDuration;
            enemySprite.color = new Color(1f, 1f, 1f, 0.5f);
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * knockbackForce, ForceMode2D.Impulse);
            rb.AddForce(Vector2.right * knockbackForce, ForceMode2D.Impulse);
        }
    }
    public void Knockback(float knockbackDuration)
    {
        if (knockbackDuration <= 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
}
