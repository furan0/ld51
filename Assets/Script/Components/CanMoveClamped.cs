using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanMoveClamped : MonoBehaviour
{
    [SerializeField] Vector3 maxClamp;
    [SerializeField] Transform maxTransform;
    [SerializeField] Vector3 minClamp;
    [SerializeField] Transform minTransform;

    public Vector3 Position {
        get { return transform.position;}
        set { checkAndSetPos(value);}

    }
    // Start is called before the first frame update
    void Start()
    {
        if (maxTransform != null) {
            maxClamp = maxTransform.position;
        }

        if (minTransform != null) {
            minClamp = minTransform.position;
        }
    }

    void checkAndSetPos(Vector3 pos) {
        //Max
        if (pos.x > maxClamp.x)
            pos.x = maxClamp.x;
        if (pos.y > maxClamp.y)
            pos.y = maxClamp.y;
        if (pos.z > maxClamp.z)
            pos.z = maxClamp.z;

        //Min
        if (pos.x < minClamp.x)
            pos.x = minClamp.x;
        if (pos.y < minClamp.y)
            pos.y = minClamp.y;
        if (pos.z < minClamp.z)
            pos.z = minClamp.z;

        transform.position = pos;
    }
}
