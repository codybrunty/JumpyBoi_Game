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

    [Header("UI")]
    [SerializeField] Button rewindButton;

    PlayerPositionService posService;
    SaveLoadService saveLoadService;
    StatsService statsService;

    private void Start() {
        posService = ServiceLocator.GetService<PlayerPositionService>();
        saveLoadService = ServiceLocator.GetService<SaveLoadService>();
        statsService = ServiceLocator.GetService<StatsService>();
        SetRewindPositionFromPositionManager();
    }

    //Get the users rewind info from the position manager.
    private void SetRewindPositionFromPositionManager() {
        rewindButton.interactable = true;
        rewindPosition = posService.GetRewindPosition();
        rewindZoneIndex = posService.GetRewindZoneIndex();
    }

    //Before the player jumps the position of the player and the camera zone is recorded into the rewind button
    //and saved into the position manager.
    public void SaveRewindPosition(Vector3 pos) {
        rewindButton.interactable = true;
        rewindPosition = pos;
        rewindZoneIndex = zones.zoneIndex;
        posService.SaveRewindPosition(rewindPosition, rewindZoneIndex);
    }

    //On button click the player is moved to the rewind position. And the camera is moved to the rewind camera position.
    public void RewindOnClick() {
        rewindButton.interactable = false;
        zones.ZoneChangeByIndex(rewindZoneIndex);
        player.transform.position = rewindPosition;
    }

    public void FullRewindOnClick() {
        saveLoadService.DeleteAllPlayerPrefs();
        statsService.ResetLocalStats();
        posService.LoadPlayerAndRewindPosition();
        SetRewindPositionFromPositionManager();
        RewindOnClick();
        EventBus.Publish("FullRewind");
    }
}
