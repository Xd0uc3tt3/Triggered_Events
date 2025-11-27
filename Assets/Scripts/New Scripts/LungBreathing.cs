using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LungBreathing : MonoBehaviour
{
    public float inhaleDuration = 2.0f;
    public float exhaleDuration = 0.8f;
    public float intensity = 0.2f;

    public AudioClip breathingClip;
    private AudioSource audioSource;

    private Vector3 originalScale;
    private float timer;
    private bool inhaling = true;

    private void Start()
    {
        originalScale = transform.localScale;

        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = breathingClip;
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
                PlayBreathingSound();
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

    private void PlayBreathingSound()
    {
        if (breathingClip == null)
            return;

        audioSource.pitch = Random.Range(0.95f, 1.05f);

        audioSource.PlayOneShot(breathingClip);
    }

    private float SmoothStep(float t)
    {
        return t * t * (3f - 2f * t);
    }
}

