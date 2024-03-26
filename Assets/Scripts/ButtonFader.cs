using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFader : MonoBehaviour
{
   // Reference to the button
   private Button button;

    void Start()
    {
        // Get the Button component attached to the GameObject
        button = GetComponent<Button>();

        // Add a listener to the button's onClick event
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        // Disable the button
        button.interactable = false;

        // Optionally, change the button's color to grey
        var colors = button.colors;
        colors.normalColor = Color.grey;
        button.colors = colors;
    }
}
