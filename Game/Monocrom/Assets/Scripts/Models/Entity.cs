using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Entity
{
    public Rigidbody2D Rigidbody2D;
    
    public GameObject GroundCheck;
    public GameObject WallCheck;
    public float wallCheckDistance;
    public LayerMask wallLayer;
    public LayerMask groundLayer;

    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public float Speed;
    public float JumpForce;

    public float MaxLife;
    public float Life = 0;

    public int Forca = 0;
    public int Destresa = 0;
    public int Sorte = 0;
    public int Persepcao = 0;
    public int Constituicao = 0;
    public int Inteligencia = 0;
    public int Carisma = 0;
    public int Energia = 0;
    public int jumpCount = 0;

    public bool inGround = false;
    public void WallSlide()
    {
        // Ative a anima��o de deslizamento na parede
        animator.SetBool("isWallSliding", true);
        
        WallSliderCommand wallSlide = new WallSliderCommand(this, Vector2.down * (JumpForce / 10));
        wallSlide.Execute();

    }
    public bool CanJump()
    {
        bool isground = Physics2D.OverlapCircle(GroundCheck.transform.position, 0.1f, groundLayer);
        if (isground)
        {
            jumpCount = 0;
        }
        inGround = isground;
        return jumpCount < 2;
    }

    public bool CanWallJump()
    {
        bool isWall = Physics2D.OverlapCircle(WallCheck.transform.position, 0.1f, wallLayer);
        return isWall;
    }

    public void Jump()
    {
        if (!CanJump())
        {
            return;
        }
        jumpCount++;
        JumpCommand jump = new JumpCommand(this, Vector2.up * (JumpForce / 10));
        jump.Execute();
    }
    public void Move(Vector2 direction)
    {
        MoveCommand move = new MoveCommand(this, direction);
        move.Execute();
    }
    public float CalcularDefesa()
    {
        return 0;
    }


}