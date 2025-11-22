using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LightManager.LightsOut = true;
        }
    }
}

