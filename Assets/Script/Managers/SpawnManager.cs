using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject prefabToSpawn;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] int minNumberOfEnnemy = 2;
    [SerializeField] int maxNumberOfEnnemy = 7;
    [SerializeField, Range(0.1f, 2f), Tooltip("Multiply the number of time in FPS by this number to determine the numùber of ennemy to spawn, clamped")] 
    float numberOfEnemyMultiplicator = 0.5f;
    private int numberOfEnemyAtAnyTime = 0;
    private int currenNumberOfEnemy = 0;
    [SerializeField] bool isEnabled = false;
    public bool IsEnabled {
        get {return isEnabled;}
        set {enableSpawn(value);}
    }

    public UnityEvent<GameObject> newSpawn;

    private PlayerData data;

    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.FindGameObjectWithTag("Root")?.GetComponent<DatabaseManager>()?.data;
        Assert.IsNotNull(prefabToSpawn);
        Assert.IsTrue(spawnPoints.Length > 0);

        if (data != null) {
            numberOfEnemyAtAnyTime = minNumberOfEnnemy + Mathf.FloorToInt(data.nbTimeInFPS * numberOfEnemyMultiplicator);
            numberOfEnemyAtAnyTime = Mathf.Clamp(numberOfEnemyAtAnyTime, minNumberOfEnnemy, maxNumberOfEnnemy);
        } else {
            numberOfEnemyAtAnyTime = Random.Range(minNumberOfEnnemy, maxNumberOfEnnemy+1);
        }
    }

    void onEnemyDeath() {
        currenNumberOfEnemy--;
        // Keep the number of ennemy identical
        SpawnNewEnemy();
    }

    void SpawnNewEnemy() {
        if (!isEnabled)
            return;

        int rng = Random.Range(0, spawnPoints.Length);
        GameObject obj = GameObject.Instantiate(prefabToSpawn, spawnPoints[rng].transform.position, Quaternion.identity);
        obj.GetComponent<CanDie>()?.objectKilledEvent.AddListener(onEnemyDeath);
        currenNumberOfEnemy++;
        newSpawn?.Invoke(obj);
    }

    public void enableSpawn(bool status) {
        isEnabled = status;
        if (isEnabled) {
            while(currenNumberOfEnemy < numberOfEnemyAtAnyTime) {
                SpawnNewEnemy();
            }
        }
    }

    public void disableSpawn() {
        isEnabled = false;
    }
}
