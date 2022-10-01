using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Et oui les amis, les gens, ça MEURT !
[RequireComponent(typeof(Collider2D))]
public class CanKillPeople : MonoBehaviour
{
    [SerializeField] protected bool selfDestruct = true;
    [SerializeField] protected float selfDestructDelay = 10f;
    private float selfDestructTime = 0.0f;

    void Start() {
        selfDestructTime = Time.time + selfDestructDelay;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        CanDie die = other.GetComponent<CanDie>();

        if (die != null) {
            //We touched something that can die ! hurra !
            die.kill();
        }
        Destroy(gameObject);
    }

    void Update() {
        if (selfDestruct && (Time.time > selfDestructTime))
            Destroy(gameObject);
    }
}
