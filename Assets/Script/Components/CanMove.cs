using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody2D))]
public class CanMove : DisbaledOnDeath
{
    [Header("Speed")]
    public float acceleration = 0.2f;
    public float desiredSped = 10f;
    [SerializeField] protected float moveThreshold = 0.1f;
    protected float currentSpeed = 0.0f;
    [SerializeField] private float desiredSpeedMultiplicator = 1.0f;

    [Header("Walking event generation")]
    [SerializeField] protected float walkEventPeriod = 0.5f;
    public UnityEvent walkEvent;
    private float nextWalkEventTime = 0.0f;
    
    public Vector3 direction;
    protected Rigidbody2D rb;
    Animator animator;
    bool isUsingNavAgent = false;

    bool isMoving = false;
    public bool IsMoving {
        get {return isMoving; }
    }
    bool isEnabled = true;
    public bool IsEnabled {
        get {return isEnabled; }
        set {enable(value); }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Assert.IsNotNull(rb);
    }

    void Update()
    {
        animator.SetBool("Run", IsMoving);

        if (isMoving && (Time.time > nextWalkEventTime)) {
            walkEvent?.Invoke();
            nextWalkEventTime = Time.time + walkEventPeriod;
        }
    }

    /** Enable or disable the movment */
    void enable(bool isEnabled_) {
        isEnabled = isEnabled_;

        if (!isEnabled) {
            //Stop current movement
            moveTo(transform.position);
        }
    }

    /** Request a mover to a given pos
        @param destination_ position to move toward
    */
    public void moveTo(Vector3 destination_)
    {
        //TODO

        /*if (isUsingNavAgent) {
            //Using navmesh -> send the command to the navmesh agent
            agent.moveTo(destination_);

        } else {
            Debug.LogError("CanMove::MoveTo requires a pathfinding agent !");
        }*/
    }

    /** Request a mover in a given direction
        @param direction_ direction to move toward
    */
    public void moveToward(Vector3 direction_)
    {
        if (direction_ == Vector3.zero) {
            //Stop moving movements
            desiredSpeedMultiplicator = 0.0f;
        } else {
            direction = direction_.normalized;
            isMoving = true;
            
            //if (isUsingNavAgent) {
                //Using navmesh -> send the command to the navmesh agent
                //agent.moveTo.position + direction;

            //} else {
                //Not using navmesh -> set the requested speed to 1
                desiredSpeedMultiplicator = 1.0f;
            //}
        }
    }

    private void FixedUpdate() {
        if(desiredSpeedMultiplicator < currentSpeed)
            currentSpeed = Mathf.Pow(Mathf.Lerp(currentSpeed, desiredSpeedMultiplicator, acceleration), 2);
        else
            currentSpeed = Mathf.Sqrt(Mathf.Lerp(currentSpeed, desiredSpeedMultiplicator, acceleration));
        Vector3 deltaPos = direction.normalized * Mathf.Sqrt(currentSpeed)*desiredSped * Time.fixedDeltaTime;
        if (deltaPos.magnitude >=0.001f) {
            rb.MovePosition( transform.position + deltaPos);
        } else if (isUsingNavAgent) { 
            //currentSpeed = agent.velocity.magnitude;
            //isMoving = agent.walk;
            //TODO
        } else {
            isMoving = false;
        }
        
        
    }
    
    private void OnDrawGizmos() {
        // Gizmos.DrawLine(transform.position,transform.position+direction*10f);
        // Gizmos.color = Color.red;
        // Gizmos.DrawLine(transform.position,transform.position+ transform.up*10f);
        // Gizmos.DrawWireSphere(transform.position + transform.up*10f*currentSpeed,1.0f);
    }
}
