using UnityEngine;
using UnityEngine.Playables;

public class HeartTrigger : MonoBehaviour
{
    public PlayableDirector heartTimeline;
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;

        if (!other.CompareTag("Player"))
            return;

        triggered = true;

        heartTimeline.Play();

        Destroy(gameObject);
    }
}
