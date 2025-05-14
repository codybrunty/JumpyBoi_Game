using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
<summary> Collision trigger zones are set up at the top of screen and the bottom of the screen. 
If the player breaks into one of these triggers the Static Camera moves to its next zone.
*/

public class CameraZones : MonoBehaviour{

    [SerializeField] GameObject zonePrefab = default;
    private Camera mainCam;
    private GameObject topZone;
    private GameObject botZone;
    public int zoneIndex = 0;
    PlayerPositionService posService;

    private void Start() {
        mainCam = Camera.main;
        posService = ServiceLocator.GetService<PlayerPositionService>();
        SetZoneIndexFromPositionManager();
    }

    //At start get Saved Position From Position Manager of last login.
    public void SetZoneIndexFromPositionManager() {
        ZoneChangeByIndex(posService.GetPlayerZoneIndex());
    }

    //Called by zones to move the Camera up and down by one. Top zone we go up, bot we go down. 
    public void IncrementZoneChange(int changeNum) {
        ZoneChangeByIndex(zoneIndex + changeNum);
    }

    //Move camera to specific position and create new Zones.
    public void ZoneChangeByIndex(int targetIndex) {
        DeleteZones();
        MoveCameraTo(targetIndex);
        InstantiateZones();
    }

    public void DeleteZones() {
        Debug.Log("Deleting Zones");
        if (topZone != null) {
            Destroy(topZone);
        }
        if(botZone != null) {
            Destroy(botZone);
        }
    }

    private void MoveCameraTo(int targetIndex) {
        if (zoneIndex != targetIndex) {
            Debug.Log("Moving Camera To Target Zone Index " + targetIndex);
            zoneIndex = targetIndex;
            //Move the camera up in Y by the cameras height to the desired camera zone.
            mainCam.transform.position = new Vector3(mainCam.transform.position.x, (((mainCam.orthographicSize * 2) - 1) * zoneIndex), mainCam.transform.position.z);
        }
        else {
            Debug.Log("Camera already in Target Zone Index");
        }
    }

    public void InstantiateZones() {
        Debug.Log("Creating Zones");
       
        Vector3 topPosition = mainCam.ViewportToWorldPoint(new Vector3(0.5f, 1.01f, 10f));
        topZone = Instantiate(zonePrefab, topPosition, Quaternion.identity, gameObject.transform);
        topZone.gameObject.name = "topZone";
        topZone.GetComponent<ZoneMechanics>().top = true;

        Vector3 botPosition = mainCam.ViewportToWorldPoint(new Vector3(0.5f, -0.01f, 10f));
        botZone = Instantiate(zonePrefab, botPosition, Quaternion.identity, gameObject.transform);
        botZone.gameObject.name = "botZone";
        botZone.GetComponent<ZoneMechanics>().top = false;
    }
    
}
