using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpCommand : ICommand
{
    private Entity _targert;
    private float _jumpForce;
    private float _jumpDirection;

    public WallJumpCommand(Entity targert, float jumpForce, float jumpDirection)
    {
        _targert = targert;
        _jumpForce = jumpForce;
        _jumpDirection = jumpDirection;
    }
    public void Execute()
    {
        _targert.spriteRenderer.flipX = _jumpDirection < 0;
        _jumpDirection = _targert.spriteRenderer.flipX ? -1 : 1;
        _targert.Rigidbody2D.velocity = new Vector2(_jumpDirection * _targert.Speed, _jumpForce);
    }

    public void Redo()
    {
        Execute();
    }
    public void Undo(){
        _targert.Rigidbody2D.velocity = new Vector2(0, 0);
    }
}
