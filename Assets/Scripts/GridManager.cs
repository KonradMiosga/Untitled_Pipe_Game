using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector3Int gridSize;
    [SerializeField] int unityGridSize;
    [SerializeField] Material material;
    public int UnityGridSize { get { return unityGridSize; } }
    Dictionary<Vector3Int, Node> grid = new Dictionary<Vector3Int, Node>();
    Dictionary<Vector3Int, Node> Grid { get { return grid; } }

    public void InitializeGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                for (int z = 0; z < gridSize.z; z++)
                {
                    Vector3Int cords = new Vector3Int(x, y, z);
                    Connections connections = new Connections(0, 0, 0, 0, 0, 0);
                    grid.Add(cords, new Node(cords, false, connections));
                    Node thisNode = grid[cords];
                }
            }
        }
    }

    public void DrawGridBounds()
    {
        GameObject boundsCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Vector3 size = new Vector3(gridSize.x, gridSize.y, gridSize.z) * unityGridSize;
        Vector3 center = size / 2f - new Vector3(unityGridSize, unityGridSize, unityGridSize) / 2f;

        boundsCube.transform.position = center;
        boundsCube.transform.localScale = size;

        boundsCube.GetComponent<Renderer>().material = material; // Optional
        boundsCube.transform.SetParent(transform);
    }
}