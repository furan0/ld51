using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Assertions;
using System;

public class CanShot : MonoBehaviour
{
    [Header("settings")]
    [SerializeField] protected Transform bulletSpawner; //Pewpew spawns here
    [SerializeField] protected GameObject[] bulletPrefab; //Pewpew that spawn
    [SerializeField] protected int prefabNbUsed = 0;    //pewpew chosen

    [Header("Events")]
    public string fxAlias = "SHOOT";
    public UnityEvent<Vector3, Vector3, string> pewpewFired; //params : position & direction & fxAlias of pewpew fired

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(bulletSpawner);
        Assert.IsNotNull(bulletPrefab);
    }

    //Shoot forward
    public void shoot() {
        shoot(transform.forward);
    }

    public void shoot(Vector3 dir_) {
        dir_.z = 0;
        dir_.Normalize();
        Vector3 pos = bulletSpawner.position;
        GameObject bullet = Instantiate(bulletPrefab[prefabNbUsed], pos, Quaternion.Euler(dir_));
        MoveInStraightLine bulletMovement = bullet.GetComponent<MoveInStraightLine>();
        if (bulletMovement != null)
            bulletMovement.direction = dir_;
        
        //FX
        Vector3 dirFx = new Vector3(0, 0, transform.eulerAngles.z);
        pewpewFired?.Invoke(pos, dirFx, fxAlias);
        Camera.main.GetComponent<CanShake>()?.shakeMoiCa();
    }
}
