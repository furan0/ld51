using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CanDie : MonoBehaviour
{
    [SerializeField] protected bool isAlive = true;
    public bool IsAlive {
        get { return isAlive; }
    }

    [Header("Events")]
    public string fxAlias = "DIE";
    public UnityEvent objectKilledEvent;
    public UnityEvent<string> killedFx;

    ///FIX///
    Collider2D col;

    void Start() {
        col = GetComponent<Collider2D>();
    }

    public void kill() {
        if(!isAlive)
            return;
        //BroadcastMessage("disableLogic");

        objectKilledEvent.Invoke();
        
        //Deactivate tagged components
        DisbaledOnDeath[] toDisable = GetComponentsInChildren<DisbaledOnDeath>();
        foreach (DisbaledOnDeath component in toDisable)
        {
            component.enabled = false;
        }

        isAlive = false;
        killedFx.Invoke(fxAlias);
    }

    private void FixedUpdate() {
        ///FIX///
        if(col != null)
            col.isTrigger = !isAlive;
    }
}
