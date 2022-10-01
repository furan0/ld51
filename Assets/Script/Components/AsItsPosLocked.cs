using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsItsPosLocked : MonoBehaviour
{
    [SerializeField] protected GameObject gameObjectToLockOnto;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObjectToLockOnto!= null)
            transform.position = gameObjectToLockOnto.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObjectToLockOnto!= null)
            transform.position = gameObjectToLockOnto.transform.position;
    }
}
