using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanAim: DisbaledOnDeath
{
    [Header("infos")]
    [SerializeField] private bool aimBasedOnPosition = true;
    [SerializeField] private Vector3 posAimed = new Vector3(-800, 0, 0);
    [SerializeField] private Vector3 dirAimed = Vector3.left;

    public Vector3 DirAimed {
        get {return (aimBasedOnPosition)? (posAimed - transform.position).normalized : dirAimed; }
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = getEulerAngles();
    }

    public void updateAimedPosition(Vector3 position_)
    {
        posAimed = position_;
        aimBasedOnPosition = true;
    }

    public void updateAimedDirection(Vector3 direction_)
    {
        dirAimed = direction_.normalized;
        posAimed = transform.position + dirAimed; //Useful for gyzmo display :)
        aimBasedOnPosition = false;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(posAimed, 0.5f);
    }

    public Vector3 getEulerAngles() {
        float angle = Vector3.SignedAngle(Vector3.right, DirAimed, Vector3.forward);
        return new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);
    }

    public float getAImedAngle() {
        return Vector3.SignedAngle(Vector3.right, DirAimed, Vector3.forward);
    }
}
