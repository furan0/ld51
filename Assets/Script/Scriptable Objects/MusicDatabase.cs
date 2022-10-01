using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicDatabase", menuName = "ScriptableObjects/Databases/MusicDatabase", order = 1)]
public class MusicDatabase : ScriptableObject
{
    [System.Serializable]
    public struct MusicElement {
        public string alias;
        public AudioClip[] musics;
    }

    [SerializeField] protected MusicElement[] database;
    private Dictionary<string, AudioClip[]> databaseDict;

    public void populateDict()
    {
        databaseDict = new Dictionary<string, AudioClip[]>();
        foreach (MusicElement elem in database)
        {
            databaseDict.Add(elem.alias, elem.musics);
        }
    }

    public AudioClip getMusic(string alias)
    {
        AudioClip[] musics;
        bool isFound = databaseDict.TryGetValue(alias, out musics);

        if (isFound) {
            int size = musics.Length;
            return musics[Random.Range(0, size)];
        } else {
            return null;
        }
    }
}
