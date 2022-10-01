using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class MainManager : MonoBehaviour
{

    [Header("General settings")]
    [SerializeField] protected GameObject player;

    public GameObject Player {
        get {return player;}
    }

    [Header("Events")]
    public UnityEvent defeat;
    public UnityEvent modeSwitch;

    [SerializeField] private bool isDefeat = false;
    public bool IsDefeat {
        get {return isDefeat; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Register to various player events
        player.GetComponent<CanDie>()?.objectKilledEvent.AddListener(() => {
            signalPlayerDeath();
        });
    }

    // Update is called once per frame
    void Update()
    {
        //Check pour éviter les conneries !!
        bool isPlayerAlive = player.GetComponent<CanDie>()?.IsAlive ?? true;

        if (!isPlayerAlive) {
            //DEFEAT !
            doDefeat();
        }
    }

    private void doDefeat() {
        if (!isDefeat) {
            //Debug.Log("Defeat !");
            isDefeat = true;
            defeat.Invoke();
            Time.timeScale = 0;
        }
    }

    private void signalPlayerDeath() {
        Debug.Log("Player was killed. Noobz !");

        //DEFEAT...
        doDefeat();
    }

    // ==== Debug functions ====
    public void killCurrentPlayer() {
        player.GetComponent<CanDie>()?.kill();
    }
}