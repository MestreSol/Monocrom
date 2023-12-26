using UnityEngine;

public class PlayerState
{
    public Player player;

    public Colors color;

    public bool isDashing;
    public bool isWallSliding;
    public bool isFacingRight;

    [SerializeField] private GameObject _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    public bool isGrounded()
    {
        bool isGround = Physics2D.OverlapCircle(_groundCheck.transform.position, 0.2f, _groundLayer);
        if (isGround)
        {
            Land();
        }
        return isGround;
    }
    public void Land()
    {
        player.movement._wallJumpingCounter = 0;
        player.animation.Land();
    }
    public bool isWalled()
    {
        bool isWall = Physics2D.OverlapCircle(player.movement._wallCheck.transform.position, 0.2f, player.movement._wallLayer);
        return isWall;
    }
    public bool isAttacking()
    {
        bool isAttacking = Physics2D.OverlapCircle(player.combat._attackCheck.transform.position, 0.2f, player.combat._enemyLayer);
        return isAttacking;
    }
}