using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreDeVie : MonoBehaviour
{
    void Start()
    {
        NumberDisplayer nd = GetComponent<NumberDisplayer>();
        if(nd != null){
            
            GameObject controller = GameObject.FindGameObjectWithTag("GameController");
            HasLife life;
            if (controller?.GetComponent<MainManager>() !=  null) {
                //FPS mode
                MainManager mm = controller.GetComponent<MainManager>();
                life = mm.Player.GetComponent<HasLife>();
            }
            else if (controller?.GetComponent<PsyManager>() !=  null) {
                //ZEN mode
                PsyManager mm = controller.GetComponent<PsyManager>();
                life = mm.GetComponent<HasLife>();
            } else {
                Debug.LogWarning("No manager found");
                return;
            }
            
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
