using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Bootstrap
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void bootstrap() {
        GameObject mainManager = Object.Instantiate(Resources.Load("MainManagers")) as GameObject;
        Assert.IsNotNull(mainManager);
        Object.DontDestroyOnLoad(mainManager);
    }
}
