using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenu : MonoBehaviour
{

     public GameObject Panel;
    public static bool controlVisible;
    // Start is called before the first frame update

    public void OpenPanel(){
        if (Panel != null){
            Panel.SetActive(true);
            controlVisible = true;
        }
    }
    void Start()
    {
        controlVisible=false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKey(KeyCode.P))
        {
            if (controlVisible == true){
               Panel.SetActive(false);
               controlVisible = false;
            }
        }
        
    }
    // Start is called before the first frame update
}
