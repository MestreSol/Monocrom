using UnityEngine;

public class JumpCommand : ICommand
{
    private readonly Entity _entity;
    private readonly Vector2 _direction;

    public JumpCommand(Entity entity, Vector2 direction)
    {
        _entity = entity;
        _direction = direction;
    }

    public void Execute()
    {
        _entity.Rigidbody2D.velocity = _direction * _entity.Speed;
        
    }

    public void Undo()
    {
        _entity.Rigidbody2D.velocity = Vector2.zero;
    }
    public void Redo()
    {
        Execute();
    }
}