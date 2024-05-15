using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : Entity
{
    [Header("Moviment")]
    public float horizontal;
    public float speed = 8f;
    public float jumpingPower = 16f;
    public bool isFacingRight = true;
    public bool isDoubleJumping;

    [Header("Wall Jump")]
    public bool isWallSliding;
    public float wallSlidingSpeed = 2f;
    public bool isWallJumping;
    public float wallJumpingDirection;
    public float wallJumpingTime = 0.2f;
    public float wallJumpingCounter;
    public float wallJumpingDuration = 0.4f;
    public Vector2 wallJumpingPower = new Vector2(8f, 16f);
    [Header("Crouch")]
    public bool isCrouch;

    [Header("Dash")]
    public bool isDashing = false;
    public bool isDashRecharging = false;
    public float dashDuration = 0.2f;
    public float dashRechargeTime = 1f;
    public float dashSpeed = 20f;

    [Header("Components")]
    public Animator anim;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public Transform groundCheck;
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] public Transform wallCheck;
    [SerializeField] public LayerMask wallLayer;
    
    private void Update()
    {
        if(GameManager.instance.gameState == GameState.Pause) return;

        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            isDoubleJumping = true;
        }
        else if (Input.GetButtonDown("Jump") && isDoubleJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            isDoubleJumping = false;
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
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouch = !isCrouch;
        }
        if (!isDashing && Input.GetKeyDown(KeyCode.LeftShift) && !isDashRecharging && IsGrounded())
        {
            if (Mathf.Abs(horizontal) > 0)
            {
                StartCoroutine(Slider(0.1f, speed));
            }
            else
            {
                StartCoroutine(Dash(0.1f, dashSpeed));
            }
        }
        PlayerAnimationUpdate();
    }
    private IEnumerator Slider(float slideDuration, float slideSpeed)
    {
        float startTime = Time.time;
        anim.SetTrigger("Slider");
        while (Time.time < startTime + slideDuration)
        {
            rb.velocity = new Vector2(slideSpeed * (isFacingRight ? 1 : -1), rb.velocity.y);
            Debug.Log($"Time.time: {Time.time}, startTime: {startTime}, dashDuration: {dashDuration}, isFacingRight: {isFacingRight}");
            yield return null;
        }
        isDashing = false;
        isDashRecharging = true;
        yield return new WaitForSeconds(dashRechargeTime);
        isDashRecharging = false;
    }
    private IEnumerator Dash(float dashDuration, float dashSpeed)
    {
        float startTime = Time.time;
        anim.SetTrigger("Dash");
        while (Time.time < startTime + dashDuration)
        {
            isDashing = true;
            rb.velocity = new Vector2(dashSpeed * (isFacingRight ? 1 : -1), rb.velocity.y);
            Debug.Log($"Time.time: {Time.time}, startTime: {startTime}, dashDuration: {dashDuration}, isFacingRight: {isFacingRight}");
            yield return null; // espera até o próximo frame
        }
        isDashing = false;
        isDashRecharging = true;
        yield return new WaitForSeconds(dashRechargeTime);
        isDashRecharging = false;
    }
    private void PlayerAnimationUpdate()
    {
        // arredonda o valor de horizontal para 1 ou -1
        anim.SetFloat("Speed", Mathf.Abs(horizontal) == 0 ? -1 : 1);
        anim.SetBool("isGrounded", IsGrounded());
        anim.SetBool("isWallSliding", isWallSliding);
        anim.SetBool("isWallJumping", isWallJumping);
        anim.SetBool("isCrouch", isCrouch);
        anim.SetBool("isWalled", IsWalled());
    }
    private void FixedUpdate()
{
  // Se tiver no walljump ou dando dash retorna e não muda a velocity
  if(isWallJumping || isDashing)
    return;

  rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
}

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && rb.velocity.y < 0f)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlidingSpeed);
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
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
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
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
    // Execução apenas de DEBUG
#if UNITY_EDITOR
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 20), "isWallSliding: " + isWallSliding);
        GUI.Label(new Rect(10, 30, 200, 20), "isWallJumping: " + isWallJumping);
        GUI.Label(new Rect(10, 50, 200, 20), "IsCrouch: " + isCrouch);
    }

#endif
    private void DrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(wallCheck.position, 0.2f);
    }
}
