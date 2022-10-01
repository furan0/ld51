using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ScoreManager : MonoBehaviour
{

    [SerializeField] int killPoint = 0;
    public int score = 0;
    public UnityEvent changeScore;
    // Start is called before the first frame update
    void Start()
    {
        MainManager manager = GetComponent<MainManager>();

        // TODO

        /*foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Spawner"))
        {
            obj.GetComponent<CanSpawnCrap>()?.newSpawnEvent.AddListener(newEnnemy);
        }*/
    }

    void addKill(){
        score += killPoint;
        changeScore?.Invoke();
    }

    /*void addSpawner(CanSpawnCrap spawner) {
        spawner.newSpawnEvent.AddListener(newEnnemy);
    }*/

    void newEnnemy(GameObject obj) {
        Debug.Log("New ennemy added");
        obj.GetComponent<CanDie>()?.objectKilledEvent.AddListener(addKill);
    }

    public int getScore() {
        return score;
    }

    public int getHighScore() {
        return PlayerPrefs.GetInt("score");
    }

    public void saveScore() {
        int lastScore = PlayerPrefs.GetInt("score");
        if (lastScore < score)
            PlayerPrefs.SetInt("score", score);
    }
}
