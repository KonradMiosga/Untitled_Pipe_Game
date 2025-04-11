using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] public Vector3Int gridSize;
    [SerializeField] int unityGridSize;
    [SerializeField] Material material;
    public Vector3Int inputPos;
    public Vector3Int outputPos;
    public int UnityGridSize { get { return unityGridSize; } }
    public Dictionary<Vector3Int, Node> grid = new Dictionary<Vector3Int, Node>();
    public List<Node> path = new List<Node>();

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
                    grid.Add(cords, new Node(cords, true, connections));
                }
            }
        }
        //PrintGrid();
    }

    public void DrawGridBounds()
    {
        GameObject boundsCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Vector3 size = new Vector3(gridSize.x, gridSize.y, gridSize.z) * unityGridSize;
        Vector3 center = size / 2f - new Vector3(unityGridSize, unityGridSize, unityGridSize) / 2f;

        boundsCube.transform.position = center;
        boundsCube.transform.localScale = size;

        boundsCube.GetComponent<Renderer>().material = material;
        boundsCube.transform.SetParent(transform);
    }

    public void PlaceInCenter(GameObject obj)
    {
        Vector3 size = new Vector3(gridSize.x, gridSize.y, gridSize.z) * unityGridSize;
        Vector3 center = size / 2f - new Vector3(unityGridSize, unityGridSize, unityGridSize) / 2f;
        obj.transform.position = center;
    }

    public void AddPipetoGrid(ObjectsDataPipes pipe, Vector3 pos, GameObject pipeGameObject)
    {
        Vector3Int cords = WorldToGridCoords(pos);
        if (!grid.ContainsKey(cords))
        {
            Debug.LogError($"Grid does NOT contain key: {cords}");
        }
        else
        {
            grid[cords].cords = cords;
            grid[cords].isFree = false;
            grid[cords].gameObject = pipeGameObject;
            grid[cords].connections.Xpos = pipe.Xpos;
            grid[cords].connections.Xneg = pipe.Xneg;
            grid[cords].connections.Ypos = pipe.Ypos;
            grid[cords].connections.Yneg = pipe.Yneg;
            grid[cords].connections.Zpos = pipe.Zpos;
            grid[cords].connections.Zneg = pipe.Zneg;
        }
    }

    public void AddInOuttoGrid(ObjectsDataInOut inOut, Vector3 pos, GameObject inOutGameObject)
    {
        Vector3Int cords = WorldToGridCoords(pos);
        if (!grid.ContainsKey(cords))
        {
            Debug.LogError($"Grid does NOT contain key: {cords}");
        }
        else
        {
            grid[cords].cords = cords;
            grid[cords].isFree = false;
            grid[cords].gameObject = inOutGameObject;
            grid[cords].connections.Xpos = inOut.Xpos;
            grid[cords].connections.Xneg = inOut.Xneg;
            grid[cords].connections.Ypos = inOut.Ypos;
            grid[cords].connections.Yneg = inOut.Yneg;
            grid[cords].connections.Zpos = inOut.Zpos;
            grid[cords].connections.Zneg = inOut.Zneg;
        }
    }

    public Vector3Int WorldToGridCoords(Vector3 worldPos)
    {
        return new Vector3Int(
            Mathf.FloorToInt(worldPos.x / unityGridSize),
            Mathf.FloorToInt(worldPos.y / unityGridSize),
            Mathf.FloorToInt(worldPos.z / unityGridSize)
        );
    }

    public void PrintGrid()
    {
        foreach (KeyValuePair<Vector3Int, Node> entry in grid)
        {
            Debug.Log($"Coord: {entry.Key} | Empty?: {entry.Value.connections}");
        }
    }

    public bool IsValidPlace(Vector3 worldPosition)
    {
        Vector3Int coords = WorldToGridCoords(worldPosition);
        if (grid.ContainsKey(coords))
        {
            return grid[coords].isFree;
        }

        return false;
    }


}