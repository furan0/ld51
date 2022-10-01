using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Assertions;
using System;

public class CanShoot : MonoBehaviour
{
    [Header("settings")]
    [SerializeField] protected Transform bulletSpawner; //Pewpew spawns here
    [SerializeField] protected GameObject[] bulletPrefab; //Pewpew that spawn
    [SerializeField] protected int prefabNbUsed = 0;    //pewpew chosen

    [Header("Events")]
    public string fxAlias = "SHOOT";
    public UnityEvent<Vector3, Vector3, string> pewpewFired; //params : position & direction & fxAlias of pewpew fired

    private CanAim aiming;

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(bulletSpawner);
        Assert.IsNotNull(bulletPrefab);
        aiming = GetComponent<CanAim>();
    }

    //Retrieve aiming direction, spawn bullet and sets its parameters
    public void shoot() {
        ///FIX///
        try
        {
            if(aiming != null){
            shoot(aiming.DirAimed);
        }
        }
        catch (System.Exception)
        {
            
        }
        // old shoot(aiming?.DirAimed ?? transform.eulerAngles);
    }

    public void shoot(Vector3 dir_) {
        //TODO

        /*if ((aiming != null) && !aiming.DirAimed.Equals(dir_)) {
            //Readjust aiming
            aiming.updateAimedDirection(dir_);
        }

        dir_.z = 0;
        dir_.Normalize();
        Vector3 pos = bulletSpawner.position;
        pos.z = 0;
        GameObject bullet = Instantiate(bulletPrefab[prefabNbUsed], pos, Quaternion.Euler(dir_));
        MoveInStraightLine bulletMovement = bullet.GetComponent<MoveInStraightLine>();
        if (bulletMovement != null)
            bulletMovement.direction = dir_;
        
        //FX
        Vector3 dirFx = new Vector3(0, 0, aiming?.getAImedAngle() ?? transform.eulerAngles.z);
        pewpewFired?.Invoke(pos, dirFx, fxAlias);
        Camera.main.GetComponent<CanShake>()?.shakeMoiCa(); */
    }
}
