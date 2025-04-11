using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PipePlacement : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    public event Action OnClicked;
    [SerializeField] private ObjectsDatabasePipes databasePipes;
    private int selectedObjectIndex = -1;
    private GameObject _ghostObject;
    [SerializeField] private Material material;
    [SerializeField] private Material ghostMat_green;
    [SerializeField] private Material ghostMat_red;
    private GridManager _gridManager;

    void Awake()
    {
        if (_gridManager == null)
            _gridManager = FindFirstObjectByType<GridManager>();
    }

    void Start()
    {
        StartPlacement();
    }

    void Update()
    {
        if (_ghostObject != null)
        {
            _ghostObject.transform.position = transform.position;

            if (_gridManager.IsValidPlace(transform.position))
            {
                _ghostObject.GetComponent<Renderer>().material = ghostMat_green;
            }
            else
            {
                _ghostObject.GetComponent<Renderer>().material = ghostMat_red;
            }
        }

        if (playerMovement.isMoving) return;

        if (Input.GetMouseButtonDown(0))
        {
            OnClicked?.Invoke();
        }
    }

    public void StartPlacement()
    {
        OnClicked += PlaceStructure;
        PrepareNextGhost();
    }

    private void PlaceStructure()
    {
        if (playerMovement.isMoving || !_gridManager.IsValidPlace(transform.position)) return;

        ObjectsDataPipes pipe = databasePipes.objectsData[selectedObjectIndex];
        GameObject placed = Instantiate(pipe.Prefab);
        placed.GetComponent<Renderer>().material = material;
        placed.transform.position = transform.position;

        Vector3Int pos = _gridManager.WorldToGridCoords(placed.transform.position);

        _gridManager.AddPipetoGrid(pipe, placed.transform.position, placed);
        Debug.Log($"Pipe placed at {_gridManager.WorldToGridCoords(placed.transform.position)} with connections {_gridManager.grid[_gridManager.WorldToGridCoords(placed.transform.position)].ToString()}");

        CheckIfConnected(_gridManager.grid[pos]);

        Destroy(_ghostObject);
        PrepareNextGhost();
    }

    private void PrepareNextGhost()
    {
        selectedObjectIndex = UnityEngine.Random.Range(0, databasePipes.objectsData.Count);

        _ghostObject = Instantiate(databasePipes.objectsData[selectedObjectIndex].Prefab);
        _ghostObject.GetComponent<Renderer>().material = ghostMat_green;
    }

    private void CheckIfConnected(Node node)
    {
        Vector3Int[] directions = new Vector3Int[]
        {
        Vector3Int.right,  // Right (X+)
        Vector3Int.left, // Left (X-)
        Vector3Int.up,  // Up (Y+)
        Vector3Int.down, // Down (Y-)
        Vector3Int.forward,  // Forward (Z+)
        Vector3Int.back  // Backward (Z-)
        };

        foreach (Vector3Int dir in directions)
        {
            Vector3Int neighborCoords = node.cords + dir;

            if (_gridManager.grid.ContainsKey(neighborCoords))
            {
                Node neighbor = _gridManager.grid[neighborCoords];

                if (!neighbor.isFree) // Only check if there's a pipe
                {
                    if (ArePipesConnected(node, neighbor, dir))
                    {
                        node.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                        neighbor.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                        Debug.Log($"Connected: {node.cords} ↔ {neighbor.cords}");
                    }
                }
            }
        }
    }

    private bool ArePipesConnected(Node pipe1, Node pipe2, Vector3Int direction)
    {
        if (direction == Vector3Int.right) // X+
            return pipe1.connections.Xpos == 1 && pipe2.connections.Xneg == 1;

        if (direction == Vector3Int.left) // X-
            return pipe1.connections.Xneg == 1 && pipe2.connections.Xpos == 1;

        if (direction == Vector3Int.up) // Y+
            return pipe1.connections.Ypos == 1 && pipe2.connections.Yneg == 1;

        if (direction == Vector3Int.down) // Y-
            return pipe1.connections.Yneg == 1 && pipe2.connections.Ypos == 1;

        if (direction == Vector3Int.forward) // Z+
            return pipe1.connections.Zpos == 1 && pipe2.connections.Zneg == 1;

        if (direction == Vector3Int.back) // Z-
            return pipe1.connections.Zneg == 1 && pipe2.connections.Zpos == 1;

        return false;
    }



}
