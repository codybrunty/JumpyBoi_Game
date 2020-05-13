using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
<summary> Move the player and the camera to the previous jump position.
*/

public class RewindMechanic : MonoBehaviour{

    [Header("Rewind Info")]
    public Vector3 rewindPosition;
    public int rewindZoneIndex;

    [Header ("Rewind Game Objects")]
    [SerializeField] GameObject player = default;
    [SerializeField] CameraZones zones = default;


    private void Start() {
        SetRewindPositionFromPositionManager();
    }

    //Get the users rewind info from the position manager.
    private void SetRewindPositionFromPositionManager() {
        gameObject.GetComponent<Button>().interactable = true;
        rewindPosition = PositionManager.PM.rewindPosition;
        rewindZoneIndex = PositionManager.PM.rewindZoneIndex;
    }

    //Before the player jumps the position of the player and the camera zone is recorded into the rewind button
    //and saved into the position manager.
    public void SavePosition(Vector3 pos) {
        gameObject.GetComponent<Button>().interactable = true;
        rewindPosition = pos;
        rewindZoneIndex = zones.zoneIndex;
        PositionManager.PM.SaveRewindPosition(rewindPosition, rewindZoneIndex);
    }

    //On button click the player is moved to the rewind position. And the camera is moved to the rewind camera position.
    public void RewindOnClick() {
        gameObject.GetComponent<Button>().interactable = false;
        zones.ZoneChangeByIndex(rewindZoneIndex);
        player.transform.position = rewindPosition;
    }
}
