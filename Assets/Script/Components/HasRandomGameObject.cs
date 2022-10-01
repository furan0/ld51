using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasRandomGameObject : MonoBehaviour
{
    [SerializeField,Tooltip("Game Object to select randomly")] GameObject[] gameObjectList;

    void Start()
    {
        gameObjectList[Random.Range(0, gameObjectList.Length)].SetActive(true);
    }


}
