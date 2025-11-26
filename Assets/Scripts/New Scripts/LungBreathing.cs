using UnityEngine;

public class Breathing : MonoBehaviour
{
    [Header("Breathing Settings")]
    public float inhaleDuration = 2.0f;
    public float exhaleDuration = 0.8f;
    public float intensity = 0.2f;

    private Vector3 originalScale;
    private float timer;
    private bool inhaling = true;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        BreathingCycle();
    }

    private void BreathingCycle()
    {
        if (inhaling)
        {
            timer += Time.deltaTime / inhaleDuration;

            if (timer >= 1f)
            {
                timer = 1f;
                inhaling = false;
            }
        }
        else
        {
            timer -= Time.deltaTime / exhaleDuration;

            if (timer <= 0f)
            {
                timer = 0f;
                inhaling = true;
            }
        }

        float eased = SmoothStep(timer);

        float scaleFactor = 1f + eased * intensity;
        transform.localScale = originalScale * scaleFactor;
    }

    private float SmoothStep(float t)
    {
        return t * t * (3f - 2f * t);
    }
}
