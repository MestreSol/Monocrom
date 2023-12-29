using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Player player;
    public SpriteRenderer sprite;
    public void Walking()
    {

    }
    public void Flip()
    {
        if (player.state.isFacingRight && player.movement._horizontal < 0f || !player.state.isFacingRight && player.movement._horizontal > 0f)
        {
            player.state.isFacingRight = !player.state.isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void Land()
    {
        player.animator.SetTrigger("Land");
    }
    public void Jump()
    {
        player.animator.SetTrigger("Jump");
    }
    public void AttackUpdate(int Sequence, int Type)
    {
        player.animator.SetInteger("AttackSequence", Sequence);
        player.animator.SetInteger("AttackType", Type);

        switch (player.state.color)
        {
            case Colors.WHITE:
                sprite.color = new Color(1f, 1f, 1f, 1f);
                break;
            case Colors.RED:
                sprite.color = new Color(1f, 0f, 0f, 1f);
                break;
            case Colors.GREEN:
                sprite.color = new Color(0f, 1f, 0f, 1f);
                break;
            case Colors.BLUE:
                sprite.color = new Color(0f, 0f, 1f, 1f);
                break;
            case Colors.BLACK:
                sprite.color = new Color(1f, 1f, 1f, 1f);
                break;
        }
    }
    public void UpdateColor(Colors color)
    {
        switch(color)
        {
            case Colors.WHITE:
                sprite.color = new Color(1f, 1f, 1f, 1f);
                break;
            case Colors.RED:
                sprite.color = new Color(1f, 0f, 0f, 1f);
                break;
            case Colors.GREEN:
                sprite.color = new Color(0f, 1f, 0f, 1f);
                break;
            case Colors.BLUE:
                sprite.color = new Color(0f, 0f, 1f, 1f);
                break;
            case Colors.BLACK:
                sprite.color = new Color(0f, 0f, 0f, 1f);
                break;
        }
        player.state.color = color;
    }
}