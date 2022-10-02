using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource source1;
    [SerializeField] AudioSource source2;
    private DatabaseManager db;

    enum E_Channel {
        CHANNEL_1,
        CHANNEL_2
    }
    private E_Channel currentChannel = E_Channel.CHANNEL_1;

    [SerializeField] float defaultTransitionTime = 1.0f;
    private float currentTransitionTime = 0.0f;
    private float spentTransitionTime = 0.0f;
    [SerializeField] private bool onGoingTransition = false;
    public bool OnGoingTransition {
        get {return onGoingTransition;}
    }

    void Awake() {
        db = GetComponent<DatabaseManager>();
        Assert.IsNotNull(db);

        source1.loop = true;
        source2.loop = true;

        currentChannel = E_Channel.CHANNEL_1;
        currentTransitionTime = defaultTransitionTime;
    }

    void Update() {
        //Handle transition if needed
        if (onGoingTransition) {
            spentTransitionTime += Time.deltaTime;
            // Calculate transition ratio
            float ratio = Mathf.Clamp(spentTransitionTime / currentTransitionTime, 0.0f, 1.0f);
            switch(currentChannel) {
                case E_Channel.CHANNEL_1:
                    source1.volume = ratio * db.settings.MusicVolume;
                    source2.volume = (1-ratio) * db.settings.MusicVolume;
                break;

                case E_Channel.CHANNEL_2:
                    source1.volume = (1-ratio) * db.settings.MusicVolume;
                    source2.volume = ratio * db.settings.MusicVolume;
                break;
            }

            if (ratio >= 1.0f) {
                //Transition completed
                onGoingTransition = false;
                switch(currentChannel) {
                    case E_Channel.CHANNEL_1:
                        source2.Stop();
                    break;

                    case E_Channel.CHANNEL_2:
                        source1.Stop();
                    break;
                }
            }
        }
    }

    public void switchMusic(string musicAlias) {
        switchMusic(musicAlias, defaultTransitionTime);
    }

    public void switchMusic(string musicAlias, float transition) {
        //Set up transition
        onGoingTransition = true;
        currentTransitionTime = transition;

        // Start Music on free channel and switch
        switch(currentChannel) {
            case E_Channel.CHANNEL_1:
                playMusic(musicAlias, source2, source1.time);
                currentChannel = E_Channel.CHANNEL_2;
            break;

            case E_Channel.CHANNEL_2:
                playMusic(musicAlias, source1, source2.time);
                currentChannel = E_Channel.CHANNEL_1;
            break;
        }
    }

    void playMusic(string alias, AudioSource channel) {
        playMusic(alias, channel, 0.0f);
    }

    void playMusic(string alias, AudioSource channel, float delay) {
        AudioClip clip = db.musicDatabase.getMusic(alias);
        if (clip != null) {
            channel.volume = db.settings.MusicVolume;
            channel.clip = clip;
            channel.time = delay;
            channel.Play();
        }
    }

    public void updateVolume() {
        // No need to update duringa transition
        if (onGoingTransition)
            return;

        switch(currentChannel) {
            case E_Channel.CHANNEL_1:
                source1.volume = db.settings.MusicVolume;
            break;

            case E_Channel.CHANNEL_2:
                source2.volume = db.settings.MusicVolume;
            break;
        }
    }
}
