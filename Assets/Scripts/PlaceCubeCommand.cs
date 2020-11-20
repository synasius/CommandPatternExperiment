using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCubeCommand : ICommand
{
    private Vector3 _position;
    private Color _color;
    private GameObject _cubePrefab;

    private GameObject _instancedCube;

    public PlaceCubeCommand(Vector3 position, Color color, GameObject cubePrefab)
    {
        _position = position;
        _color = color;
        _cubePrefab = cubePrefab;
    }

    public void Execute()
    {
        _instancedCube = CubePlacer.PlaceCube(_position, _color, _cubePrefab);
    }

    public void Undo()
    {
        if (_instancedCube != null)
        {
            GameObject.Destroy(_instancedCube);
            _instancedCube = null;
        }

        Debug.Assert(_instancedCube == null);
    }
}
