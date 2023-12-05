using System.Collections.Generic;
using UnityEngine;

public class CommandHandler : MonoBehaviour
{
    private readonly Stack<ICommand> _commands = new Stack<ICommand>();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _commands.Push(command);
    }

    public void Undo()
    {
        if (_commands.Count == 0)
        {
            return;
        }

        var command = _commands.Pop();
        command.Undo();
    }
}
