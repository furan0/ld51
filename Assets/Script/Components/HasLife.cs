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

    // Start is called before the first frame update
    void Start()
    {
        if (currentLife == -1)
            currentLife = lifeMax;
    }

    public void changeLife(int delta) 
    {
        if (delta == 0)
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
    }
}
