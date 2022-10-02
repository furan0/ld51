using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ZenTuto : MonoBehaviour
{
    [SerializeField] WriteText writer;
    [SerializeField] float initialDelay = 1.0f;
    [SerializeField] string firstPhrase;
    [SerializeField] float delay1 = 1.0f;
    [SerializeField] string secondPhrase;

    public void startTuto() {
        Assert.IsNotNull(writer);
    }
}
