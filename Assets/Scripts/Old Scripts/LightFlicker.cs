using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public float minIntensity = 0.2f;
    public float maxIntensity = 1.5f;
    public float flickerIntervalMin = 0.01f;
    public float flickerIntervalMax = 0.15f;

    private Light flickerLight;
    private float baseIntensity;
    private float nextFlickerTime;

    void Start()
    {
        flickerLight = GetComponent<Light>();
        baseIntensity = flickerLight.intensity;
        nextFlickerTime = Time.time + Random.Range(flickerIntervalMin, flickerIntervalMax);
    }

    void Update()
    {
        if (LightManager.LightsOut)
        {
            flickerLight.enabled = false;
            return;
        }

        flickerLight.enabled = true;

        if (Time.time >= nextFlickerTime)
        {
            flickerLight.intensity = baseIntensity * Random.Range(minIntensity, maxIntensity);
            nextFlickerTime = Time.time + Random.Range(flickerIntervalMin, flickerIntervalMax);
        }
    }
}


