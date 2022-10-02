using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class HasLifeBd : MonoBehaviour
{
    private DatabaseManager db;
    private HasLife life;

    void Awake() {
        db = GameObject.FindGameObjectWithTag("Root")?.GetComponent<DatabaseManager>();
        life = GetComponent<HasLife>();

        Assert.IsNotNull(db);
        Assert.IsNotNull(life);
    }

    // Start is called before the first frame update
    void Start()
    {
        life.setLife(db.data.playerHealth);
        life.lifeGained.AddListener(lifeChanged);
        life.lifeLost.AddListener(lifeChanged);
    }

    void lifeChanged(int newValue) {
        db.data.playerHealth = newValue;
    }

    /*void OnDisable() {
        db.data.playerHealth = life.CurrentLife;
    }*/
}
