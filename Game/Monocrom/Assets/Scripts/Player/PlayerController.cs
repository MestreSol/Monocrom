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
        if(GameManager.instance.gameState == GameState.InGame)
        {
            moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

            if (isGrounded)
            {
                canDoubleJump = true;
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;
                rb.velocity = Vector2.up * jumpForce;
            }

            if (Input.GetButton("Jump") && isJumping)
            {
                if (jumpTimeCounter > 0)
                {
                    rb.velocity = Vector2.up * jumpForce;
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }

            if (Input.GetButtonUp("Jump"))
            {
                isJumping = false;
            }

            if (Input.GetButtonDown("Jump") && canDoubleJump && !isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce;
                canDoubleJump = false;
            }

            if (Input.GetButtonDown("Fire1") && !isAttacking)
            {
                isAttacking = true;
                attackRateCounter = attackRate;
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Entity>().TakeDamage(attackDamage);
                    if (enemiesToDamage[i].transform.position.x > transform.position.x)
                    {
                        enemiesToDamage[i].GetComponent<Rigidbody2D>().AddForce(Vector2.right * knockbackForce);
                    }
                    else
                    {
                        enemiesToDamage[i].GetComponent<Rigidbody2D>().AddForce(Vector2.left * knockbackForce);
                    }
                }
            }
        }    
    }
    

        
}
