using UnityEngine;

public enum PaintTool
{
    Cube,
    Sphere,
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private CommandInvoker _invoker;

    [SerializeField]
    private GameObject _cubePrefab;

    [SerializeField]
    private GameObject _spherePrefab;

    [SerializeField]
    private UIManager _uiManager;

    private Camera _mainCam;
    private PaintTool _activeTool;

    private void Awake()
    {
        _mainCam = Camera.main;
        _activeTool = PaintTool.Cube;
        _invoker = FindObjectOfType<CommandInvoker>();
        _invoker.CountChanged += UpdateUICounters;
    }

    void Start()
    {
        UpdateUIActiveTool();
        UpdateUICounters();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
            {
                PaintAt(hitInfo.point);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            _invoker.Undo();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            _invoker.Redo();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            _activeTool = PaintTool.Cube;
            UpdateUIActiveTool();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _activeTool = PaintTool.Sphere;
            UpdateUIActiveTool();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            // CommandInvoker.AddCommand(new ResetCommand());
        }
    }

    private void PaintAt(Vector3 position)
    {
        Color c = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
        if (_activeTool == PaintTool.Cube)
        {
            _invoker.AddCommand(new PlaceObjectCommand(position, c, _cubePrefab));
        }
        else if (_activeTool == PaintTool.Sphere)
        {
            _invoker.AddCommand(new PlaceObjectCommand(position, c, _spherePrefab));
        }
    }

    private void UpdateUIActiveTool()
    {
        _uiManager.SetActiveTool(_activeTool);
    }

    private void UpdateUICounters()
    {
        _uiManager.SetUndoCount(_invoker.UndoCount);
        _uiManager.SetRedoCount(_invoker.RedoCount);
    }
}
