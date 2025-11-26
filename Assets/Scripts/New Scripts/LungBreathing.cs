using UnityEngine;

public class LungBreathing : MonoBehaviour
{
    [Header("Breathing Settings")]
    public float breathingSpeed = 0.5f;
    public float breathingIntensity = 0.15f;

    private Vector3 originalScale;
    private float timer = 0f;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        BreathingVisual();
    }

    private void BreathingVisual()
    {
        timer += Time.deltaTime * breathingSpeed;

        float cycle = (Mathf.Sin(timer) + 1f) * 0.5f;

        float scaleFactor = 1f + cycle * breathingIntensity;
        transform.localScale = originalScale * scaleFactor;
    }
}
