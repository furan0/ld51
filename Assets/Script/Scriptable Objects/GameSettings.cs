using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** This class store the various game settings needed all accross the game
*/
[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
    [Header("Volume")]
    [SerializeField, Range(0, 1), Tooltip("Game music volume")] protected float musicVolume;
    public float MusicVolume {
        get {return musicVolume;}
    }

    [SerializeField, Range(0, 1), Tooltip("Game sound effects volume")] protected float soundVolume;
    public float SoundVolume {
        get {return soundVolume;}
    }

    public void setMusicVolume(float volume) {
        if (volume > 1.0f) volume = 1.0f;
        if (volume < 0.0f) volume = 0.0f;
        musicVolume = volume;
    }

    public void setSoundVolume(float volume) {
        if (volume > 1.0f) volume = 1.0f;
        if (volume < 0.0f) volume = 0.0f;
        soundVolume = volume;
    }
}
