using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PsycheDatabase", menuName = "ScriptableObjects/Databases/PsycheDatabase", order = 1)]
public class PsycheDatabase : ScriptableObject
{
    [SerializeField]  List<string> database;

    public string getPsychiatristBabble()
    {
        return database[Random.Range(0, database.Count)];
    }

    public string getPsychiatristBabble(int index)
    {
        if (index >= database.Count)
            index = database.Count - 1;
        return database[index];
    }
}
