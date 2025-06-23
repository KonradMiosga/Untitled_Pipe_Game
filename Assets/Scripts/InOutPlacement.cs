using UnityEngine;

public class InOutPlacement : MonoBehaviour
{
    [SerializeField] private ObjectsDatabaseInOut databaseInOut;
    private int _side;
    private GridManager _gridManager;
    private GameManager _gameManager;
    private ObjectsDataInOut _input;
    private ObjectsDataInOut _output;
    private int _xOut = 0;
    private int _yOut = 0;
    private int _zOut = 0;
    private int _xIn = 0;
    private int _yIn = 0;
    private int _zIn = 0;

    void Awake()
    {
        if (_gridManager == null)
            _gridManager = FindFirstObjectByType<GridManager>();
        if (_gameManager == null)
            _gameManager = FindFirstObjectByType<GameManager>();
    }
    public void PlaceInOutInWorld()
    {
        _side = UnityEngine.Random.Range(0, 6);

        switch (_side)
        {
            case 0: //up
                _xOut = UnityEngine.Random.Range(0, _gridManager.gridSize.x);
                _yOut = _gridManager.gridSize.y - 1;
                _zOut = UnityEngine.Random.Range(0, _gridManager.gridSize.z);
                _xIn = UnityEngine.Random.Range(0, _gridManager.gridSize.x);
                _yIn = 0;
                _zIn = UnityEngine.Random.Range(0, _gridManager.gridSize.z);
                break;
            case 1: //down
                _xOut = UnityEngine.Random.Range(0, _gridManager.gridSize.x);
                _yOut = 0;
                _zOut = UnityEngine.Random.Range(0, _gridManager.gridSize.z);
                _xIn = UnityEngine.Random.Range(0, _gridManager.gridSize.x);
                _yIn = _gridManager.gridSize.y - 1;
                _zIn = UnityEngine.Random.Range(0, _gridManager.gridSize.z);
                break;
            case 2: //left
                _xOut = 0;
                _yOut = UnityEngine.Random.Range(0, _gridManager.gridSize.y);
                _zOut = UnityEngine.Random.Range(0, _gridManager.gridSize.z);
                _xIn = _gridManager.gridSize.x - 1;
                _yIn = UnityEngine.Random.Range(0, _gridManager.gridSize.y);
                _zIn = UnityEngine.Random.Range(0, _gridManager.gridSize.z);
                break;
            case 3: //right
                _xOut = _gridManager.gridSize.x - 1;
                _yOut = UnityEngine.Random.Range(0, _gridManager.gridSize.y);
                _zOut = UnityEngine.Random.Range(0, _gridManager.gridSize.z);
                _xIn = 0;
                _yIn = UnityEngine.Random.Range(0, _gridManager.gridSize.y);
                _zIn = UnityEngine.Random.Range(0, _gridManager.gridSize.z);
                break;
            case 4: //fwd
                _xOut = UnityEngine.Random.Range(0, _gridManager.gridSize.x);
                _yOut = UnityEngine.Random.Range(0, _gridManager.gridSize.y);
                _zOut = _gridManager.gridSize.z - 1;
                _xIn = UnityEngine.Random.Range(0, _gridManager.gridSize.x);
                _yIn = UnityEngine.Random.Range(0, _gridManager.gridSize.y);
                _zIn = 0;
                break;
            case 5: //bkw
                _xOut = UnityEngine.Random.Range(0, _gridManager.gridSize.x);
                _yOut = UnityEngine.Random.Range(0, _gridManager.gridSize.y);
                _zOut = 0;
                _xIn = UnityEngine.Random.Range(0, _gridManager.gridSize.x);
                _yIn = UnityEngine.Random.Range(0, _gridManager.gridSize.y);
                _zIn = _gridManager.gridSize.z - 1;
                break;
            default:
                Debug.Log("Could not place InOut");
                break;
        }

        _input = databaseInOut.objectsData[_side];
        Vector3Int posIn = new Vector3Int(_xIn, _yIn, _zIn);
        _gridManager.inputPos = posIn;
        GameObject placedIn = Instantiate(_input.Prefab, _gameManager._objectContainer.transform);
        placedIn.transform.position = posIn;
        placedIn.gameObject.GetComponent<Renderer>().material.color = Color.lightSlateGray;
        _gridManager.AddInOuttoGrid(_input, posIn, placedIn);
        _gridManager.path.Add(_gridManager.grid[posIn]);
        Debug.Log($"Input placed at {posIn} with connections {_gridManager.grid[posIn]}");

        _output = databaseInOut.objectsData[_side + 6];
        Vector3Int posOut = new Vector3Int(_xOut, _yOut, _zOut);
        _gridManager.outputPos = posOut;
        GameObject placedOut = Instantiate(_output.Prefab, _gameManager._objectContainer.transform);
        placedOut.transform.position = posOut;
        placedOut.gameObject.GetComponent<Renderer>().material.color = Color.lightSlateGray;
        _gridManager.AddInOuttoGrid(_output, posOut, placedOut);
        Debug.Log($"Output placed at {posOut} with connections {_gridManager.grid[posOut]}");

    }

}
