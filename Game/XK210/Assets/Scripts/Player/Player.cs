using UnityEngine;

[System.Serializable]
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
    public GameObject gameObject;

    public void Awake()
    {
        progress = progress == null ? new PlayerProgress() : progress;
        state = state == null ? new PlayerState() : state;
        movement = movement == null ? new PlayerMovement() : movement;
        combat = combat == null ? new PlayerCombat() : combat;
        animation = animation == null ? new PlayerAnimation() : animation;
        input = input == null ? new PlayerInput() : input;
        inventory = inventory == null ? new PlayerInventory() : inventory;
        aparence = aparence == null ? new PlayerAparence() : aparence;
        hud = hud == null ? new PlayerHUD() : hud;

        progress.player = this;
        state.player = this;
        movement.player = this;
        combat.player = this;
        animation.player = this;
        input.player = this;
        inventory.player = this;
        aparence.player = this;
        hud.player = this;

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
        if(GameManager.instance.curState == GameState.Playing)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                movement.Jump();
            }
            if (state.isWalled())
            {
                movement.WallSliding();
                movement.WallJumping();
            }

            combat.UpdateComboTimer();
            combat.UpdateAttackCooldown();
            combat.ProcessAttackInput();

            if (Input.GetKeyDown(KeyCode.LeftShift) && !state.isDashing)
            {
                movement.Dash();
            }
        }


    }
    private void OnGUI()
    {
        string text = "isGrounded: " + state.isGrounded() + "\n" +
                      "isWalled: " + state.isWalled() + "\n" +
                      "isWallSliding: " + isWallSliding + "\n" +
                      "isWallJumping: " + isWallJumping + "\n" +
                      "wallJumpingCounter: " + movement._wallJumpingCounter + "\n" +
                      "isFacingRight: " + state.isFacingRight + "\n" +
                      "horizontal: " + movement._horizontal + "\n" +
                      "rb.velocity: " + movement._rigidbody.velocity + "\n" +
                      "Wall Count: " + jumpCount;
        // Draw in screen
        GUI.Label(new Rect(10, 10, 500, 500), text);
    }
}