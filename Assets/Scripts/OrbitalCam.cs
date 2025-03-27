using UnityEngine;

public class OrbitalCam : MonoBehaviour
{
    [SerializeField] float MouseSpeed = 3;
    [SerializeField] float orbitDamping = 10;

    Vector3 localRot;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            localRot.x += Input.GetAxis("Mouse X") * MouseSpeed;
            localRot.y -= Input.GetAxis("Mouse Y") * MouseSpeed;

            localRot.y = Mathf.Clamp(localRot.y, -90f, 90f);

            Quaternion QT = Quaternion.Euler(localRot.y, localRot.x, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, QT, Time.deltaTime * orbitDamping);
        }
    }
}
