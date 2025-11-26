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
            StartCoroutine(FirstMessageThenDoor());
        }
        else
        {
            ShowMessage("I don't have any parts");
        }
    }

    private IEnumerator FirstMessageThenDoor()
    {
        ShowMessage("I must rebuild him");

        yield return new WaitForSeconds(messageDuration);

        doorDirector.Play();
        hasPlayed = true;
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
