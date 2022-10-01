using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanShake : MonoBehaviour
{
    [SerializeField] int nbFrameOfShake = 5;
    [SerializeField] float amountShaking = 0.1f;
    [SerializeField] bool fixedInitialPosition = true;
    private Vector3 initialPos;
    bool isShaking = false;
    bool oneshakeMore = false;

    void Start() {
        initialPos = transform.localPosition;
    }

    public void shakeMoiCa(){
        if(!isShaking){
            StartCoroutine(Shake(nbFrameOfShake,amountShaking));
        }else{
            oneshakeMore = true;
        }
    }

    IEnumerator Shake(int howLong,float howMuch){
        isShaking = true;
        if (!fixedInitialPosition)
            initialPos = transform.localPosition;
        for (int i = 0; i < howLong; i++){
            transform.localPosition = initialPos + new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f)) * howMuch;
            yield return new WaitForFixedUpdate();
        }
        isShaking = false;
        transform.localPosition = initialPos;
        if(oneshakeMore){
            oneshakeMore = false;
            StartCoroutine(Shake(howLong,howMuch));
        }
    }
}