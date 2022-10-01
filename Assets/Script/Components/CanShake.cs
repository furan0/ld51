using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanShake : MonoBehaviour
{
    [SerializeField] int nbFrameOfShake = 5;
    [SerializeField] float amountShaking = 0.1f;
    bool isShaking = false;
    bool oneshakeMore = false;

    public void shakeMoiCa(){
        if(!isShaking){
            StartCoroutine(Shake(nbFrameOfShake,amountShaking));
        }else{
            oneshakeMore = true;
        }
    }

    IEnumerator Shake(int howLong,float howMuch){
        isShaking = true;
        Vector3 initialPosition = transform.position;
        for (int i = 0; i < howLong; i++){
            transform.position = initialPosition + new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f)) * howMuch;
            yield return new WaitForFixedUpdate();
        }
        isShaking = false;
        if(oneshakeMore){
            oneshakeMore = false;
            StartCoroutine(Shake(howLong,howMuch));
        }
    }
}