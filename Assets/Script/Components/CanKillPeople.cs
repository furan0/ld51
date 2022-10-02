using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Et oui les amis, les gens, ça MEURT !
[RequireComponent(typeof(Collider))]
public class CanKillPeople : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] protected int baseDamage = 3;
    [SerializeField] protected float damageFalloff = 0.5f;
    [SerializeField] protected bool instaKill = false;
    [Header("Self-destruct")]
    [SerializeField] protected bool selfDestruct = true;
    [SerializeField] protected float selfDestructDelay = 10f;
    [SerializeField] protected bool isDestroyedOnHit = true;
    private float selfDestructTime = 0.0f;

    void Start() {
        selfDestructTime = Time.time + selfDestructDelay;
    }


    private void OnTriggerEnter(Collider other)
    {
        //Check hit validity with a raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Hit touched something : " + hit.collider.gameObject.name);

            if (instaKill) {
                CanDie die = hit.collider.gameObject.GetComponent<CanDie>();
                if (die != null) {
                    //We touched something that can die ! hurra !
                    die.kill();
                }
            } else {
                HasLife life = hit.collider.gameObject.GetComponent<HasLife>();  
                life?.changeLife(-calculateDamage(hit.distance));
            }
            
        }

        if (isDestroyedOnHit)
            Destroy(gameObject);
    }

    void Update() {
        if (selfDestruct && (Time.time > selfDestructTime))
            Destroy(gameObject);
    }

    private int calculateDamage(float distance) {
        return baseDamage - (int) (Mathf.Pow(distance, 2) * damageFalloff);
    }
}
