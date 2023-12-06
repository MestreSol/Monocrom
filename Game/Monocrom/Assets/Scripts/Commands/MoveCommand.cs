using UnityEngine;
public class MoveCommand : ICommand
{
    private readonly Entity _entity;
    private readonly Vector2 _direction;

    public MoveCommand(Entity entity, Vector2 direction)
    {
        _entity = entity;
        _direction = direction;
    }

   public void Execute()
{
    _entity.Rigidbody2D.velocity = new Vector2(_direction.x * _entity.Speed, _entity.Rigidbody2D.velocity.y);
}

public void Undo()
{
    _entity.Rigidbody2D.velocity = new Vector2(0, _entity.Rigidbody2D.velocity.y);
}
    public void Redo()
    {
        Execute();
    }
}