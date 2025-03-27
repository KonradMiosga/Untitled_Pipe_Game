using System;
using UnityEngine;

public class PipePlacement : MonoBehaviour
{

    [SerializeField] PlayerMovement playerMovement;
    public event Action OnClicked;
    [SerializeField]
    private ObjectsDatabaseSO database;
    private int selectedObjectIndex = -1;
    int rotX;
    int rotY;
    int rotZ;
    void Start()
    {
        //int rand = UnityEngine.Random.Range(0, database.objectsData.Count);
        StartPlacement();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClicked?.Invoke();
        }
        if (playerMovement.isMoving) return;
    }

    public void StartPlacement()
    {
        //int ID = UnityEngine.Random.Range(0, database.objectsData.Count);
        //selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        OnClicked += PlaceStructure;
    }

    private void PlaceStructure()
    {
        if (!playerMovement.isMoving)
        {
            selectedObjectIndex = UnityEngine.Random.Range(0, database.objectsData.Count);
            GameObject gameObject = Instantiate(database.objectsData[selectedObjectIndex].Prefab);
            gameObject.transform.position = transform.position;
            rotX = UnityEngine.Random.Range(0, 4) * 90;
            rotY = UnityEngine.Random.Range(0, 4) * 90;
            rotZ = UnityEngine.Random.Range(0, 4) * 90;

            gameObject.transform.rotation = Quaternion.Euler(rotX, rotY, rotZ);

        }
    }
}
