using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//OBS: Necessario veriricar Pary antes de aplicar o dano
public class PlayerController : Player
{
    // Variáveis privadas para armazenar os valores de movimento do jogador
    private float _horizontal;
    private float _speed = 5f;
    private float _jumpForce = 5f;
    private float _wallSlidingSpeed = 0.75f;
    private float _walljumpingDirection;
    private float _wallJumpingTime = 0.5f;
    private float _wallJumpingCounter;

    // Variáveis serializadas para permitir a configuração no editor Unity
    [SerializeField] private int _jumpCountMax = 2;
    [SerializeField] private Vector2 _wallJumpingPower = new Vector2(8f, 16f);
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private GameObject _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private GameObject _wallCheck;
    [SerializeField] private LayerMask _wallLayer;
    [SerializeField] private GameObject _attackCheck;
    [SerializeField] private float _attackCooldownTime = 0.1f;
    [SerializeField] private TMP_Text _interactivePoint;

    // Variáveis públicas para controle de inventário e dashing
    public bool IsDashing = false;
    public bool cooldownDash = false;
    public float dashCooldown = 0.5f;
    public float dashSpeed = 10f;

    // Variáveis privadas para controle de combo e ataque
    private List<string> _comboSequence = new List<string>();
    private float _comboTimer = 0f;
    private float _comboMaxTime = 2f;
    private float _maxCombo = 10f;
    private float _attackCooldown = 0f;
    private float _attackSpeedDump = 0f;

    private void Awake()
    {
        Weapon weapon = new Weapon("Sword", 10, 1, 1);
        equipedWeapon = weapon;
        _attackSpeedDump = _attackCooldown;
    }
    private void Update()
    {
        if(GameManager.instance.curState == GameState.Playing){
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (IsWalled())
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

        if (Input.GetKeyDown(KeyCode.LeftShift) && !cooldownDash)
            Dash();
        }

    }
    public void ShowMessage(string Message)
    {
        _interactivePoint.text = Message;
        _interactivePoint.enabled = true;
    }
    public void HideMessage()
    {
        _interactivePoint.enabled = false;
    }
    private void UpdateComboTimer()
    {
        if (_comboSequence.Count > 0)
        {
            _comboTimer -= Time.deltaTime;
            if (_comboTimer <= 0f)
            {
                ResetCombo();
            }
        }
    }
    private void UpdateAttackCooldown()
    {
        if (_attackCooldown > 0f)
        {
            _attackCooldown -= Time.deltaTime;
        }
    }

    private void ProcessAttackInput()
    {
        if (_attackCooldown <= 0f)
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
            _attackCooldown = _attackCooldownTime;

        }
    }

    public void AddCombo(string combo)
    {

        _comboSequence.Add(combo);
        if (_comboSequence.Count > _maxCombo)
        {
            ResetCombo();
            return;
        }
        _comboTimer = _comboMaxTime;

        string lastCombo = _comboSequence[_comboSequence.Count - 1];
        string beforeCombo;
        try

        {
            beforeCombo = _comboSequence[_comboSequence.Count - 2];
        }
        catch (Exception e)
        {
            beforeCombo = "";
        }
        
        float damage = 0f;
        float DeltaS = 0;
        Color color = spriteRenderer.color;
        switch (lastCombo)
        {
            //I = Vermelho = Forte
            case "I":
                color -= new Color(0f, 0.1f, 0.1f, 0f);
                color += new Color(0.1f, 0f, 0f, 0f);
                
                animator.SetInteger("AttackType", 1);
                DeltaS = ((EntityStatus.GetStat(StatType.Luck).Value+equipedWeapon.CritChance) % (UnityEngine.Random.Range(1, 100)/100)) / 100;
                if(DeltaS > 50) {
                    damage = (EntityStatus.GetStat(StatType.Strength).Value * (EntityStatus.GetStat(StatType.Luck).Value / DeltaS * 2)) + equipedWeapon.Damage + 1;
                } else {
                    damage = (EntityStatus.GetStat(StatType.Strength).Value) + equipedWeapon.Damage + 1;
                }

                break;
            //O = Verde = Rapido
            case "O":
                color -= new Color(0.1f, 0f, 0.1f, 0f);
                color += new Color(0f, 0.1f, 0f, 0f);

                animator.SetInteger("AttackType", 2);
                damage = (EntityStatus.GetStat(StatType.Strength).Value * 0.5f) + equipedWeapon.Damage + 1;
                //_attackCooldown = _attackCooldownTime - (EntityStatus.GetStat(StatType.Dexterity).Value - inventory.Peso);
                break;

            //P = Azul = Magico
            case "P":
                color -= new Color(0.1f, 0.1f, 0f, 0f);
                color += new Color(0f, 0f, 0.1f, 0f);
                animator.SetInteger("AttackType", 3);
                DeltaS = ((EntityStatus.GetStat(StatType.Luck).Value + equipedWeapon.CritChance) % (UnityEngine.Random.Range(1, 100) / 100)) / 100;
                damage = (EntityStatus.GetStat(StatType.Energi).Value * (EntityStatus.GetStat(StatType.Luck).Value / DeltaS));
                break;
            case "J":
                color -= new Color(0.1f, 0.1f, 0.1f, 0f);
                color += new Color(0.1f, 0.1f, 0.1f, 0f);
                animator.SetInteger("AttackType", 4);
                break;
        }
        StartCoroutine(ChangeColorGradually(color, 0.01f));

        Debug.Log(damage);
        MakeDamage(damage);
        if (_comboSequence.Count == 10)
        {
            ResetCombo();
            return;
        }
        animator.SetInteger("AttackSequence", _comboSequence.Count);
        animator.SetTrigger("Attack");
        Debug.Log(lastCombo);
        Debug.Log(beforeCombo);
    }
    private void ResetCombo()
    {
        _comboSequence.Clear();
        _comboTimer = 0f;
        animator.SetInteger("AttackSequence", 0);
        animator.SetInteger("AttackType", 0);
        switch (CurColor)
        {
            case Colors.WHITE:
                StartCoroutine(ChangeColorGradually(new Color(1f, 1f, 1f, 1f),0.1f));
                break;
            case Colors.RED:
                StartCoroutine(ChangeColorGradually(new Color(1f,0f,0f,1f),0.1f));
                break;
            case Colors.BLUE:
                StartCoroutine(ChangeColorGradually(new Color(0f, 0f, 1f, 1f),.1f));
                break;
            case Colors.YELLOW:
                StartCoroutine(ChangeColorGradually(new Color(1f, 1f, 0f, 1f), .1f));
                break;
            case Colors.BLACK:
                StartCoroutine(ChangeColorGradually(new Color(0f, 0f, 0f, 1f), .1f));
                break;
        }
        _attackCooldown = _attackSpeedDump;
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }
    IEnumerator ChangeColorGradually(Color targetColor, float duration)
    {
        Color initialColor = spriteRenderer.color;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            spriteRenderer.color = Color.Lerp(initialColor, targetColor, t);
            yield return null;
        }
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
        if (_rb.velocity.y < 0f)
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
        if (jumpCount < _jumpCountMax)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            jumpCount++;
            animator.SetBool("isJump", true);
            animator.SetBool("isFall", false);
        }
    }
    private void Move()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        Flip();
        _rb.velocity = new Vector2(_horizontal * _speed, _rb.velocity.y);
        animator.SetBool("isRun", _horizontal != 0f);
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
        bool isground = Physics2D.OverlapCircle(_groundCheck.transform.position, 0.2f, _groundLayer);
        if (isground)
        {
            Land();
        }
        return isground;
    }

    private bool IsWalled()
    {

        return Physics2D.OverlapCircle(_wallCheck.transform.position, 0.2f, _wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && _horizontal != 0f)
        {
            isWallSliding = true;
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Clamp(_rb.velocity.y, -_wallSlidingSpeed, float.MaxValue));
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
            _walljumpingDirection = -transform.localScale.x;
            _wallJumpingCounter = _wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            _wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && _wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            _rb.velocity = new Vector2(_walljumpingDirection * _wallJumpingPower.x, _wallJumpingPower.y);
            _wallJumpingCounter = 0f;

            if (transform.localScale.x != _walljumpingDirection)
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
        if (isFacingRight && _horizontal < 0f || !isFacingRight && _horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private void Dash()
    {
        cooldownDash = true;
        Invoke(nameof(StopDash), dashCooldown);
        _rb.AddForce(new Vector2(_rb.velocity.x * dashSpeed, _rb.velocity.y));
    }
    private void StopDash()
    {
        cooldownDash = false;
    }
    private void MakeDamage(float damage)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackCheck.transform.position, 0.5f);
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
                      "wallJumpingCounter: " + _wallJumpingCounter + "\n" +
                      "isFacingRight: " + isFacingRight + "\n" +
                      "horizontal: " + _horizontal + "\n" +
                      "rb.velocity: " + _rb.velocity + "\n" +
                      "Wall Count: " + jumpCount;
        // Draw in screen
        GUI.Label(new Rect(10, 10, 500, 500), text);
    }
}
