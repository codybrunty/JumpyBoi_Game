using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{
    public static GameManager instance;
    void Awake() {
        if (instance == null) {  
            instance = this; 
            DontDestroyOnLoad(gameObject);

            //Register Services
            ServiceLocator.RegisterService<SaveLoadService>(new SaveLoadService());
            ServiceLocator.RegisterService<StatsService>(new StatsService());
            ServiceLocator.RegisterService<PlayerPositionService>(new PlayerPositionService());
        }
        else {
            Destroy(gameObject);
        }
    }
}
