
using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

//MOAR PEWPEW !
public class CanDoAwesomeFX : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] protected Transform spawnPoint;
    [SerializeField] protected Vector3 spawnDir = Vector3.zero;
    [SerializeField] protected float stayAliveDuration = 5.0f;
    private FxDatabase database;

    private List<GameObject> ongoingFxs;

    // Start is called before the first frame update
    void Start()
    {
        if (spawnPoint == null)
            spawnPoint = transform;
        database = GameObject.FindGameObjectWithTag("GameController")?.GetComponent<DatabaseManager>()?.fxDatabase;
        Assert.IsNotNull(database);
        ongoingFxs = new List<GameObject>();
    }

    private void spawnFx(GameObject fx_, Vector3 spawnPoint_, Vector3 dir_, float duration_)
    {
        GameObject obj = Instantiate(fx_, spawnPoint_, Quaternion.Euler(dir_));
        //obj.transform.eulerAngles = dir_;
        Destroy(obj, duration_);
    }

    IEnumerator spawnAfterDelay(GameObject fx_, Vector3 spawnPoint_, Vector3 dir_, float duration_, float delay_) {
        yield return new WaitForSeconds(delay_);
        spawnFx(fx_, spawnPoint_, dir_, duration_);
    }

    public void stopOngoingFxs() {
        foreach (GameObject fx in ongoingFxs)
        {
            Destroy(fx);
        }
        ongoingFxs.Clear();
    }

    public void doFx(string fxAlias_) {
        spawnFx(database.getFx(fxAlias_), spawnPoint.position, spawnDir, stayAliveDuration);
    }

    public void doFxAndKeepItAlive(string fxAlias_) {
        ongoingFxs.Add(Instantiate(database.getFx(fxAlias_), spawnPoint.position, Quaternion.Euler(spawnDir)));
    }

    public void doFxAfterDelay(float delay_, string fxAlias_) {
        StartCoroutine(spawnAfterDelay(database.getFx(fxAlias_), spawnPoint.position, spawnDir, stayAliveDuration, delay_));
    }

    public void doFx(Vector3 spawnPos_, string fxAlias_) {
        spawnFx(database.getFx(fxAlias_), spawnPos_, spawnDir, stayAliveDuration);
    }

    public void doFx(Vector3 spawnPos_, Vector3 spawnDir_, string fxAlias_) {
        spawnFx(database.getFx(fxAlias_), spawnPos_, spawnDir_, stayAliveDuration);
    }

    public void doFx(float duration_, string fxAlias_) {
        spawnFx(database.getFx(fxAlias_), spawnPoint.position, spawnDir, duration_);
    }

    public void doFx(Vector3 spawnPos_, float duration_, string fxAlias_) {
        spawnFx(database.getFx(fxAlias_), spawnPos_, spawnDir, duration_);
    }

    public void doFx(Vector3 spawnPos_, Vector3 spawnDir_, float duration_, string fxAlias_) {
        spawnFx(database.getFx(fxAlias_), spawnPos_, spawnDir_, duration_);
    }
}
