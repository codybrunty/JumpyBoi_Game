using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
<summary> Keep track of total jumps for leaderboars an achievements.
*/

public class UI_JumpCounter : MonoBehaviour{

    private Text jumpCounterText;
    StatsService statsService;

    private void Start() {
        jumpCounterText = GetComponent<Text>();
        statsService = ServiceLocator.GetService<StatsService>();
        UpdateJumpCounterText();
    }

    public void UpdateJumpCounterText() {
        int jumpTotal = statsService.GetStat("Jumps");
        jumpCounterText.text = "Jumps: "+ jumpTotal;
    }

}
