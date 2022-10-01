using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FxDatabase", menuName = "ScriptableObjects/Databases/FxDatabase", order = 1)]
public class FxDatabase : ScriptableObject
{
    [System.Serializable]
    public struct FxElement {
        public string alias;
        public GameObject[] fxs;
    }

    [SerializeField] protected FxElement[] database;
    private Dictionary<string, GameObject[]> databaseDict;

    public void populateDict()
    {
        databaseDict = new Dictionary<string, GameObject[]>();
        foreach (FxElement elem in database)
        {
            databaseDict.Add(elem.alias, elem.fxs);
        }
    }

    public GameObject getFx(string fxAlias)
    {
        GameObject[] fxs;
        bool isFound = databaseDict.TryGetValue(fxAlias, out fxs);

        if (isFound) {
            int size = fxs.Length;
            return fxs[Random.Range(0, size)];
        } else {
            return null;
        }
    }
}
