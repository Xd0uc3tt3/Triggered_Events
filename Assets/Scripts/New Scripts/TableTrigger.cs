using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using System.Collections;

public class TableTrigger : MonoBehaviour
{
    public TMP_Text uiText;
    public PlayableDirector doorDirector;
    public float messageDuration = 2f;

    private bool hasPlayed = false;
    private Coroutine hideCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (!hasPlayed)
        {
            StartCoroutine(FirstInteractionSequence());
        }
        else
        {
            StartCoroutine(SecondInteractionSequence());
        }
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

    private IEnumerator SecondInteractionSequence()
    {
        ShowMessage("I don't have any parts");
        yield return new WaitForSeconds(messageDuration);

        ShowMessage("I should look around to find some parts");
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
