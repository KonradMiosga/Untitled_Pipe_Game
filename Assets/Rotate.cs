using UnityEngine;

public class Rotate : MonoBehaviour
{
    int rotX;
    int rotY;
    int rotZ;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotX = UnityEngine.Random.Range(0,4);
        rotY = UnityEngine.Random.Range(0,4);
        rotZ = UnityEngine.Random.Range(0,4);

        transform.Rotate(rotX, rotY, rotZ);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
