using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerProgress))]
[RequireComponent(typeof(PlayerState))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerInventory))]
[RequireComponent(typeof(PlayerAparence))]
[RequireComponent(typeof(PlayerHUD))]
[RequireComponent(typeof(PlayerActions))]
public class Player : Entity
{
    public PlayerProgress progress;
    public PlayerState state;
    public PlayerMovement movement;
    public PlayerCombat combat;
    public PlayerAnimation animation;
    public PlayerInput input;
    public PlayerInventory inventory;
    public PlayerAparence aparence;
    public PlayerHUD hud;
    public PlayerActions actions;
    public GameObject gameObject;

    public delegate void OnPontosChangedDelegate(int pontos);
    public event OnPontosChangedDelegate OnPontosChanged;
    private int _pontos = 0;
    public int Pontos
    {
        get { return _pontos; }
        set
        {
            if (_pontos != value)
            {
                _pontos = value;
                OnPontosChanged?.Invoke(_pontos);
            }
        }
    }
    private int _cristais;
    public int Cristais
    {
        get { return _cristais; }
        set
        {
            if (_cristais != value)
            {
                _cristais = value;
                hud.Cristais.text = _cristais.ToString();
            }
        }
    }

    public int Nivel = 1;
    public int PontosParaProxNivel = 100;

    public delegate void OnXPChangedDelegate(int XP);
    public event OnXPChangedDelegate OnXPChanged;
    private int _XP;
    public int XP
    {
        get { return _XP; }
        set
        {
            if (_XP != value)
            {
                _XP = value;
                OnXPChanged?.Invoke(_XP);
            }
        }
    }
    public void Awake()
    {
        progress.player = this;
        state.player = this;
        movement.player = this;
        combat.player = this;
        animation.player = this;
        input.player = this;
        inventory.player = this;
        aparence.player = this;
        hud.player = this;
        actions.player = this;
        Born();
    }
    
    public void FixedUpdate()
    {
        if (!isWallJumping)
        {
            movement.Move();
        }
    }

    public void Update()
    {
        if (GameManager.instance.curState == GameState.Playing)
        {
            bool isWalled = state.isWalled();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                movement.Jump();
            }
            if (isWalled)
            {
                movement.WallSliding();
                movement.WallJumping();
            }

            combat.UpdateComboTimer();
            combat.UpdateAttackCooldown();
            combat.ProcessAttackInput();

            actions.UseHeal();

            if (Input.GetKeyDown(KeyCode.LeftShift) && !state.isDashing)
            {
                movement.Dash();
            }
            UpdateStamina();
            UpdatePosture();
        }
    }


    public float posturaRegenCooldown = 1f;
    public float posturaRegenTimer = 0f;
    public float posturaRegenRedus = 0f;
    public void UpdatePosture()
    {
        if (Posture >= maxPosture)
        {
            posturaRegenTimer = 0f; // Reset the timer when stamina is full
            return;
        }

        if (posturaRegenTimer >= posturaRegenCooldown)
        {
            float regen = (destresa / 10) - posturaRegenRedus;
            Posture += regen; // Make the increase smooth by multiplying with Time.deltaTime
            posturaRegenTimer = 0f; // Reset the timer after regenerating stamina
        }
        else
        {
            posturaRegenTimer += Time.deltaTime; // Only increase the timer if stamina is not full
        }
    }

    public float staminaRegenCooldown = 1f;
    public float staminaRegenTimer = 0f;
    public float staminaRegenRedus = 0f;

    public void UpdateStamina()
    {
        if (Stamina >= maxStamina)
        {
            staminaRegenTimer = 0f; // Reset the timer when stamina is full
            return;
        }

        if (staminaRegenTimer >= staminaRegenCooldown)
        {
            float regen = (destresa / 10) - staminaRegenRedus;
            Stamina += regen; // Make the increase smooth by multiplying with Time.deltaTime
            staminaRegenTimer = 0f; // Reset the timer after regenerating stamina
        }
        else
        {
            staminaRegenTimer += Time.deltaTime; // Only increase the timer if stamina is not full
        }
    }


    public void Start()
    {
        SteamController.Instance.UpdatePlayerLocation();
    }
}