using System.Collections.Generic;
using UnityEngine;

public class CommandHandler : MonoBehaviour
{
    private readonly Stack<ICommand> _executedCommands = new Stack<ICommand>();
    private readonly Stack<ICommand> _undoneCommands = new Stack<ICommand>();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _executedCommands.Push(command);

        // Quando um novo comando é executado, limpe a pilha de comandos desfeitos
        _undoneCommands.Clear();
    }

    public void Undo()
    {
        if (_executedCommands.Count == 0)
        {
            return;
        }

        var command = _executedCommands.Pop();
        command.Undo();
        _undoneCommands.Push(command);
    }

    public void Redo()
    {
        if (_undoneCommands.Count == 0)
        {
            return;
        }

        var command = _undoneCommands.Pop();
        command.Execute();
        _executedCommands.Push(command);
    }
}