using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CanAcoouple : MonoBehaviour
{
    public UnityEvent accouplement;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            // We have an accouplement !
            accouplement?.Invoke();
        }
    }
}
