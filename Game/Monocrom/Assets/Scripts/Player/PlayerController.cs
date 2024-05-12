using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    // Controle das informações do jogador no mapa 2D
    public float horizontalSpeed;
    public float verticalSpeed;
    public float jumpForce;
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
    public bool isAttacking;
    public Transform attackPosition;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public float attackRate;
    public float attackRateCounter;
    public float attackDamage;
    public float knockbackForce;
    public float knockbackDuration;
    public float invincibilityDuration;
    public float invincibilityCounter;
    public bool isInvincible;
    public SpriteRenderer playerSprite;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveInput;
    private bool facingRight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
        attackRateCounter = attackRate;
    }

    private void Update()
    {
        if(GameManager.instance.CanProced()){
            GroundCheck();
            ProcessMove();
            if(Input.GetButtonDown("Jump"))
            {
                ProcessJump();
            }
            
        }
        
    }
    public void ProcessMove()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.velocity = new Vector2(moveInput.x * horizontalSpeed, rb.velocity.y);
        if(moveInput.x > 0 && !facingRight)
        {
            Flip();
        }
        else if(moveInput.x < 0 && facingRight)
        {
            Flip();
        }
        if(rb.velocity.x != 0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        anim.SetBool("isRunning", isRunning);
    }
    public void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    public void ProcessJump()
    {
        if(isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
        }
        if(isJumping && Input.GetButton("Jump"))
        {
            if(jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if(Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

    }
    public void GroundCheck()
    {
        // Verifica se o jogador esta colidindo com o chão
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }
    

        
}
