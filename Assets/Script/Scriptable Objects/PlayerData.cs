using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** This class store the various game settings needed all accross the game
*/
[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [SerializeField] private int initialLife = 5;
    public int playerHealth = 5;
    public bool tutoAlreadyPlayed = false;
    public bool firstTimeInFPS = true;
    public int nbTimeInFPS = 0;
    public int score = 0;
    [SerializeField] private Vector3 initialPosition = Vector3.zero;
    public Vector3 lastKilledPosition = Vector3.zero;

    public void reset() {
        playerHealth = initialLife;
        tutoAlreadyPlayed = false;
        score = 0;
        nbTimeInFPS = 0;
        lastKilledPosition = initialPosition;
    }
}
