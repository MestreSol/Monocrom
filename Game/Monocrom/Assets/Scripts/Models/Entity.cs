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

}