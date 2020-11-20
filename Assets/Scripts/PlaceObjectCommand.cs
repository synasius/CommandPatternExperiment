using UnityEngine;

public class PlaceObjectCommand : ICommand
{
    private Vector3 _position;
    private Color _color;
    private GameObject _prefab;

    private GameObject _instancedCube;

    public PlaceObjectCommand(Vector3 position, Color color, GameObject prefab)
    {
        _position = position;
        _color = color;
        _prefab = prefab;
    }

    public void Execute()
    {
        _instancedCube = ObjectPlacer.PlaceObject(_position, _color, _prefab);
    }

    public void Undo()
    {
        if (_instancedCube != null)
        {
            GameObject.Destroy(_instancedCube);
            _instancedCube = null;
        }
    }

    public bool IsUndoable()
    {
        return true;
    }
}
