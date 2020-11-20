public interface ICommand
{
    void Execute();
    void Undo();

    bool IsUndoable();
}
