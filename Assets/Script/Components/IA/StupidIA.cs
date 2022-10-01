using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Stupid AI.
Look out for the player in a straight line. If found, do the followings : 
  * have a probability to just stay idle
  * try to shot
  * if impossible, try to reach target distance with the player
*/
public class StupidIA : MonoBehaviour
{
    [Header("Walk parameters")]
    [SerializeField] float detectionRange = 10.0f;
    [SerializeField, Range(0,1)] float idleProbability = 0.3f; //Lazy Bum
    [SerializeField] float maxDeviationAngle = 10.0f;
    [SerializeField] float minWalkTime = 0.5f;
    private float timeSinceStartWalk = 0.0f;
    private Vector3 movePos;
    [Header("Shot parameter")]
    [SerializeField] float shotDistance = 5.0f;
    [SerializeField] float timeBetweenShot = 2.0f;
    private float lastTimeSinceShot = 0.0f;

    enum E_State {
        IDLE,
        WALK,
        SHOT
    }
    [Header("Observable")]
    [SerializeField] private E_State state = E_State.IDLE;

    CanMove move;
    CanShot shot;

    void Start() {
        move = GetComponent<CanMove>();
        shot = GetComponent<CanShot>();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        state = E_State.IDLE;
        lastTimeSinceShot = 0.0f;
        timeSinceStartWalk = 0.0f;
        movePos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        lastTimeSinceShot += Time.deltaTime;
        timeSinceStartWalk += Time.deltaTime;
        bool playerFound = lookForPlayer(out hit);

        switch (state) {
            case E_State.IDLE:
                if (!playerFound)
                    break;
                //Player found ! hurra  !

                //Are we lazy ?
                if (isLazy())
                    break;
                
                // Or not I guess... Is the player in shoting range ?
                if ((hit.distance <= shotDistance) && (lastTimeSinceShot > timeBetweenShot)) {
                    // PEW PEW !
                    state = E_State.SHOT;
                }
                else 
                {
                    // walk toward him
                    state = E_State.WALK;
                }

            break;

            case E_State.WALK:
                //Are we still walking ?
                if (timeSinceStartWalk <= minWalkTime) {
                    //Still walking
                    move?.moveTo(movePos);
                    break;
                }

                //Are we lazy ?
                if (isLazy()) {
                    state = E_State.IDLE;
                    break;
                }

                // Or not I guess... Is the player in shoting range ?
                if ((hit.distance <= shotDistance) && (lastTimeSinceShot > timeBetweenShot)) {
                    // PEW PEW !
                    state = E_State.SHOT;
                }
                
                // Let's move baby !
                timeSinceStartWalk = 0.0f;
                movePos = hit.transform.position;
                //Add random angle
                float rng = Random.Range(-maxDeviationAngle, maxDeviationAngle);
                movePos = Quaternion.Euler(0, rng, 0) * movePos;
                move?.moveTo(movePos);
            break;

            case E_State.SHOT:
                //Are we lazy ?
                if (isLazy())
                    break;

                // Or not I guess... Is the player in shoting range ?
                if ((hit.distance <= shotDistance) && (lastTimeSinceShot > timeBetweenShot)) {
                    // PEW PEW !
                    shot?.shoot();
                    lastTimeSinceShot = 0.0f;
                }

                state = E_State.IDLE;
            break;
        }
    }

    bool lookForPlayer(out RaycastHit hit) {
        Debug.Log("Stupid cast toward : " + transform.rotation.eulerAngles);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * detectionRange, Color.yellow);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, detectionRange, LayerMask.GetMask(new string[] {"Player", "Default"}))) {
            Debug.Log("Something wa hit");
            return (hit.transform.gameObject.tag.Equals("Player"));
        } else
            return false;
    }

    bool isLazy() {
        //Throw a dice to see if we are currently lazy...
        float rng = Random.Range(0.0f, 1.0f);
        return (rng <= idleProbability);
    }
}
