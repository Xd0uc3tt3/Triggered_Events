using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using System.Collections;

public class HeartTrigger : MonoBehaviour
{
    public PlayableDirector heartTimeline;

    public TMP_Text uiText;
    public float messageDuration = 2f;
    private Coroutine hideCoroutine;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;

        if (!other.CompareTag("Player"))
            return;

        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        if (inventory == null)
            return;

        if (inventory.HasPart())
        {
            ShowMessage("I can only carry one part at a time.");
            return;
        }

        inventory.PickUpPart(BodyPart.Heart);
        ShowMessage("Picked up the heart");

        triggered = true;

        heartTimeline.Play();

        //destroys the trigger object not the heart, i had thought otherwise at first
        Destroy(gameObject);
    }

    private void ShowMessage(string message)
    {
        uiText.text = message;

        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);

        hideCoroutine = StartCoroutine(HideMessageAfterDelay());
    }

    private IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(messageDuration);
        uiText.text = "";
    }
}

