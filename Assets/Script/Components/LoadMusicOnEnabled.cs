using UnityEngine;

public class LoadMusicOnEnabled : MonoBehaviour
{
    [SerializeField] string musicName;
    [SerializeField] float transition = 0.0f;
    
    void OnEnable()
    {
        GameObject.FindGameObjectWithTag("Root")?.GetComponent<MusicManager>()?.switchMusic(musicName, transition);
    }

}
