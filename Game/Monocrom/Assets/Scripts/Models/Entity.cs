using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Entity : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public float wallCheckDistance;
    public float Speed;
    public float JumpForce;
    public float wallJumpingDuration = 0.1f;

    public float MaxLife;
    public float Life = 0;

    [Header("Player Attributes")]
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
}