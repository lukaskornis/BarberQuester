using UnityEngine;
using UnityEngine.XR;

public class PerformanceManager : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 120;
        XRSettings.eyeTextureResolutionScale = 1.7f;
    }
}
