using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneMechanics : MonoBehaviour{

    public bool top;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Jumpy") {
            Debug.Log("Entered Camera Zone");
            if (top) {
                gameObject.GetComponentInParent<CameraZones>().IncrementZoneChange(1);
            }
            else {
                gameObject.GetComponentInParent<CameraZones>().IncrementZoneChange(-1);
            }
        }
    }

}
