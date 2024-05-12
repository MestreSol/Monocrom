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
    public Transform WallCheck;
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
        jumpTimeCounter = jumpTime;
        attackRateCounter = attackRate;
        invincibilityCounter = invincibilityDuration;
        wallJumpTimeCounter = wallJumpTime;
        
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
    public void ProcessWallJump(){
        isTouchingWall = Physics2D.OverlapCircle(WallCheck.position, wallCheckDistance, whatIsWall);
        if(isTouchingWall && !isGrounded && moveInput.x != 0)
        {
            isWallJumping = true;
            canWallJump = true;
        }
        if(isWallJumping && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, wallJumpForce);
            wallJumpTimeCounter = wallJumpTime;
            isWallJumping = false;
        }
        if(wallJumpTimeCounter > 0 && canWallJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, wallJumpForce);
            wallJumpTimeCounter -= Time.deltaTime;
        }
        if(wallJumpTimeCounter <= 0)
        {
            canWallJump = false;
        }
    }
    public void ProcessJump()
    {
        if(isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
            canDoubleJump = true; // Enable double jump when grounded
        }
        else if(canDoubleJump && !isJumping) // Check for double jump only if not already jumping
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
            canDoubleJump = false; // Disable double jump after using it
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
        
        if(Input.GetKeyDown(KeyCode.RightControl))
        {
            ChangeMovement();
        }
    }
    
    public void ChangeMovement()
    {
        if(horizontalSpeed == walkSpeed)
        {
            horizontalSpeed = runSpeed;
        }
        else
        {
            horizontalSpeed = walkSpeed;
        }
    }
    public void GroundCheck()
    {
        // Verifica se o jogador esta colidindo com o chão
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(WallCheck.position, new Vector3(WallCheck.position.x + wallCheckDistance, WallCheck.position.y, WallCheck.position.z));

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
    // Execução apenas de DEBUG
    #if UNITY_EDITOR
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "isGrounded: " + isGrounded);
        GUI.Label(new Rect(10, 30, 100, 20), "isJumping: " + isJumping);
        GUI.Label(new Rect(10, 50, 100, 20), "canDoubleJump: " + canDoubleJump);
        GUI.Label(new Rect(10, 70, 100, 20), "isRunning: " + isRunning);
        GUI.Label(new Rect(10, 90, 100, 20), "isAttacking: " + isAttacking);
        GUI.Label(new Rect(10, 110, 100, 20), "isTouchingWall: " + isTouchingWall.ToString());
    }
    #endif
        
}
