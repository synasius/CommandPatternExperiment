using System.Collections.Generic;
using UnityEngine;

public delegate void CountChangedHandler();

public class CommandInvoker : MonoBehaviour
{
    private Queue<ICommand> _commandBuffer;
    private Stack<ICommand> _commandHistory;
    private Stack<ICommand> _commandFuture;

    public event CountChangedHandler CountChanged;

    public int UndoCount
    {
        get { return _commandHistory.Count; }
    }
    public int RedoCount
    {
        get { return _commandFuture.Count; }
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

            if (command.IsUndoable())
            {
                _commandHistory.Push(command);
                _commandFuture.Clear();
            }
            else
            {
                ResetHistory();
            }
            CountChanged?.Invoke();
        }
    }

    public void AddCommand(ICommand command)
    {
        _commandBuffer.Enqueue(command);
    }

    public void ResetHistory()
    {
        _commandHistory.Clear();
        _commandFuture.Clear();
    }

    public void Undo()
    {
        if (_commandHistory.Count > 0)
        {
            ICommand command = _commandHistory.Pop();
            command.Undo();

            _commandFuture.Push(command);
            CountChanged?.Invoke();
        }
    }

    public void Redo()
    {
        if (_commandFuture.Count > 0)
        {
            ICommand command = _commandFuture.Pop();
            command.Execute();
            _commandHistory.Push(command);
            CountChanged?.Invoke();
        }
    }

}
