using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreDeVie : MonoBehaviour
{
    void Start()
    {
        NumberDisplayer nd = GetComponent<NumberDisplayer>();
        if(nd != null){
            
            MainManager mm = GameObject.FindGameObjectWithTag("GameController").GetComponent<MainManager>();
            if(mm == null){
                Debug.LogWarning("Je n'ai pas accès au manager");
            }
            if(mm.Player == null){
                Debug.LogWarning("la tu cherches...");
            }
            HasLife life = mm.Player.GetComponent<HasLife>();
            life?.lifeLost.AddListener(nd.setCounter);
            life?.lifeGained.AddListener(nd.setCounter);
            if (life != null)
                nd.setCounter(life.CurrentLife);
            
        }else{
            Debug.LogWarning("Je n'ai pas de quoi afficher ma vie Connard.");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
