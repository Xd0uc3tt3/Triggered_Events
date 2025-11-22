using UnityEngine;

public class EyeLights : MonoBehaviour
{
    Light eyeLight;

    void Start()
    {
        eyeLight = GetComponent<Light>();
    }

    void Update()
    {
        eyeLight.enabled = LightManager.eyeLights;
    }
}
