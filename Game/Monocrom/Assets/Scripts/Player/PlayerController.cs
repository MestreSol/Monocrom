using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//OBS: Necessario veriricar Pary antes de aplicar o dano
public class PlayerController : Player
{
    [SerializeField] private int maxComboSequence = 10;
    [SerializeField] private int maxJumpSequence = 2;
    [SerializeField] private float wallSlidingSpeed = 0.75f;
    [SerializeField] private Vector2 wallJumpForce = new Vector2(8f, 16f);
    [SerializeField] private float wallJumpingCooldown = 0.5f;
    [SerializeField] private float attackCooldown = 0.1f;
    [SerializeField] private float dashCooldown = 0.5f;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float climbSpeed = 3f;
    [SerializeField] private float comboCooldown = 1f;
    [SerializeField] private TMP_Text message;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private GameObject attackCheck;
    [SerializeField, Range(0f, 10f)] private float ledgeClimbXOffset1 = 0.2f;
    [SerializeField, Range(0f, 10f)] private float ledgeClimbYOffset1 = 0.2f;
    [SerializeField, Range(0f, 10f)] private float ledgeClimbXOffset2 = 0.2f;
    [SerializeField, Range(0f, 10f)] private float ledgeClimbYOffset2 = 0.2f;


    private SpriteRenderer spriteRender;
    private bool isClimbing;
    private bool canClimbLedge;
    private bool isDashing;
    private bool cooldownDash;
    private bool ledgeDetected;
    private float attackSpeedDirt;
    private float walljumpingDirection;
    private float wallJumpingCounter;
    private float horizontalForce;
    private float comboTimer;
    private float attackTimer = 0f;
    private List<string> comboSequence = new List<string>();
    private Vector2 ledgePosBot;
    private Vector2 ledgePos1, ledgePos2;


    private void CheckLedgeClimb()
    {
        if (ledgeDetected && !canClimbLedge)
        {
            canClimbLedge = true;
            ledgePosBot = wallCheck.position;
        }

        if (canClimbLedge && !isClimbing)
        {
            StartCoroutine(LedgeClimbRoutine());
        }
    }

    private IEnumerator LedgeClimbRoutine()
    {
        isClimbing = true;
        canClimbLedge = false;
        ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x - ledgeClimbXOffset1), Mathf.Floor(ledgePosBot.y + ledgeClimbYOffset1));
        ledgePos2 = new Vector2(Mathf.Floor(ledgePosBot.x - ledgeClimbXOffset2), Mathf.Floor(ledgePosBot.y + ledgeClimbYOffset2));

        while (Vector2.Distance(transform.position, ledgePos1) > 0.1f)
        {
            Vector2 newPos = Vector2.MoveTowards(transform.position, ledgePos1, climbSpeed * Time.deltaTime);
            rigidbody.MovePosition(newPos);
            yield return null;
        }

        while (Vector2.Distance(transform.position, ledgePos2) > 0.1f)
        {
            Vector2 newPos = Vector2.MoveTowards(transform.position, ledgePos2, climbSpeed * Time.deltaTime);
            rigidbody.MovePosition(newPos);
            yield return null;
        }

        isClimbing = false;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        EntityStatus = new EntityStatus();
        jumpCount = maxJumpSequence;
        attackSpeedDirt = attackCooldown;
    }
    private void HanddlerMoviment()
    {

        if (isDashing)
        {
            return;
        }
        horizontalForce = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("IsWallking"))
        {
            dirSpeed = speed;
            speed /= 2;
        }
        else if (Input.GetButtonUp("IsWallking"))
        {
            speed = dirSpeed;
        }
        if (horizontalForce > 0)
        {
            MoveRight(speed);
        }
        else if (horizontalForce < 0f)
        {
            MoveLeft(speed);
        }
        else
        {
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        }
    }
    private void HanddlerJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded())
            {
                Jump();
            }
            else
            {
                if (isWallSliding)
                {
                    WallJump();
                }
                else
                {
                    if (jumpCount < maxJumpSequence)
                    {
                        Jump();
                    }
                }
            }
        }
    }
    private void HanddlerWallSlide()
    {
        isWallSliding = false;
        if (isTouchingWall() && !isGrounded() && rigidbody.velocity.y < 0)
        {
            isWallSliding = true;
            if (rigidbody.velocity.y < -wallSlidingSpeed)
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, -wallSlidingSpeed);
            }
        }
    }
    private void HanddlerWallJump()
    {
        if (isWallJumping)
        {
            wallJumpingCounter += Time.deltaTime;
            if (wallJumpingCounter > wallJumpingCooldown)
            {
                isWallJumping = false;
                wallJumpingCounter = 0;
            }
        }
    }
    private void HanddlerAttack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (attackTimer <= 0)
            {
                if (comboSequence.Count < maxComboSequence)
                {
                    comboSequence.Add("Attack");
                    attackTimer = attackCooldown;
                }
                else
                {
                    comboSequence.Clear();
                    comboSequence.Add("Attack");
                    attackTimer = attackCooldown;
                }
            }
        }
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }
    private void HanddlerDash()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (cooldownDash)
            {
                return;
            }
            if (isGrounded())
            {
                StartCoroutine(Dash(0.1f, dashSpeed));
            }

        }
    }
    private IEnumerator Dash(float dashDuration, float dashSpeed)
    {
        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            if (horizontalForce != 0)
            {
                rigidbody.velocity = new Vector2(dashSpeed * horizontalForce, rigidbody.velocity.y);
            }
            else
            {
                rigidbody.velocity = new Vector2(dashSpeed * (spriteRender.flipX ? -1 : 1), rigidbody.velocity.y);
            }

            yield return null; // espera até o próximo frame
        }

        cooldownDash = true;
        yield return new WaitForSeconds(dashCooldown);
        cooldownDash = false;
    }
    public void ShowMessage(string message)
    {
        this.message.text = message;
        this.message.gameObject.SetActive(true);
    }
    public void HideMessage()
    {
        this.message.gameObject.SetActive(false);
    }
    private void HanddlerCombo()
    {
        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
        }
        else
        {
            comboSequence.Clear();
        }
    }
    private void Update()
    {
        HanddlerMoviment();
        HanddlerJump();
        HanddlerWallSlide();
        HanddlerWallJump();
        HanddlerAttack();
        HanddlerDash();
        CheckLedgeClimb();
        HanddlerCombo();
    }

}
