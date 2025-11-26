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
    public PlayableDirector armTimeline;
    public PlayableDirector legTimeline;
    public PlayableDirector headTimeline;

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
        yield return new WaitForSeconds(messageDuration);

        switch (part)
        {
            case BodyPart.Heart:
                if (heartTimeline != null) heartTimeline.Play();
                break;

            case BodyPart.Part2:
                if (armTimeline != null) armTimeline.Play();
                break;

            case BodyPart.Part3:
                if (legTimeline != null) legTimeline.Play();
                break;

            case BodyPart.Part4:
                if (headTimeline != null) headTimeline.Play();
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

