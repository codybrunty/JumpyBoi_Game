using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadService {

    #region Player Position
    public void SavePlayerPositionData(Vector3 position, int zoneIndex) {
        PlayerPrefs.SetFloat("positionX", position.x);
        PlayerPrefs.SetFloat("positionY", position.y);
        PlayerPrefs.SetInt("playerZoneIndex", zoneIndex);
        PlayerPrefs.Save();
    }
    public void SaveRewindPositionData(Vector3 position, int zoneIndex) {
        PlayerPrefs.SetFloat("rewindPositionX", position.x);
        PlayerPrefs.SetFloat("rewindPositionY", position.y);
        PlayerPrefs.SetInt("rewindZoneIndex", zoneIndex);
        PlayerPrefs.Save();
    }

    public (Vector3, int) LoadPlayerPositionData() {
        Vector3 position = new Vector3(PlayerPrefs.GetFloat("positionX", 0.4f),PlayerPrefs.GetFloat("positionY", -2.3f),0f);
        int zoneIndex = PlayerPrefs.GetInt("playerZoneIndex", 0);
        return (position, zoneIndex);
    }

    public (Vector3, int) LoadRewindPositionData() {
        Vector3 position = new Vector3(PlayerPrefs.GetFloat("rewindPositionX", 0.4f), PlayerPrefs.GetFloat("rewindPositionY", -2.3f), 0f);
        int zoneIndex = PlayerPrefs.GetInt("rewindZoneIndex", 0);
        return (position, zoneIndex);
    }
    #endregion

    #region Stats
    private const string StatsPrefix = "Stat_";
    public void SaveStat(string statName, int value) {
        PlayerPrefs.SetInt(StatsPrefix + statName, value);
        PlayerPrefs.Save();
    }
    public int LoadStat(string statName) {
        return PlayerPrefs.GetInt(StatsPrefix + statName, 0);
    }

    #endregion

    #region Reset
    public void DeleteAllPlayerPrefs() {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
    #endregion

}
