using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HasLife : MonoBehaviour
{

    [SerializeField, Range(1, 10)] private int lifeMax = 3;
    [SerializeField] private int currentLife = -1; 
    public int CurrentLife {
        get {return currentLife;}
    }
    public UnityEvent lifeEmpty;
    public UnityEvent lifeFull;
    public UnityEvent<int> lifeLost;
    public UnityEvent<int> lifeGained;

    private DatabaseManager db;

    // Start is called before the first frame update
    

    void Awake()
    {
        if (currentLife == -1)
            currentLife = lifeMax;
    }

    void Start() {
        db = GameObject.FindGameObjectWithTag("Root")?.GetComponent<DatabaseManager>();
        if (db != null)
            setLife(db.data.playerHealth);
    }

    public void changeLife(int delta) 
    {
        if ((delta == 0) || !enabled)
            return;

        currentLife += delta;
        if (currentLife > lifeMax) {
            currentLife = lifeMax;
            lifeFull?.Invoke();
        }
        if (currentLife <= 0) {
            currentLife = 0;
            lifeEmpty?.Invoke();
        }

        if (delta < 0) {
            lifeLost?.Invoke(currentLife);
        } else if (delta > 0) {
            lifeGained?.Invoke(currentLife);
        }

        voidUpdateBd();
    }

    public void setLife(int life) {
        if ((currentLife == life) || !enabled)
            return;

        if (life < currentLife) {

            lifeLost?.Invoke(life);
        } else {
            lifeGained?.Invoke(life);
        }

        currentLife = life;
        if (currentLife > lifeMax) {
            currentLife = lifeMax;
            lifeFull?.Invoke();
        }
        if (currentLife <= 0) {
            currentLife = 0;
            lifeEmpty?.Invoke();
        }

        voidUpdateBd();
    }

    private void voidUpdateBd() {
        if (db != null)
            db.data.playerHealth = currentLife;
    }
}
