using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
<summary> Stores the players position and the postion of the rewind button between sessions. 
*/

public class PositionManager : MonoBehaviour{

    public static PositionManager PM;

    [Header ("Player Info")]
    public Vector3 playerPosition;
    public int playerZoneIndex;

    [Header ("Rewind Info")]
    public Vector3 rewindPosition;
    public int rewindZoneIndex;

    //Singleton
    void Awake() {
        if (PM == null) {
            //ResetAllData();
            LoadPlayerAndRewindPosition();
            DontDestroyOnLoad(gameObject);
            PM = this;
        }
        else if (PM != this) {
            Destroy(gameObject);
        }
    }

    //Loads player and rewind data into the position manager on awake for gameobjects to call
    private void LoadPlayerAndRewindPosition() {
        //Players Position and camera zone index.
        playerPosition = new Vector3(PlayerPrefs.GetFloat("positionX", 0.4f), PlayerPrefs.GetFloat("positionY", -2.3f), 0.0f);
        playerZoneIndex = PlayerPrefs.GetInt("playerZoneIndex", 0);

        //Players Position and camera zone index for previous jump for rewind button.
        rewindPosition = new Vector3(PlayerPrefs.GetFloat("rewindPositionX", 0.4f), PlayerPrefs.GetFloat("rewindPositionY", -2.3f), 0.0f);
        rewindZoneIndex = PlayerPrefs.GetInt("rewindZoneIndex", 0);
    }

    //Grounded Mechanic calls this function when the player hits the ground.
    public void SavePlayerPosition(Vector3 position, int index) {
        playerPosition = position;
        PlayerPrefs.SetFloat("positionX", playerPosition.x);
        PlayerPrefs.SetFloat("positionY", playerPosition.y);
        
        playerZoneIndex = index;
        PlayerPrefs.SetInt("playerZoneIndex", playerZoneIndex);
    }

    //The Rewind button calls this function when the player makes a jump.
    public void SaveRewindPosition(Vector3 position, int zone) {
        rewindPosition = position;
        PlayerPrefs.SetFloat("rewindPositionX", rewindPosition.x);
        PlayerPrefs.SetFloat("rewindPositionY", rewindPosition.y);

        rewindZoneIndex = zone;
        PlayerPrefs.SetInt("rewindZoneIndex", rewindZoneIndex);
    }

    //Reset Game.
    public void ResetAllData() {
        PlayerPrefs.DeleteAll();
        LoadPlayerAndRewindPosition();
        SceneManager.LoadScene(0);
    }

}
