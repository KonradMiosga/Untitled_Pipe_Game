using UnityEngine;
using UnityEngine.InputSystem;

public class ZoomController : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] private Camera cam;

    [SerializeField] private float zoomSpeed = 1000f;
    [SerializeField] private float minDistance = 3f;
    [SerializeField] private float maxDistance = 20f;

    private float scrollInput;
    private Vector3 initialOffset;

    void Awake()
    {
        playerController = new PlayerController();

        playerController.Gameplay.Zoom.performed += ctx => scrollInput = ctx.ReadValue<float>();
        playerController.Gameplay.Zoom.canceled += _ => scrollInput = 0;
    }

    void OnEnable() => playerController.Enable();
    void OnDisable() => playerController.Disable();

    void Start()
    {
        // Get initial distance from camera to the focus point (e.g., player or origin)
        initialOffset = cam.transform.position - transform.position;
    }

    void Update()
    {
        if (scrollInput != 0f)
        {
            Vector3 camDir = cam.transform.forward;
            Vector3 newPos = cam.transform.position + camDir * scrollInput;

            float newDistance = Vector3.Distance(newPos, transform.position);
            if (newDistance >= minDistance && newDistance <= maxDistance)
            {
                cam.transform.position = newPos;
            }
        }
    }
}
