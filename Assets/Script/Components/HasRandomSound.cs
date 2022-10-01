using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HasRandomSound : MonoBehaviour
{
    
    [SerializeField,Tooltip("Clips to select randomly")] AudioClip[] clip;
    [SerializeField,Tooltip("Should the clip be played on start ?")] bool playOnStart=true;
    [Header("Audio Parameters")]
    [SerializeField,Tooltip("Volume of the clips")] float volume;
    [SerializeField,Tooltip("pitch minimum to play")] float pitchMinus;
    [SerializeField,Tooltip("pitch maximum to play")] float pitchMax;

    void Start()
    {
        if(playOnStart){
            PlaySound();
        }
    }

    public void PlaySound(){
        AudioSource audio = GetComponent<AudioSource>();
        if(audio){
            audio.volume = volume;
            audio.PlayOneShot(clip[Random.Range(0, clip.Length)]);
            audio.pitch = Random.Range(pitchMinus,pitchMax);
        }
    }
    
}
