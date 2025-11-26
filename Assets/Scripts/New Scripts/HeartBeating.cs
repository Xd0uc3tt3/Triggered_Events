using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HeartBeating : MonoBehaviour
{
    [Header("Pulse Settings")]
    public float pulseSpeed = 1.5f;
    public float pulseIntensity = 0.2f;

    [Header("Heartbeat Timing")]
    public float beatInterval = 0.9f;
    public float secondBeatDelay = 0.2f;

    [Header("Heartbeat Sound")]
    public AudioClip heartbeatClip;
    private AudioSource audioSource;

    private Vector3 originalScale;
    private float timer;
    private float beatTimer;

    private void Start()
    {
        originalScale = transform.localScale;

        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = heartbeatClip;
    }

    private void Update()
    {
        PulseVisual();
        PulseAudio();
    }

    private void PulseVisual()
    {
        timer += Time.deltaTime * pulseSpeed;

        float scaleFactor = Mathf.Abs(Mathf.Sin(timer))
                          + (Mathf.Abs(Mathf.Sin(timer * 0.5f)) * 0.3f);

        float finalScale = 1f + scaleFactor * pulseIntensity;
        transform.localScale = originalScale * finalScale;
    }

    private void PulseAudio()
    {
        beatTimer += Time.deltaTime;

        if (beatTimer >= beatInterval)
        {
            PlayBeat();

            Invoke(nameof(PlayBeat), secondBeatDelay);

            beatTimer = 0f;
        }
    }

    private void PlayBeat()
    {
        if (heartbeatClip != null)
            audioSource.PlayOneShot(heartbeatClip);
    }
}
