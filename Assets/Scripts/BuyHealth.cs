using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyHealth : MonoBehaviour
{

    public GameObject playerObject;

    private bool buttonClicked = false;

    void Update(){
        PlayerStats playerStats = playerObject.GetComponent<PlayerStats>();
        GetComponent<Button>().onClick.AddListener(OnClick);
         if (buttonClicked)
        {
            // Perform actions when the button is clicked
            Debug.Log("Button is clicked!");
            // Reset buttonClicked to false after performing actions

            if (playerStats != null)
        {

            if (playerStats.coins >= 3.0f){
                playerStats.coins-=3.0f;
                playerStats.health+=1.0f;
            }
            // Increase player's health

            // Optionally, you can update UI elements to reflect changes
        }

            buttonClicked = false;
        }
        

    }
    private void OnClick()
    {
        buttonClicked = true;
        // Get the PlayerStats component attached to the player object

    }
}
