using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] bool unlockOnlyY = false;
    Vector3 initialRotation;

    void Start() {
        initialRotation = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform.position);
        if (unlockOnlyY) {
            transform.rotation = Quaternion.Euler(initialRotation.x, transform.rotation.eulerAngles.y, initialRotation.z);
        }
    }
}
