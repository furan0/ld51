using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class playRandomMusic : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] string[] musicNames;
    private DatabaseManager db;

    // Start is called before the first frame update
    void Start()
    {
        if (source == null)
            source = GetComponent<AudioSource>();
        db = GameObject.FindGameObjectWithTag("Root")?.GetComponent<DatabaseManager>();
        Assert.IsNotNull(db);

        if(musicNames.Length > 0 && source != null) {
            source.clip = db.musicDatabase.getMusic(musicNames[Random.Range(0, musicNames.Length)]);
            source.Play();
        }
    }
}
