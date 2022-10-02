using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class MainManager : MonoBehaviour
{

    [Header("General settings")]
    [SerializeField] protected GameObject player;
    [SerializeField] float delayBeforeEverythingStart = 1.0f;
    [SerializeField] float delayBeforeEverythingStartFirsTime = 5.0f;
    [SerializeField] float delayBeforeSwitchingScene = 1.0f;

    public GameObject Player {
        get {return player;}
    }

    [Header("Events")]
    public UnityEvent defeat;
    public UnityEvent startStuff;
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

        PlayerData data = GameObject.FindGameObjectWithTag("Root")?.GetComponent<DatabaseManager>()?.data;
        if ((data != null) && data.firstTimeInFPS) {
            delayBeforeEverythingStart = delayBeforeEverythingStartFirsTime;
            data.firstTimeInFPS = false;
        }

        StartCoroutine(startTimerAndGameAfterALittleWhile());
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
            Time.timeScale = 0;
            defeat.Invoke();
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

    public void changeMode() {
        Debug.Log("Mode switcheing to ZEN");
        modeSwitch.Invoke();
        //change scene after a while
        GameObject.FindGameObjectWithTag("Root")?.GetComponent<SceneLoader>()?.switchSceneAfterDelay("Psy", delayBeforeSwitchingScene);
    }

    private IEnumerator startTimerAndGameAfterALittleWhile() {
        yield return new WaitForSeconds(delayBeforeEverythingStart);
        startStuff?.Invoke();
        Debug.Log("FPS fully started");
    }
}