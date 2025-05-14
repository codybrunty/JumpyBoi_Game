using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
<summary> Collision trigger under the player that detects if we are standing on the ground.
*/

public class GroundMechanic : MonoBehaviour{

    public bool grounded = false;
    public int groundCounter = 0;
    [SerializeField] CameraZones zones=default;
    PlayerPositionService posService;

    private void Start() {
        posService = ServiceLocator.GetService<PlayerPositionService>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Ground") {
            Debug.Log("touched ground");
            RecordPlayerPosition();
            groundCounter++;
            grounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.name == "Ground") {
            Debug.Log("not touching ground");
            groundCounter--;
            //Keeping track of the number of grounds we are touching incase the user is touhing more then one. 
            //Example user is in the corner or on the edge of two platforms.
            if (groundCounter < 1) {
                groundCounter = 0;
                grounded = false;
            }
        }
    }

    //Record the position of player into the Position Manager everytime we hit the ground.
    private void RecordPlayerPosition() {
        posService.SavePlayerPosition(gameObject.transform.parent.transform.position, zones.zoneIndex);
    }

}
