using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject prefabToSpawn;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] int numberOfEnemyAtAnyTime = 5;
    int currenNumberOfEnemy = 0;
    [SerializeField] bool isEnabled = false;
    public bool IsEnabled {
        get {return isEnabled;}
        set {enableSpawn(value);}
    }

    public UnityEvent<GameObject> newSpawn;

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(prefabToSpawn);
        Assert.IsTrue(spawnPoints.Length > 0);
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
