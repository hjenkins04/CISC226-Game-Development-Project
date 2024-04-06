using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProximityInteraction : MonoBehaviour
{
    public Transform playerTransform;
    public Transform chestTransform;
    public float proximityDistance = 3f;
    public Image displayImage; // Reference to the UI Image
    public Image displayBtn;

    public bool interactable;

    void Start()
    {
        interactable = false;
        displayImage.enabled = false; // Initially hide the image
        if (displayBtn != null)
        {
            displayBtn.enabled = false;
        }
    }

    void Update()
    {
        if (playerTransform != null && chestTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(playerTransform.position, chestTransform.position);

            // Check if the player is close to the chest
            if (distanceToPlayer <= proximityDistance)
            {
                // Show the image
                displayImage.enabled = true;

                if (displayBtn != null) 
                { 
                    displayBtn.enabled = true; 
                }

                // Check for player interaction
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable = !interactable;
                    print(interactable); // For debugging
                }
            }
            else
            {
                // Hide the image
                displayImage.enabled = false;

                if (displayBtn != null)
                {
                    displayBtn.enabled = false;
                }

                // Optionally, reset interactable state when the player is far away
                if (distanceToPlayer > 20f)
                {
                    interactable = false;
                }
            }
        }
    }
}
