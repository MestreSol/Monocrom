using UnityEngine;

public class WallSliderCommand : ICommand
{
    private Entity _targert;
    private float _jumpForce;
    private float _jumpDirection;
    // Na classe Entity
    public WallSliderCommand(Entity entity, float jumpForce, float jumpDirection)
    {
        _targert = entity;
        _jumpForce = jumpForce;
        _jumpDirection = jumpDirection;
    }

    // Na classe WallJumpCommand
    public void Execute()
    {
        // Se o jogador está deslizando na parede, faça-o saltar para frente
        if (_targert.animator.GetBool("isWallSliding"))
        {
            _targert.spriteRenderer.flipX = _jumpDirection < 0;
            _jumpDirection = _targert.spriteRenderer.flipX ? -1 : 1;

            // Faça o jogador saltar para frente
            _targert.Rigidbody2D.velocity = new Vector2(_jumpDirection * _targert.Speed, _jumpForce);

            // Gire o jogador para que o wallcheck fique virado para frente
            _targert.Rigidbody2D.transform.Rotate(0, _targert.spriteRenderer.flipX ? 180 : 0, 0);

            // Desative a animação de deslizamento na parede
            _targert.animator.SetBool("isWallSliding", false);
        }
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }

    public void Redo()
    {
        throw new System.NotImplementedException();
    }
}