using UnityEngine;

public class MoveInStraightLine : MonoBehaviour
{
    [Header("Parameters")]
    public Vector3 direction = Vector3.forward;
    public float speed = 1.0f;

    void Start()
    {
        direction.Normalize();
    }

    void FixedUpdate()
    {
        transform.position = transform.position + direction * speed * Time.fixedDeltaTime;
    }
}
