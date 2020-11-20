using UnityEngine;

public static class CubePlacer
{
    public static GameObject PlaceCube(Vector3 position, Color color, GameObject cubePrefab)
    {
        GameObject cube = GameObject.Instantiate(cubePrefab, position, Quaternion.identity);
        cube.GetComponent<MeshRenderer>().material.color = color;
        return cube;
    }
}
