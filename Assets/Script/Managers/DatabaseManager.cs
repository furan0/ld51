using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DatabaseManager : MonoBehaviour
{
    public FxDatabase fxDatabase;

    // Start is called before the first frame update
    void Awake()
    {
        Assert.IsNotNull(fxDatabase);
        fxDatabase.populateDict();
    }
}
