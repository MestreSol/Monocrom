using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Player
{
    private float horizontal;
    private float speed = 5f;
    private float jumpForce = 5f;

    private float wallSlidingSpeed = 0.75f;

    private float walljumpingDirection;
    private float wallJumpingTime = 0.5f;
    private float wallJumpingCounter;

    [SerializeField] private int jumpCountMax = 2;

    [SerializeField] private Vector2 wallJumpingPower = new Vector2(8f, 16f);

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private GameObject attackCheck;

    private List<string> comboSequence = new List<string>();
    private float comboTimer = 0f;
    private float comboMaxTime = 2f;
    private float maxCombo = 10f;
    private float attackCooldown = 0f;
    private float attackCooldownTime = 0.1f;
    private void Awake(){
        Weapon weapon = new Weapon("Sword", 10, 1, 1);
        equipedWeapon = weapon;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if(IsWalled())
        {
            WallSlide();
            WallJump();
        }
        else
        {
            isWallSliding = false;
            animator.SetBool("isWallSliding", isWallSliding);
        }

        UpdateComboTimer();
        UpdateAttackCooldown();
        ProcessAttackInput();
        

    }
    private void UpdateComboTimer()
    {
        if (comboSequence.Count > 0)
        {
            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0f)
            {
                ResetCombo();
            }
        }
    }
    private void UpdateAttackCooldown()
    {
        if (attackCooldown > 0f)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    private void ProcessAttackInput()
    {
        if (attackCooldown <= 0f)
        {
            CheckAndAddCombo(KeyCode.I, "I");
            CheckAndAddCombo(KeyCode.O, "O");
            CheckAndAddCombo(KeyCode.P, "P");
            CheckAndAddCombo(KeyCode.J, "J");
        }
    }
    private void CheckAndAddCombo(KeyCode key, string combo)
    {
        if (Input.GetKeyDown(key))
        {
            AddCombo(combo);
            attackCooldown = attackCooldownTime;

        }
    }
    
    public void AddCombo(string combo)
    {

        comboSequence.Add(combo);
        if (comboSequence.Count > maxCombo)
        {
            ResetCombo();
            return;
        }
        comboTimer = comboMaxTime;

        string lastCombo = comboSequence[comboSequence.Count - 1];
        string beforeCombo;
        try

        {
            beforeCombo = comboSequence[comboSequence.Count - 2];
        }
        catch (Exception e)
        {
            beforeCombo = "";
        }
        float damage = 0f;
        switch (lastCombo)
        {
            case "I":
                spriteRenderer.color -= new Color(0f, 0.1f, 0.1f, 0f);
                spriteRenderer.color += new Color(0.1f, 0f, 0f, 0f);
                animator.SetInteger("AttackType", 1);
                
                break;
            case "O":
                spriteRenderer.color -= new Color(0.1f, 0f, 0.1f, 0f);
                spriteRenderer.color += new Color(0f, 0.1f, 0f, 0f);
                animator.SetInteger("AttackType", 2);
                break;
            case "P":
                spriteRenderer.color -= new Color(0.1f, 0.1f, 0f, 0f);
                spriteRenderer.color += new Color(0f, 0f, 0.1f, 0f);
                animator.SetInteger("AttackType", 3);
                break;
            case "J":
                spriteRenderer.color -= new Color(0.1f, 0.1f, 0.1f, 0f);
                spriteRenderer.color += new Color(0.1f, 0.1f, 0.1f, 0f);
                animator.SetInteger("AttackType", 4);
                break;
        }

        damage = (Forca * (Sorte/equipedWeapon.CritChance)) + equipedWeapon.Damage+1;
        Debug.Log(damage);
        MakeDamage(damage);
        if (comboSequence.Count == 10)
        {
            ResetCombo();
            return;
        }
        animator.SetInteger("AttackSequence", comboSequence.Count);
        animator.SetTrigger("Attack");
        Debug.Log(lastCombo);
        Debug.Log(beforeCombo);
    }
    private void ResetCombo()
    {
        comboSequence.Clear();
        comboTimer = 0f;
        animator.SetInteger("AttackSequence", 0);
        animator.SetInteger("AttackType", 0);
        switch (CurColor)
        {
            case Colors.WHITE:
                spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
                break;
            case Colors.RED:
                spriteRenderer.color = new Color(1f, 0f, 0f, 1f);
                break;
            case Colors.BLUE:
                spriteRenderer.color = new Color(0f, 0f, 1f, 1f);
                break;
            case Colors.YELLOW:
                spriteRenderer.color = new Color(1f, 1f, 0f, 1f);
                break;
            case Colors.BLACK:
                spriteRenderer.color = new Color(0f, 0f, 0f, 1f);
                break;
        }

        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }
    private void Land()
    {
        jumpCount = 0;
        animator.SetBool("isJump", false);
        animator.SetBool("isFall", false);
        animator.SetBool("inGround", true);
    }
    private void Fall()
    {
        if (rb.velocity.y < 0f)
        {
            animator.SetBool("isFall", true);
            animator.SetBool("isJump", false);
        }
        else
        {
            animator.SetBool("isFall", false);
        }
    }
    private void Jump()
    {
        if (jumpCount < jumpCountMax)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            jumpCount++;
            animator.SetBool("isJump", true);
            animator.SetBool("isFall", false);
        }
    }
    private void Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        Flip();
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        animator.SetBool("isRun", horizontal != 0f);
    }
    private void FixedUpdate()
    {
        if (!isWallJumping)
        {
            Move();
        }
    }

    private bool IsGrounded()
    {
        bool isground = Physics2D.OverlapCircle(groundCheck.transform.position, 0.2f, groundLayer);
        if (isground)
        {
            Land();
        }
        return isground;
    }

    private bool IsWalled()
    {

        return Physics2D.OverlapCircle(wallCheck.transform.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
        animator.SetBool("isWallSliding", isWallSliding);

    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            walljumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(walljumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != walljumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
            animator.SetBool("isJump", true);
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private void MakeDamage(float damage){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.transform.position, 0.5f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Enemy>())
            {
                collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }
    private void OnGUI()
    {
        string text = "isGrounded: " + IsGrounded() + "\n" +
                      "isWalled: " + IsWalled() + "\n" +
                      "isWallSliding: " + isWallSliding + "\n" +
                      "isWallJumping: " + isWallJumping + "\n" +
                      "wallJumpingCounter: " + wallJumpingCounter + "\n" +
                      "isFacingRight: " + isFacingRight + "\n" +
                      "horizontal: " + horizontal + "\n" +
                      "rb.velocity: " + rb.velocity + "\n" +
                      "Wall Count: " + jumpCount;
        // Draw in screen
        GUI.Label(new Rect(10, 10, 500, 500), text);
    }
}
