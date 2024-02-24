using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Idiomas
{
    Common,
    Dwarvish,
    Elvish,
    Giant,
    Gnomish,
    Goblin,
    Halfling,
    Orc,
    Abyssal,
    Celestial,
    Draconic,
    Deep_Speech,
    Invernal,
    Primordial,
    Sylvan,
    Undercommon
}

[System.Serializable]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Entity : MonoBehaviour
{
    [Header("Unity Objects")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    [Header("Variables")]
    public float wallCheckDistance;
    public float speed;
    public float dirSpeed;
    public float flySpeed;
    public float jumpForce;
    public float wallJumpingDuration = 0.1f;

    public bool canFlying;

    public float armor;
    public List<Idiomas> idiomas;

    [Header("Player Attributes")]
    public float life = 0;
    public float maxLife;
    
    public EntityStatus EntityStatus;
    public int jumpCount = 0;

    [Header("Player Status")]
    public bool inGround = false;
    public bool isWallSliding = false;
    public bool isFacingRight = true;
    public bool isWallJumping = false;
    public bool isJumping = false;
    public Rigidbody2D rigidbody;
    public void TakeDamage(float damage){
        life -= damage;
        animator.SetTrigger("TakeDamage");
        if(life <= 0){
            Die();
        }
    }
    private void Die(){
        Destroy(gameObject);
    }
    public void MoveLeft(float horizontalForce)
    {
        if (isFacingRight)
        {
            Flip();
        }
        rigidbody.velocity = new Vector2(-horizontalForce, rigidbody.velocity.y);
    }
    public void MoveRight(float horizontalForce)
    {
        if (!isFacingRight)
        {
            Flip();
        }
        rigidbody.velocity = new Vector2(horizontalForce, rigidbody.velocity.y);
    }
    public void Jump()
    {
        if (jumpCount < 2)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
        }
    }
    public void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = true;
            isWallSliding = false;
            jumpCount = 0;
            StartCoroutine(DisableMovement(0.1f));
            if (isFacingRight)
            {
                rigidbody.AddForce(new Vector2(-1, 1) * jumpForce, ForceMode2D.Impulse);
            }
            else
            {
                rigidbody.AddForce(new Vector2(1, 1) * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
    public void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    private IEnumerator DisableMovement(float duration)
    {
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        isWallJumping = false;
    }
    public bool isTouchingWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, wallCheckDistance);
        return hit.collider != null;
    }
    public bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, wallCheckDistance);
        return hit.collider != null;
    }
}