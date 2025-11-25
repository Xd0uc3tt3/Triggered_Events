using UnityEngine;

public class DimLights : MonoBehaviour
{
    Light dimLight;

    void Start()
    {
        dimLight = GetComponent<Light>();
    }

    void Update()
    {
        dimLight.enabled = !LightManager.LightsOut;
    }
}
