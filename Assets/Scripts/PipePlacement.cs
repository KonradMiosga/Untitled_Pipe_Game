using System;
using UnityEngine;

public class PipePlacement : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    public event Action OnClicked;

    [SerializeField] private ObjectsDatabasePipes databasePipes;

    private int selectedObjectIndex = -1;
    private int rotX, rotY, rotZ;

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

        GameObject placed = Instantiate(databasePipes.objectsData[selectedObjectIndex].Prefab);
        placed.GetComponent<Renderer>().material = material;
        placed.transform.position = transform.position;

        _gridManager.AddtoGrid(placed);
        Debug.Log(UnityEngine.Random.Range(-4,0));

        Destroy(_ghostObject);
        PrepareNextGhost();
    }

    private void PrepareNextGhost()
    {
        selectedObjectIndex = UnityEngine.Random.Range(0, databasePipes.objectsData.Count);

        _ghostObject = Instantiate(databasePipes.objectsData[selectedObjectIndex].Prefab);
        _ghostObject.GetComponent<Renderer>().material = ghostMat_green;
    }

}
