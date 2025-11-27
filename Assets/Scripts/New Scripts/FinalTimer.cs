using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class FinalTimer : MonoBehaviour
{
    public TMP_Text uiText;
    public PlayableDirector cutscene;
    public float delayBeforeCountdown = 22f;
    public float countdownDuration = 60f;

    private bool timerStarted = false;
    private bool halfwayTriggered = false;

    private void Start()
    {
        if (cutscene != null)
        {
            cutscene.stopped += OnCutsceneFinished;
        }
    }

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
        float halfwayPoint = countdownDuration / 2f;

        while (remainingTime > 0)
        {
            if (!halfwayTriggered && remainingTime <= halfwayPoint)
            {
                halfwayTriggered = true;
                LightManager.LightsOut = true;
            }

            uiText.text = "God is coming in " + Mathf.CeilToInt(remainingTime) + "s";
            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
        }

        uiText.text = "God is Here!";
        yield return new WaitForSeconds(15f);
        uiText.text = "";

        float randomDelay = Random.Range(1f, 5f);
        yield return new WaitForSeconds(randomDelay);

        if (cutscene != null)
        {
            cutscene.Play();
        }
    }

    private void OnCutsceneFinished(PlayableDirector director)
    {
        SceneManager.LoadScene("MainMenu");
    }
}

