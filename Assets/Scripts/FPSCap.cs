using UnityEngine;

public class FPSCap : MonoBehaviour
{
    [SerializeField] private int frameRate = 60;

    private void Awake()
    {
#if UNITY_EDITOR
        QualitySettings.vSyncCount = 0; // Disable VSync
        Application.targetFrameRate = -1; // Reset first
        Application.targetFrameRate = frameRate;
#endif
    }

    void Update()
    {
        Debug.Log(1f / Time.deltaTime);
    }
}
