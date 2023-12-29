using System;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public Player player;

    public Colors color;

    public bool isDashing;
    public bool isWallSliding;
    public bool isFacingRight;
    public float groundCheckDistance;
    [SerializeField] private GameObject _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    public bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, _groundLayer);
        if (hit.collider != null)
        {
            player.inGround = true;
        }
        else
        {
            player.inGround = false;
            Land();
        }
        return player.inGround;
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