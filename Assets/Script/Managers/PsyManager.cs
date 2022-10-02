using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PsyManager : MonoBehaviour
{
    public GameObject Yin;
    public GameObject Yang;

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(Yin);
        Assert.IsNotNull(Yang);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void accouplementReussi() {
        Debug.Log("Accouplement !");
    }
}
