using UnityEngine;
using UnityEngine.Events;

public class FinalTimer : MonoBehaviour
{
    public float timerDuration = 10f;

    public UnityEvent onTimerComplete;

    private float timer;
    private bool running = false;

    private void Update()
    {
        if (!running)
            return;

        timer += Time.deltaTime;

        if (timer >= timerDuration)
        {
            running = false;
            onTimerComplete?.Invoke();
            Debug.Log("Final ominous timer finished.");
        }
    }

    public void StartTimer()
    {
        timer = 0f;
        running = true;
        Debug.Log("Final ominous timer started.");
    }
}
