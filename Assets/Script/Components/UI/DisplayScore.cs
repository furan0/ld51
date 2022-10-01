using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textMaxScore;
    ScoreManager manager;

    void Start() {
        manager = GameObject.FindGameObjectWithTag("GameController")?.GetComponent<ScoreManager>();
        updateScore();
    }

    public void updateScore() {
        textScore.text = "Score : " + manager?.getScore().ToString();
        textMaxScore.text = "High Score : " + manager?.getHighScore().ToString();
    }
}
