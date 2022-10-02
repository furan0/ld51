using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using TMPro;

public class DisplayScoreSimple : MonoBehaviour
{
    public TextMeshProUGUI textScore;
    public Color colorHighscore = new Color(255, 68, 68);
    ScoreManager manager;

    void Awake() {
        manager = GameObject.FindGameObjectWithTag("GameController")?.GetComponent<ScoreManager>();
    }

    void Start() {
        if (manager != null) {
            updateScore(0, manager.getBonus());
            if(manager.isHighScore())
                isNewHighScore();
        }
    }

    public void updateScore(int scoreAdded, bool bonus) {
        textScore.text = "Score: " + manager.getScore().ToString();
    }

    public void isNewHighScore() {
        textScore.color = colorHighscore;
        textScore.text += "<br>High score !";
    }
}
