using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventAfterDelay : MonoBehaviour
{
    public float delay = 1.0f;
    public UnityEvent eventFired;

    public void doDelay() {
        StartCoroutine(startWait());
    }

    private IEnumerator startWait() {
        yield return new WaitForSeconds(delay);
        eventFired?.Invoke();
    }
}
