using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject following;

    void LateUpdate()
    {
        transform.position = following.transform.position;
    }
}
