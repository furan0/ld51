using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{

    [SerializeField] int killPoint = 0;
    [SerializeField] int meditationPoint = 0;
    [SerializeField] int meditationPointBONUS = 0;
    public UnityEvent<int, bool> changeScore;
    public UnityEvent newHighScore;

    private PlayerData data;
    private bool specialMode = false;
    private int currentHighScore;

    void Awake() {
        data = GameObject.FindGameObjectWithTag("Root")?.GetComponent<DatabaseManager>()?.data;
        Assert.IsNotNull(data);
        getHighScore();
    }

    void addKill() {
        updateScore(killPoint);
    }

    public void addMeditation(){
        int pointToAdd = (specialMode)? meditationPointBONUS : meditationPoint;
        updateScore(pointToAdd);
    }

    public void newEnnemy(GameObject obj) {
        obj.GetComponent<CanDie>()?.objectKilledEvent.AddListener(addKill);
    }


    public int getScore() {
        return data.score;
    }

    public bool getBonus() {
        return specialMode;
    }

    private int getHighScore() {
        currentHighScore = PlayerPrefs.GetInt("score");
        return currentHighScore;
    }

    public void saveScore() {
        int lastScore = PlayerPrefs.GetInt("score");
        if (lastScore < data.score)
            PlayerPrefs.SetInt("score", data.score);
    }

    public bool isHighScore() {
        return (data.score > currentHighScore);
    }

    public void lifeFull() {
        specialMode = true;
    }

    public void lifeNotFull() {
        specialMode = false;
    }

    void updateScore(int pointToAdd) {
        data.score += pointToAdd;
        changeScore?.Invoke(pointToAdd, specialMode);

        if (isHighScore())
            newHighScore?.Invoke();
    }
}
