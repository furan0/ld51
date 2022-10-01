using UnityEngine;
using UnityEngine.Events;

public class FireEventWhenEnabled : MonoBehaviour
{
    public UnityEvent wasEnabled; //params : position & direction & fxAlias of pewpew fired

    void OnEnable()
    {
        wasEnabled?.Invoke();
    }
}
