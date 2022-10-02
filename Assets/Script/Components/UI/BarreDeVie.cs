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
                print("Je n'ai pas accès au manager");
            }
            if(mm.Player == null){
                print("la tu cherches...");
            }
            if(mm.Player.GetComponent<HasLife>() == null){
                print("Voyons..");
            }
            //mm.Player.GetComponent<HasLife>().lifeLost.AddListener(nd.setCounter);
            //mm.Player.GetComponent<HasLife>().lifeGained.AddListener(nd.setCounter);
            
        }else{
            print("Je n'ai pas de quoi afficher ma vie Connard.");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
