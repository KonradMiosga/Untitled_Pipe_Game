using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 moveInput;
    public bool isMoving;
    private PlayerController controls;

    [SerializeField] Transform cam;
    [SerializeField] float moveTime = 0.1f;
    [SerializeField] float holdDelay = 0.3f;

    //private bool isHoldingKey = false;
    private Coroutine moveLoopCoroutine;

    void Awake()
    {
        controls = new PlayerController();

        controls.Gameplay.Move.performed += ctx =>
        {
            moveInput = ctx.ReadValue<Vector3>();
            //isHoldingKey = true;

            if (moveLoopCoroutine == null)
                moveLoopCoroutine = StartCoroutine(HandleHoldMovement());
        };

        controls.Gameplay.Move.canceled += ctx =>
        {
            //isHoldingKey = false;
        };
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    private IEnumerator HandleHoldMovement()
    {
        yield return TryMoveOnce();

        yield return new WaitForSeconds(holdDelay);

        while (GetWorldDirection(moveInput) != Vector3.zero)
        {
            yield return TryMoveOnce();
        }

        moveLoopCoroutine = null;
    }

    private IEnumerator TryMoveOnce()
    {
        if (isMoving || moveInput == Vector3.zero)
            yield break;

        Vector3 inputDir = GetWorldDirection(moveInput);
        if (inputDir == Vector3.zero)
            yield break;

        Vector3 snappedDir = SnapDirectionToGrid(inputDir);
        Vector3 nextPos = transform.position + snappedDir;
        yield return StartCoroutine(MoveToPosition(nextPos));
    }

    private Vector3 GetWorldDirection(Vector3 input)
    {
        if (input == Vector3.zero) return Vector3.zero;

        Vector3 camForward = cam.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cam.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 horizontal = camForward * input.z + camRight * input.x;
        Vector3 vertical = Vector3.up * input.y;

        return horizontal + vertical;
    }

    private Vector3 SnapDirectionToGrid(Vector3 dir)
    {
        if (Mathf.Abs(dir.y) >= Mathf.Abs(dir.x) && Mathf.Abs(dir.y) >= Mathf.Abs(dir.z))
        {
            return Vector3.up * Mathf.Sign(dir.y);
        }
        else if (Mathf.Abs(dir.x) >= Mathf.Abs(dir.z))
        {
            return Vector3.right * Mathf.Sign(dir.x);
        }
        else
        {
            return Vector3.forward * Mathf.Sign(dir.z);
        }
    }


    private IEnumerator MoveToPosition(Vector3 target)
    {
        isMoving = true;

        Vector3 start = transform.position;
        float elapsed = 0f;

        while (elapsed < moveTime)
        {
            float t = elapsed / moveTime;
            t = Mathf.SmoothStep(0f, 1f, t);
            transform.position = Vector3.Lerp(start, target, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = target;
        isMoving = false;
    }
}
