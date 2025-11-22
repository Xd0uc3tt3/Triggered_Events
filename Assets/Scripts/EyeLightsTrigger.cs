using UnityEngine;

public class EyeLightsTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LightManager.eyeLights = false;
        }
    }
}
