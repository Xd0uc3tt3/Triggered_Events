using UnityEngine;

public class AmbientLight: MonoBehaviour
{
    public Light ambientLight;

    private bool hasHidden = false;

    void Start()
    {
        ambientLight = GetComponent<Light>();
    }

    void Update()
    {
        if (LightManager.LightsOut)
        {
            if (ambientLight != null)
            {
                ambientLight.enabled = false;
            }
            else
            {
                gameObject.SetActive(false);
            }
            hasHidden = true;

            enabled = false;
        }
    }
}
