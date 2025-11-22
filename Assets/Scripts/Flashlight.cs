using UnityEngine;

public class PlayerFlashlight : MonoBehaviour
{
    public Light flashlight;
    public float delay = 1f;

    bool currentLightsOutState;
    float timer;
    bool waiting;

    void Start()
    {
        if (flashlight == null)
            flashlight = GetComponent<Light>();

        currentLightsOutState = LightManager.LightsOut;
        flashlight.enabled = currentLightsOutState;
    }

    void Update()
    {
        if (LightManager.LightsOut != currentLightsOutState && !waiting)
        {
            waiting = true;
            timer = Time.time + delay;
        }

        if (waiting && Time.time >= timer)
        {
            currentLightsOutState = LightManager.LightsOut;
            flashlight.enabled = currentLightsOutState;
            waiting = false;
        }
    }
}

