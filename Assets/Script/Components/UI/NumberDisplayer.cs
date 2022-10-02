using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberDisplayer : MonoBehaviour
{

    int counter = 0;
    [SerializeField] GameObject[] item_list;
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0 ;  (i<item_list.Length); i++ ){
            item_list[i].SetActive(false);
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
        foreach (var item in item_list)
        {
            item.SetActive(false);
        }

        for (int i = 0 ; (i <counter) && (i<item_list.Length); i++ ){
            item_list[i].SetActive(true);
        }
    }

}
