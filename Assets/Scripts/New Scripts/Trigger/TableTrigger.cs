using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using System.Collections;

public class TableTrigger : MonoBehaviour
{
    public TMP_Text uiText;

    // Door animation
    public PlayableDirector doorDirector;

    // Part Animations
    public PlayableDirector heartTimeline;
    public PlayableDirector LungsTimeline;
    public PlayableDirector BrainTimeline;
    public PlayableDirector RibsTimeline;

    public float messageDuration = 2f;

    private bool hasPlayed = false;
    private Coroutine hideCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        if (inventory == null)
            return;

        if (!hasPlayed)
        {
            StartCoroutine(FirstInteractionSequence());
            return;
        }

        if (!inventory.HasPart())
        {
            StartCoroutine(NoPartSequence());
            return;
        }

        StartCoroutine(PlacePartSequence(inventory));
    }

    private IEnumerator FirstInteractionSequence()
    {
        ShowMessage("I must rebuild him");
        yield return new WaitForSeconds(messageDuration);

        doorDirector.Play();
        hasPlayed = true;

        ShowMessage("There must be some parts lying around");
        yield return new WaitForSeconds(messageDuration);

        HideMessage();
    }

    private IEnumerator NoPartSequence()
    {
        ShowMessage("I don't have any parts");
        yield return new WaitForSeconds(messageDuration);

        ShowMessage("I should look around to find some parts");
        yield return new WaitForSeconds(messageDuration);

        HideMessage();
    }

    private IEnumerator PlacePartSequence(PlayerInventory inventory)
    {
        BodyPart part = inventory.currentPart;

        ShowMessage("Placing " + part.ToString().ToLower() + "...");

        switch (part)
        {
            case BodyPart.Heart:
                if (heartTimeline != null) heartTimeline.Play();
                break;

            case BodyPart.Lungs:
                if (LungsTimeline != null) LungsTimeline.Play();
                break;

            case BodyPart.Brain:
                if (BrainTimeline != null) BrainTimeline.Play();
                break;

            case BodyPart.Ribs:
                if (RibsTimeline != null) RibsTimeline.Play();
                break;
        }

        inventory.RemovePart();

        ShowMessage("One step closer...");
        yield return new WaitForSeconds(messageDuration);

        HideMessage();
    }

    private void ShowMessage(string message)
    {
        uiText.text = message;

        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);

        hideCoroutine = StartCoroutine(HideMessageAfterDelay());
    }

    private void HideMessage()
    {
        uiText.text = "";
    }

    private IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(messageDuration);
        uiText.text = "";
    }
}

