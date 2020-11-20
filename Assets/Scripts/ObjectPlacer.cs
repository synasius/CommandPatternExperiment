using UnityEngine;

public static class ObjectPlacer
{
    public static GameObject PlaceObject(Vector3 position, Color color, GameObject prefab)
    {
        GameObject cube = GameObject.Instantiate(prefab, position, Quaternion.identity);
        cube.GetComponent<MeshRenderer>().material.color = color;
        return cube;
    }
}
