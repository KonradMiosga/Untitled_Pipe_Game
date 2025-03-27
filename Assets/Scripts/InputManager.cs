using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public Vector3 MoveDirection { get; private set; }
    public bool Clicked { get; private set; }
    public float ScrollDelta { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        HandleMovementInput();
        HandleMouseInput();
    }

    private void HandleMovementInput()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) direction += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) direction += Vector3.back;
        if (Input.GetKey(KeyCode.D)) direction += Vector3.right;
        if (Input.GetKey(KeyCode.A)) direction += Vector3.left;
        if (Input.GetKey(KeyCode.E)) direction += Vector3.up;
        if (Input.GetKey(KeyCode.Q)) direction += Vector3.down;

        MoveDirection = direction;
    }

    private void HandleMouseInput()
    {
        Clicked = Input.GetMouseButtonDown(0);
        ScrollDelta = Input.GetAxis("Mouse ScrollWheel");
    }
}
