using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _cubePrefab;

    [SerializeField]
    private UIManager _uiManager;

    private Camera _mainCam;

    void Start()
    {
        _mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
            {
                Color c = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));

                ICommand command = new PlaceCubeCommand(hitInfo.point, c, _cubePrefab);
                CommandInvoker.AddCommand(command);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            CommandInvoker.Undo();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            CommandInvoker.Redo();
        }

        // At the moment I need to update counter on every frame 
        // in order to show correct values because command are enqueued 
        // and then invoked. 
        // An better option would be to send an event from Command invoker when 
        // its state has changed.
        UpdateUICounters();
    }

    private void UpdateUICounters()
    {
        _uiManager.SetUndoCount(CommandInvoker.UndoCount);
        _uiManager.SetRedoCount(CommandInvoker.RedoCount);
    }
}
