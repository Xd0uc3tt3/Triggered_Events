using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using System.Collections;

public class TableTrigger : MonoBehaviour
{
    public TMP_Text uiText;

    public PlayableDirector doorDirector;

    public PlayableDirector heartTimeline;
    public PlayableDirector lungsTimeline;
    public PlayableDirector brainTimeline;
    public PlayableDirector StomachTimeline;

    public PlayableDirector ominousStage5_4;
    public PlayableDirector ominousStage4_3;
    public PlayableDirector ominousStage3_2;
    public PlayableDirector ominousStage2_1;

    public FinalTimer finalTimer;

    public float messageDuration = 2f;

    private bool hasPlayed = false;
    private Coroutine hideCoroutine;

    private int addedPartsCounter = 0;

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

        if (part == BodyPart.Heart && heartTimeline != null)
        {
            heartTimeline.Play();
        }
        else if (part == BodyPart.Lungs && lungsTimeline != null)
        {
            lungsTimeline.Play();
        }
        else if (part == BodyPart.Brain && brainTimeline != null)
        {
            brainTimeline.Play();
        }
        else if (part == BodyPart.Stomach && StomachTimeline != null)
        {
            StomachTimeline.Play();
        }

        inventory.RemovePart();

        yield return new WaitForSeconds(messageDuration);

        addedPartsCounter++;

        ShowMessage("One step closer...");
        

        if (addedPartsCounter == 1 && ominousStage5_4 != null)
        {
            ominousStage5_4.Play();
        }
        else if (addedPartsCounter == 2 && ominousStage4_3 != null)
        {
            ominousStage4_3.Play();
        }
        else if (addedPartsCounter == 3 && ominousStage3_2 != null)
        {
            ominousStage3_2.Play();
        }
        else if (addedPartsCounter == 4 && ominousStage2_1 != null)
        {
            ominousStage2_1.Play();

            if (finalTimer != null)
                finalTimer.StartTimer();
        }

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
