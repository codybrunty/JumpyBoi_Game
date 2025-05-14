using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPositionService {
    private Vector3 playerPosition;
    private int playerZoneIndex;

    private Vector3 rewindPosition;
    private int rewindZoneIndex;
    public Vector3 GetPlayerPosition() => playerPosition;
    public int GetPlayerZoneIndex() => playerZoneIndex;
    public Vector3 GetRewindPosition() => rewindPosition;
    public int GetRewindZoneIndex() => rewindZoneIndex;

    private SaveLoadService saveLoadService;

    public PlayerPositionService() {
        saveLoadService = ServiceLocator.GetService<SaveLoadService>();
        LoadPlayerAndRewindPosition();
    }

    public void LoadPlayerAndRewindPosition() {
        (playerPosition, playerZoneIndex) = saveLoadService.LoadPlayerPositionData();
        (rewindPosition, rewindZoneIndex) = saveLoadService.LoadRewindPositionData();
    }

    public void SavePlayerPosition(Vector3 position, int index) {
        playerPosition = position;
        playerZoneIndex = index;
        saveLoadService.SavePlayerPositionData(position, index);
    }

    public void SaveRewindPosition(Vector3 position, int zone) {
        rewindPosition = position;
        rewindZoneIndex = zone;
        saveLoadService.SaveRewindPositionData(position, zone);
    }

}

