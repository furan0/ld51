using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberDisplayer : MonoBehaviour
{

    int counter = 0;
    [SerializeField] bool desactivate = true;
    [SerializeField] GameObject[] item_list;
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0 ;  (i<item_list.Length); i++ ){
            if(desactivate){
                item_list[i].SetActive(false);
            }else{
                item_list[i].GetComponent<Animator>()?.SetBool("Mort",true); 
            }
            //.SetActive(false);
        }
    }

    public void setCounter(int newCounter){
        /*
        Update the current counter and the view.
        */
        counter = newCounter;
        updateDisplay();
    }

    public void updateDisplay(){
        // foreach (var item in item_list)
        // {
        //     item.SetActive(false);
        // }
        for (int i = 0; (i < counter) && (i<item_list.Length); i++ ){
            if(desactivate){
                item_list[i].SetActive(true);
            }else{
                item_list[i].GetComponent<Animator>()?.SetBool("Mort",false); 
            }
        }
        for (int j = counter ; j<item_list.Length ; j++ ){
            if(desactivate){
                item_list[j].SetActive(false);
            }else{
                item_list[j].GetComponent<Animator>()?.SetBool("Mort",true); 
            }
        }

    }

}
