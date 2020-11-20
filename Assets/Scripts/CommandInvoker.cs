using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    private static Queue<ICommand> _commandBuffer;
    private static Stack<ICommand> _commandHistory;
    private static Stack<ICommand> _commandFuture;

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
            Log();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (_commandHistory.Count > 0)
                {
                    ICommand command = _commandHistory.Pop();
                    command.Undo();
                    _commandFuture.Push(command);
                    Log();
                }
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                if (_commandFuture.Count > 0)
                {
                    ICommand command = _commandFuture.Pop();
                    command.Execute();
                    _commandHistory.Push(command);
                    Log();
                }
            }
        }
    }

    private void Log()
    {
        Debug.Log("History: " + _commandHistory.Count + " | Future: " + _commandFuture.Count);
    }
}
