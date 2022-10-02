using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FreudDeath : MonoBehaviour
{
    [SerializeField] GameObject thingToDestroyAtTheEnd;
    [SerializeField] float delayInitial = 0.01f;
    [SerializeField] Transform spawner1;
    [SerializeField] float scale1 = 1.0f;
    [SerializeField] float delayBetween1And2 = 0.02f;
    [SerializeField] Transform spawner2;
    [SerializeField] float scale2 = 0.7f;
    [SerializeField] float delayBetween2And3 = 0.02f;
    [SerializeField] Transform spawner3;
    [SerializeField] float scale3 = 1.2f;
    [SerializeField] float finalDelay = 0.01f;
    [SerializeField] float autoDestructTime = 0.5f;
    [SerializeField] string fxAlias;
    [SerializeField] string soundAlias;

    private FxDatabase fxs;

    // Start is called before the first frame update
    void Start()
    {
        fxs =  GameObject.FindGameObjectWithTag("Root")?.GetComponent<DatabaseManager>()?.fxDatabase;

        Assert.IsNotNull(spawner1);
        Assert.IsNotNull(spawner2);
        Assert.IsNotNull(spawner3);
    }


    void OnEnable() {
        StartCoroutine(doDeath());
    }

    IEnumerator doDeath() {
        // Sound
        CanPlaySound soundPlayer = GetComponent<CanPlaySound>();
        soundPlayer?.playSound(soundAlias);

        // 1
        yield return new WaitForSeconds(delayInitial);
        GameObject obj = GameObject.Instantiate(fxs.getFx(fxAlias), spawner1.position, spawner1.rotation);
        obj.transform.localScale *= scale1;
        Destroy(obj, autoDestructTime);

        // 2
        yield return new WaitForSeconds(delayBetween1And2);
        obj = GameObject.Instantiate(fxs.getFx(fxAlias), spawner2.position, spawner1.rotation);
        obj.transform.localScale *= scale2;
        Destroy(obj, autoDestructTime);

        // 3
        yield return new WaitForSeconds(delayBetween2And3);
        obj = GameObject.Instantiate(fxs.getFx(fxAlias), spawner3.position, spawner1.rotation);
        obj.transform.localScale *= scale3;
        Destroy(obj, autoDestructTime);

        yield return new WaitForSeconds(finalDelay);
        if (thingToDestroyAtTheEnd != null)
        Destroy(thingToDestroyAtTheEnd);
    }
}
