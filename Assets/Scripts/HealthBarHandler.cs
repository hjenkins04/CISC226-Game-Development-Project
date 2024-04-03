using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider healthSlider; // Reference to your slider
    public GameObject playerObject; // Reference to the player object in the scene

    void Update()
    {
        if (playerObject != null)
        {
            PlayerStats playerStats = playerObject.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                // Get player health from the PlayerController script
                float playerHealth = playerStats.health;
                playerHealth = Mathf.Clamp(playerHealth, 0f, playerStats.maxHealth);
                // Update the slider value with the player's health
                if (healthSlider != null)
                {
                    healthSlider.value = playerHealth;
                }
            }
        }
    }
}
