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
    public float Speed;
    public bool CanFlying;
    public float FlySpeed;
    public float JumpForce;
    public float wallJumpingDuration = 0.1f;

    public float Armor;
    public List<Idiomas> idiomas;

    [Header("Player Attributes")]
    public float Life = 0;
    public float MaxLife;
    
    public int Forca = 0;
    public int Destresa = 0;
    public int Sorte = 0;
    public int Persepcao = 0;
    public int Constituicao = 0;
    public int Inteligencia = 0;
    public int Carisma = 0;
    public int Energia = 0;
    public int jumpCount = 0;

    [Header("Player Status")]
    public bool inGround = false;
    public bool isWallSliding = false;
    public bool isFacingRight = true;
    public bool isWallJumping = false;
    public bool isJumping = false;

    public void TakeDamage(float damage){
        Life -= damage;
        if(Life <= 0){
            Die();
        }
    }
    private void Die(){
        Destroy(gameObject);
    }

}