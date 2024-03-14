using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelHandler : MonoBehaviour
{
    public Canvas canvas;
    public GameObject panel;
    private bool isPanelVisible = false;

    // Reference to an instance of TextVisibility
    public TextVisibilty textVisibiltyScript;

    void Update()
    {
        // Check if the textVisibilityScript reference is not null
        if (textVisibiltyScript != null)
        {
            // Access the interactable property directly
            if (!textVisibiltyScript.interactable)
            {
                HidePanel();
            }
            else
            {
                ShowPanel();
            }
        }
    }

    void ShowPanel()
    {
        // Show the panel
        panel.SetActive(true);
        isPanelVisible = true;
        canvas.sortingOrder = 1;
    }

    void HidePanel()
    {
        // Hide the panel
        panel.SetActive(false);
        isPanelVisible = false;
        canvas.sortingOrder = 0;
    }
}