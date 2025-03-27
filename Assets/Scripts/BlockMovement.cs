using System;
using System.Collections;
using Mono.Cecil.Cil;
using UnityEditor.UI;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    private bool isMoving;
    private Vector3 origPos, targetPos;
    [SerializeField] float timeToMove = 0.1f;
    [SerializeField] Transform cam;
    [SerializeField] Collider boundingBox; // Use Collider instead of Transform for correct bounds
    private Vector3 cubeExtents;
    public event Action OnClicked;
    [SerializeField]
    private ObjectsDatabaseSO database;
    private int selectedObjectIndex = -1;

    [SerializeField]
    float sensitivity = 10f;

    float minFov = 15f;
    float maxFov = 90f;
    void Start()
    {
        cubeExtents = GetComponent<Collider>().bounds.extents; // Get half the size of the cube
        int rand = UnityEngine.Random.Range(0, 1);
        StartPlacement(1);
    }

    public void StartPlacement(int ID)
    {
        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        OnClicked += PlaceStructure;
    }

    private void PlaceStructure()
    {
        if (!isMoving)
        {
            GameObject gameObject = Instantiate(database.objectsData[selectedObjectIndex].Prefab);
            gameObject.transform.position = transform.position;

        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClicked?.Invoke();
        }
        if (isMoving) return;

        float fov = Camera.main.fieldOfView;

        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov,minFov,maxFov);
        Camera.main.fieldOfView = fov;

        Vector3 camForward = cam.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cam.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 moveDir = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) moveDir += camForward;
        if (Input.GetKey(KeyCode.S)) moveDir -= camForward;
        if (Input.GetKey(KeyCode.D)) moveDir += camRight;
        if (Input.GetKey(KeyCode.A)) moveDir -= camRight;
        if (Input.GetKey(KeyCode.E)) moveDir = Vector3.up;
        if (Input.GetKey(KeyCode.Q)) moveDir = Vector3.down;

        if (moveDir != Vector3.zero)
        {
            moveDir = SnapDirectionToGrid(moveDir);
            StartCoroutine(MoveBlock(moveDir));
        }
    }

    private Vector3 SnapDirectionToGrid(Vector3 direction)
    {
        // Get dominant axis for movement
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
        {
            direction.z = 0;
            direction.x = Mathf.Sign(direction.x);
        }
        else
        {
            direction.x = 0;
            direction.z = Mathf.Sign(direction.z);
        }

        // Normalize Y movement
        if (direction.y != 0)
        {
            direction.y = Mathf.Sign(direction.y);
            direction.x = 0;
            direction.z = 0;
        }

        return direction;
    }

    private bool ValidTargetPos(Vector3 tarPos)
    {
        if (boundingBox == null)
        {
            Debug.LogWarning("Bounding box is not assigned!");
            return true; // Allow movement if no bounding box is set
        }

        Bounds bounds = boundingBox.bounds; // Get actual world space bounds
        // Define the cube's min and max corners in world space
        Vector3 minCorner = tarPos - cubeExtents;
        Vector3 maxCorner = tarPos + cubeExtents;

        // Check if all corners are within bounds
        return bounds.Contains(minCorner) && bounds.Contains(maxCorner);
    }

    private IEnumerator MoveBlock(Vector3 dir)
    {
        origPos = transform.position;
        targetPos = origPos + dir;

        if (!ValidTargetPos(targetPos))
        {
            yield break; // Exit if movement is not valid
        }

        isMoving = true;
        float elapsedTime = 0f;

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, elapsedTime / timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos; // Snap to final position
        isMoving = false;
    }
}
