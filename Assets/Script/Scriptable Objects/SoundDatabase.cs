using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundDatabase", menuName = "ScriptableObjects/Databases/SoundDatabase", order = 1)]
public class SoundDatabase : ScriptableObject
{
    [System.Serializable]
    public struct SoundElement {
        public string alias;
        public AudioClip[] clips;
    }

    [SerializeField] protected SoundElement[] database;
    private Dictionary<string, AudioClip[]> databaseDict;

    public void populateDict()
    {
        databaseDict = new Dictionary<string, AudioClip[]>();
        foreach (SoundElement elem in database)
        {
            databaseDict.Add(elem.alias, elem.clips
            );
        }
    }

    public AudioClip getSound(string soundAlias)
    {
        AudioClip[] clips;
        bool isFound = databaseDict.TryGetValue(soundAlias, out clips);

        if (isFound) {
            int size = clips.Length;
            return clips[Random.Range(0, size)];
        } else {
            return null;
        }
    }
}
