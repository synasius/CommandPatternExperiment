using System.Collections.Generic;
using UnityEngine;

public delegate void CountChangedHandler();

public class CommandInvoker : MonoBehaviour
{
    private Queue<ICommand> _commandBuffer;
    private Stack<ICommand> _undoCommandStack;
    private Stack<ICommand> _redoCommandStack;

    public event CountChangedHandler CountChanged;

    public int UndoCount
    {
        get { return _undoCommandStack.Count; }
    }
    public int RedoCount
    {
        get { return _redoCommandStack.Count; }
    }

    private void Awake()
    {
        _commandBuffer = new Queue<ICommand>();
        _undoCommandStack = new Stack<ICommand>();
        _redoCommandStack = new Stack<ICommand>();
    }

    void Update()
    {
        if (_commandBuffer.Count > 0)
        {
            ICommand command = _commandBuffer.Dequeue();
            command.Execute();

            if (command.IsUndoable())
            {
                _undoCommandStack.Push(command);
                _redoCommandStack.Clear();
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
        _undoCommandStack.Clear();
        _redoCommandStack.Clear();
    }

    public void Undo()
    {
        if (_undoCommandStack.Count > 0)
        {
            ICommand command = _undoCommandStack.Pop();
            command.Undo();

            _redoCommandStack.Push(command);
            CountChanged?.Invoke();
        }
    }

    public void Redo()
    {
        if (_redoCommandStack.Count > 0)
        {
            ICommand command = _redoCommandStack.Pop();
            command.Execute();
            _undoCommandStack.Push(command);
            CountChanged?.Invoke();
        }
    }

}
