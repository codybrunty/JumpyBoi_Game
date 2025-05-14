using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
<summary> Trigger area that sets the win state.
*/

public class WinMechanic : MonoBehaviour{

    [SerializeField] GameObject winMessage = default;
    [SerializeField] PlayerControler player = default;
    [SerializeField] Button rewind = default;
    StatsService statsService;

    private void Start() {
        statsService = ServiceLocator.GetService<StatsService>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.name == "Grounded") {
            Debug.Log("You Win! It took " + statsService.GetStat("Jumps") + "jumps to get to the top.");
            player.touchEnabled = false;
            rewind.interactable = false;
            winMessage.SetActive(true);
        }
    }


}
