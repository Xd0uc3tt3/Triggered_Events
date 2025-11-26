using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using System.Collections;

public class BrainPickUp : MonoBehaviour
{
    public PlayableDirector brainTimeline;
    public TMP_Text uiText;
    public float messageDuration = 2f;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        if (inventory == null) return;

        if (inventory.HasPart())
        {
            ShowMessage("I can only carry one part at a time.");
            return;
        }

        inventory.PickUpPart(BodyPart.Brain);

        triggered = true;

        StartCoroutine(HandlePickupSequence());
    }

    private IEnumerator HandlePickupSequence()
    {
        ShowMessage("Picked up the brain");

        brainTimeline.Play();

        yield return new WaitForSeconds(messageDuration);

        uiText.text = "";

        Destroy(gameObject);
    }

    private void ShowMessage(string message)
    {
        uiText.text = message;
    }
}
