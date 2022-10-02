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

    public UnityEvent tutoFinished;

    public void startTuto() {
        Assert.IsNotNull(writer);
        StartCoroutine(tutoing());
    }

    private IEnumerator tutoing() {
        yield return new WaitForSeconds(initialDelay);
        writer.writeText(firstPhrase);

        yield return new WaitForSeconds(delay1);
        writer.writeText(secondPhrase);

        yield return new WaitForSeconds(delay2);
        writer.writeText(thirdPhrase);

        yield return new WaitForSeconds(delay3);
        gameArea.SetActive(true);
        foreach (GameObject item in tutorialElems)
        {
            item.SetActive(true);
        }

        yield return new WaitForSeconds(delayBetweenTutoAndPlay);
        tutoFinished?.Invoke();
    }
}
