using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class PsyManager : MonoBehaviour
{
    public GameObject Yin;
    public GameObject Yang;

    [SerializeField] float delayBeforeEverythingStart = 0.1f;
    [SerializeField] float delayBeforeSwitchingScene = 1.0f;
    [SerializeField] float delayBetweenAccouplement = 0.5f;

    [Header("Events")]
    public UnityEvent startStuff;
    public UnityEvent modeSwitch;

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(Yin);
        Assert.IsNotNull(Yang);

        //Set life
        DatabaseManager db = GameObject.FindGameObjectWithTag("Root")?.GetComponent<DatabaseManager>();
        Assert.IsNotNull(db);

        ZenTuto tuto = GetComponent<ZenTuto>();
        if ((tuto != null) && !db.data.tutoAlreadyPlayed) {
            db.data.tutoAlreadyPlayed = true;
            tuto.startTuto();
        } else
            startGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void accouplementReussi() {
        StartCoroutine(waitForEndAccoluplement());
    }

    public void startGame() {
        StartCoroutine(startTimerAndGameAfterALittleWhile());
    }

    public void changeMode() {
        Debug.Log("Mode switcheing to FPS");
        modeSwitch.Invoke();
        //change scene after a while
        GameObject.FindGameObjectWithTag("Root")?.GetComponent<SceneLoader>()?.switchSceneAfterDelay("Pewpew", delayBeforeSwitchingScene);
    }

    private IEnumerator startTimerAndGameAfterALittleWhile() {
        yield return new WaitForSeconds(delayBeforeEverythingStart);
        startStuff?.Invoke();
        Debug.Log("ZEN fully started");
    }

    private IEnumerator waitForEndAccoluplement() {
        yield return new WaitForSeconds(delayBetweenAccouplement);

        Yin.GetComponent<MoveRandomPos>()?.move();
        Yang.GetComponent<MoveRandomPos>()?.move();
        Yin.GetComponent<CanMoveClamped>().IsEnabled = true;
        Yang.GetComponent<CanMoveClamped>().IsEnabled = true;
    }
}
