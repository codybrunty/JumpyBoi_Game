using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
<summary> Reset button in game calls the position managers reset game function.
*/

public class ResetButtonMechanics : MonoBehaviour{

    public void ResetOnClick() {
        PositionManager.PM.ResetAllData();
    }

}
