using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Assertions;

public class ZenTuto : MonoBehaviour
{
    [SerializeField] WriteText writer;
    [SerializeField] float initialDelay = 1.0f;
    [SerializeField] string firstPhrase;
    [SerializeField] float delay1 = 1.0f;
    [SerializeField] string secondPhrase;
    [SerializeField] float delay2 = 1.0f;
    [SerializeField] string thirdPhrase;
    [SerializeField] float delay3 = 1.0f;
    [SerializeField] float delayBetweenTutoAndPlay= 1.0f;

    [SerializeField] GameObject gameArea;
    [SerializeField] GameObject [] tutorialElems;
    private bool skipWait = false;
    private bool doWait = false;

    public UnityEvent tutoFinished;

    public void startTuto() {
        Assert.IsNotNull(writer);
        doWait = false;
        skipWait = false;
        StartCoroutine(tutoing());
    }

    private IEnumerator tutoing() {
        yield return StartCoroutine(doCountdown(initialDelay));
        writer.writeText(firstPhrase);

        yield return StartCoroutine(doCountdown(delay1));
        writer.writeText(secondPhrase);

        yield return StartCoroutine(doCountdown(delay2));
        writer.writeText(thirdPhrase);

        yield return StartCoroutine(doCountdown(delay3));
        gameArea.SetActive(true);
        foreach (GameObject item in tutorialElems)
        {
            item.SetActive(true);
        }

        yield return StartCoroutine(doCountdown(delayBetweenTutoAndPlay));
        tutoFinished?.Invoke();
    }

    public void skip() {
        if (doWait)
            skipWait = true;
    }

    private IEnumerator doCountdown(float delay) {
        doWait = true;
        float timeToWait = Time.time + delay;
        while(Time.time < timeToWait)
        {
            if(skipWait)
            {
                skipWait = false;
                doWait = false;
                yield break ;
            }
            yield return null ;
        }
        doWait = false;
    }
}
