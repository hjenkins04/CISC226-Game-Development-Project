using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextVisibilty : MonoBehaviour
{

    
    public Transform playerTransform;
    public Transform chestTransform;
    public float proximityDistance = 3f;
    public TextMeshProUGUI displayText;

    public bool interactable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null && chestTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(playerTransform.position, chestTransform.position);

            // Check if the player is close to the chest
            if (distanceToPlayer <= proximityDistance)
            {
                // Show the text
                displayText.text = "Press \"E\" to interact";
            }
            else
            {
                // Hide the text
                displayText.text = "";
            }
        }
        
    }
}
