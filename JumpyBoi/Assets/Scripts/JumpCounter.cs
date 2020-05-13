using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
<summary> Keep track of total jumps for leaderboars an achievements.
*/

public class JumpCounter : MonoBehaviour{

    public int jumpTotal = 0;

    private void Start() {
        jumpTotal = PlayerPrefs.GetInt("JumpTotal", 0);
        UpdateJumpCounterText();
    }

    public void UpdateJumpCounterText() {
        gameObject.GetComponent<Text>().text = "Jumps: "+jumpTotal;
    }

    public void AddOneJump() {
        jumpTotal++;
        PlayerPrefs.SetInt("JumpTotal", jumpTotal);
        UpdateJumpCounterText();
    }

}
