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

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        inGround = IsGrounded();
        if ((Input.GetButtonDown("Jump")) && jumpCount < jumpCountMax)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            jumpCount++;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        WallSlide();
        WallJump();

        if (!isWallJumping)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        if (!isWallJumping)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private bool IsGrounded()
    {
        bool isground = Physics2D.OverlapCircle(groundCheck.transform.position, 0.2f, groundLayer);
        if (isground)
        {
            jumpCount = 0;
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
    private void OnGUI()
    {
        string text = "isGrounded: " + IsGrounded() + "\n" +
                      "isWalled: " + IsWalled() + "\n" +
                      "isWallSliding: " + isWallSliding + "\n" +
                      "isWallJumping: " + isWallJumping + "\n" +
                      "wallJumpingCounter: " + wallJumpingCounter + "\n" +
                      "isFacingRight: " + isFacingRight + "\n" +
                      "horizontal: " + horizontal + "\n" +
                      "rb.velocity: " + rb.velocity + "\n"+
                      "Wall Count: " + jumpCount;
        // Draw in screen
        GUI.Label(new Rect(10, 10, 500, 500), text);
    }
}
