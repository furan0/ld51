using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    public TextMeshProUGUI textScore;
    public GameObject prefabAddScore;
    public float delayBeforeDestroyingAdd = 0.3f;
    Animator anim;
    ScoreManager manager;

    void Awake() {
        manager = GameObject.FindGameObjectWithTag("GameController")?.GetComponent<ScoreManager>();
        anim = textScore.gameObject.GetComponent<Animator>();
    }

    void Start() {
        manager?.changeScore.AddListener(updateScore);
        manager?.newHighScore.AddListener(isNewHighScore);
        if (manager != null) {
            updateScore(0, manager.getBonus());
            if(manager.isHighScore())
                isNewHighScore();
        }
    }

    public void updateScore(int scoreAdded, bool bonus) {
        textScore.text = manager.getScore().ToString();
        anim?.SetBool("bonus", bonus);

        if (scoreAdded > 0) {
            anim?.SetTrigger("new");

            //TODO : spawn prefab 
            GameObject obj = GameObject.Instantiate(prefabAddScore, textScore.transform);
            obj.GetComponent<TextMeshProUGUI>().text = "+" + scoreAdded.ToString() + "!";
            Destroy(obj, delayBeforeDestroyingAdd);
        } 
    }

    public void isNewHighScore() {
        anim?.SetTrigger("highScore");
    }
}
