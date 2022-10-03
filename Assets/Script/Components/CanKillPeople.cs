using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Et oui les amis, les gens, ça MEURT !
[RequireComponent(typeof(Collider))]
public class CanKillPeople : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] protected int baseDamage = 3;
    [SerializeField] protected float damageFalloff = 0.5f;
    [SerializeField] protected bool instaKill = false;
    [SerializeField] string[] raycastLayer = {"Default"};
    [Header("Self-destruct")]
    [SerializeField] protected bool selfDestruct = true;
    [SerializeField] protected float selfDestructDelay = 10f;
    [SerializeField] protected bool isDestroyedOnHit = true;
    private float selfDestructTime = 0.0f;

    [Header("Events")]
    [SerializeField] string fxAliasHit;
    [SerializeField] string fxAliasMiss;
    public UnityEvent<Vector3, string> hitSomething;

    void Start() {
        selfDestructTime = Time.time + selfDestructDelay;
    }


    private void OnTriggerEnter(Collider other)
    {
        //Direction toward target
        Vector3 contactPoint = other.ClosestPointOnBounds(transform.position);
        Vector3 dir = (contactPoint - transform.position).normalized;
        //Check hit validity with a raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity, LayerMask.GetMask(raycastLayer)))
        {
            Debug.DrawRay(transform.position, dir * hit.distance, Color.yellow);
            //Debug.Log("Hit touched something : " + hit.collider.gameObject.name);

            if (instaKill) {
                CanDie die = hit.collider.gameObject.GetComponent<CanDie>();
                if (die != null) {
                    //We touched something that can die ! hurra !
                    die.kill();
                    hitSomething?.Invoke(hit.point, fxAliasHit);
                } else {
                    hitSomething?.Invoke(hit.point, fxAliasMiss);
                }
            } else {
                HasLife life = hit.collider.gameObject.GetComponent<HasLife>();
                if (life != null) {
                    life.changeLife(-calculateDamage(hit.distance));
                    hitSomething?.Invoke(hit.point, fxAliasHit);
                } else {
                    hitSomething?.Invoke(hit.point, fxAliasMiss);
                }
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
