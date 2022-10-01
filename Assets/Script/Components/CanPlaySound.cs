using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine;

public class CanPlaySound : MonoBehaviour
{
    [SerializeField] AudioSource source;
    private SoundDatabase soundDb;
    private GameSettings settings;

    // Start is called before the first frame update
    void Start()
    {
        if (source == null)
            source = GetComponent<AudioSource>();

        GameObject controller = GameObject.FindGameObjectWithTag("GameController");
        soundDb = controller?.GetComponent<DatabaseManager>()?.soundDatabase;
        settings = controller?.GetComponent<DatabaseManager>()?.settings;

    }

    public void playSound(string soundAlias) {
        if (soundDb != null && source != null) {
            if (settings != null)
                source.volume = settings.SoundVolume;

            AudioClip sound = soundDb.getSound(soundAlias);
            if (sound != null)
                source.PlayOneShot(sound);
            else
                Debug.LogWarning("Impossible to play sound, " + soundAlias + " not found in DB");
        }
    }
}
