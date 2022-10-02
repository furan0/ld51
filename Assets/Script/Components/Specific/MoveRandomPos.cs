using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRandomPos : MonoBehaviour
{
    [SerializeField] Vector3 maxClamp;
    [SerializeField] Transform maxTransform;
    [SerializeField] Vector3 minClamp;
    [SerializeField] Transform minTransform;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (maxTransform != null) {
            maxClamp = maxTransform.position;
        }

        if (minTransform != null) {
            minClamp = minTransform.position;
        }
        
        float x = Random.Range(minClamp.x, maxClamp.x);
        float y = Random.Range(minClamp.y, maxClamp.y);
        float z = Random.Range(minClamp.z, maxClamp.z);
        transform.position = new Vector3(x, y, z);
    }
}
