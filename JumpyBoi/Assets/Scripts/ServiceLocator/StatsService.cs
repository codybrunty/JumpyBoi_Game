using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsService {
    private Dictionary<string, int> statsDict = new();
    private SaveLoadService saveLoadService;

    public StatsService() {
        saveLoadService = ServiceLocator.GetService<SaveLoadService>();
        LoadStat("Jumps");
    }

    private void LoadStat(string statName) {
        int value = saveLoadService.LoadStat(statName);
        statsDict[statName] = value;
    }

    public int GetStat(string statName) {
        if (!statsDict.ContainsKey(statName)) {
            LoadStat(statName);
        }
        return statsDict[statName];
    }

    public void IncrementStat(string statName, int amount = 1) {
        if (!statsDict.ContainsKey(statName)) {
            LoadStat(statName);
        }
        statsDict[statName] += amount;
        saveLoadService.SaveStat(statName, statsDict[statName]);
    }

    public void ResetLocalStats() {
        statsDict.Clear();
    }
}
