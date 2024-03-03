using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{


    public GameObject Panel;
    public static bool settingVisible;
    // Start is called before the first frame update
    void Start()
    {
        settingVisible = false;
    }


    public void OpenPanel(){
        if (Panel != null){
            Panel.SetActive(true);
            settingVisible = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (settingVisible == true){
               Panel.SetActive(false);
               settingVisible = false;
            }
        }
    }
}
