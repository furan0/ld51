using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** This class tries to kill things by casting a raycast in a particular direction */
public class CanKillWithRaycast : MonoBehaviour
{
    [Header("Self-destruct")]
    [SerializeField] protected bool selfDestruct = true;
    [SerializeField] protected float selfDestructDelay = 10f;
    private float selfDestructTime = 0.0f;
    [Header("Raycast")]
    [SerializeField] protected bool doRaycastOnStart = true;
    [SerializeField] protected float raycastRadius = 0.5f;
    [SerializeField] protected float raycastlength = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        selfDestructTime = Time.time + selfDestructDelay;

        if (doRaycastOnStart) {
            // Do a forward raycast at start if required
            doRaycast(transform.forward);
        }
    }

    // Update is called once per frame
    void Update() {
        if (selfDestruct && (Time.time > selfDestructTime))
            Destroy(gameObject);
    }

    public void doRaycast(Vector3 dir) {

    }
}
