using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

[RequireComponent(typeof(CanPlaySound))]
public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject [] panels = null;
    private int activePanel = 0;

    public String sonClicAlias;
    private CanPlaySound soundPlayer;

    public float waitBeforeSwitchingScene = 0.2f;
    public String menuName = "menu";


    void Start() {
        soundPlayer = GetComponent<CanPlaySound>();
        switchPanel(activePanel);
    }

    public void allerAlaScene(string nomDeScene){
        playClicSound();
        GameObject root = GameObject.FindGameObjectWithTag("Root");
        root?.GetComponent<SceneLoader>()?.switchSceneAfterDelay(nomDeScene, waitBeforeSwitchingScene);
    }

    public void switchPanel(int numeroPanel){
        activePanel = numeroPanel;
        if (numeroPanel < panels.Length){
            playClicSound();
            for (int i = 0; i< panels.Length;i++){
                panels[i].SetActive(false);
            }
            panels[numeroPanel].SetActive(true);
        }
    }

    public int getActivePanel() {
        return activePanel;
    }


    public void retourMenu() {
        allerAlaScene(menuName);
    }

    public void playClicSound() {
        soundPlayer.playSound(sonClicAlias);
    }
}
