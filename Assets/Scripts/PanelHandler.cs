using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelHandler : MonoBehaviour
{
    // Start is called before the first frame update

    
 public GameObject panel;
    private bool isPanelVisible = false;

   

    void Update()
    {
        // Check if the designated key is pressed
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     // Toggle the visibility of the panel
        //     if (isPanelVisible)
        //     {
        //         HidePanel();
        //     }
        //     else
        //     {
        //         ShowPanel();
        //     }
        // }

        if (TextVisibilty.interactable == false){
            HidePanel();
        }
        else{
            ShowPanel();
        }
    }

    void ShowPanel()
    {
        // Show the panel
        panel.SetActive(true);
        isPanelVisible = true;
    }

    void HidePanel()
    {
        // Hide the panel
        panel.SetActive(false);
        isPanelVisible = false;
    }
}
