using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using System.Collections;

public class FinalTimer : MonoBehaviour
{
    public TMP_Text uiText;
    public PlayableDirector cutscene;
    public float delayBeforeCountdown = 22f;
    public float countdownDuration = 30f;

    private bool timerStarted = false;

    public void StartTimer()
    {
        if (!timerStarted)
        {
            timerStarted = true;
            StartCoroutine(TimerSequence());
        }
    }

    private IEnumerator TimerSequence()
    {
        yield return new WaitForSeconds(delayBeforeCountdown);

        float remainingTime = countdownDuration;
        while (remainingTime > 0)
        {
            uiText.text = "God is coming in " + Mathf.CeilToInt(remainingTime) + "s";
            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
        }

        uiText.text = "God is coming!";
        yield return new WaitForSeconds(2f);
        uiText.text = "";

        float randomDelay = Random.Range(1f, 5f);
        yield return new WaitForSeconds(randomDelay);

        if (cutscene != null)
        {
            cutscene.Play();
        }
    }
}
