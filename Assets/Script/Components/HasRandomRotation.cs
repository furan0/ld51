using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasRandomRotation : MonoBehaviour
{

    [SerializeField] Vector3 bornInfRot;
    [SerializeField] Vector3 bornSupRot;
    // Start is called before the first frame update
    void Start()
    {
        float rotx = Random.Range(bornInfRot.x,bornSupRot.x);
        float roty = Random.Range(bornInfRot.y,bornSupRot.y);
        float rotz = Random.Range(bornInfRot.z,bornSupRot.z);
        transform.localRotation = Quaternion.Euler(rotx,roty,rotz);
    }

    
}
