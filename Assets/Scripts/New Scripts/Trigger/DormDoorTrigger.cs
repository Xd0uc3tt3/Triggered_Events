using UnityEngine;
using UnityEngine.Playables;

public class DormDoorTrigger : MonoBehaviour
{
    public PlayableDirector animationDirector;
    public bool destroyAfterTrigger = true;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;

        if (!other.CompareTag("Player"))
            return;

        triggered = true;

        if (animationDirector != null)
            animationDirector.Play();

        if (destroyAfterTrigger)
        {
            Destroy(gameObject);
        }
        else
        {
            Collider col = GetComponent<Collider>();
            if (col != null)
                col.enabled = false;
        }
    }
}
