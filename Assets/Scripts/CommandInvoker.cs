using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    private static Queue<ICommand> _commandBuffer;
    private static Stack<ICommand> _commandHistory;
    private static Stack<ICommand> _commandFuture;

    public static int UndoCount
    {
        get { return _commandHistory.Count; }
    }
    public static int RedoCount
    {
        get { return _commandFuture.Count; }
    }

    public static void AddCommand(ICommand command)
    {
        _commandBuffer.Enqueue(command);
    }

    private void Awake()
    {
        _commandBuffer = new Queue<ICommand>();
        _commandHistory = new Stack<ICommand>();
        _commandFuture = new Stack<ICommand>();
    }

    void Update()
    {
        if (_commandBuffer.Count > 0)
        {
            ICommand command = _commandBuffer.Dequeue();
            command.Execute();

            _commandHistory.Push(command);
            _commandFuture.Clear();
        }
    }

    public static void Undo()
    {
        if (_commandHistory.Count > 0)
        {
            ICommand command = _commandHistory.Pop();
            command.Undo();
            _commandFuture.Push(command);
        }
    }

    public static void Redo()
    {
        if (_commandFuture.Count > 0)
        {
            ICommand command = _commandFuture.Pop();
            command.Execute();
            _commandHistory.Push(command);
        }
    }
}
