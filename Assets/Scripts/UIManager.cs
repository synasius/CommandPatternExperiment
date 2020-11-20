using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _undoCountText;

    [SerializeField]
    private TextMeshProUGUI _redoCountText;

    [SerializeField]
    private TextMeshProUGUI _activeToolText;

    public void SetUndoCount(int count)
    {
        _undoCountText.text = "UNDO: " + count;
    }

    public void SetRedoCount(int count)
    {
        _redoCountText.text = "REDO: " + count;
    }

    public void SetActiveTool(PaintTool tool)
    {
        _activeToolText.text = tool.ToString().ToUpper();
    }
}