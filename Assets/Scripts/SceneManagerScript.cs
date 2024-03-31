using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadScene(string sceneName){
        if (!ControlsMenu.controlVisible && !SettingsMenu.settingVisible ){
            SceneManager.LoadScene(sceneName);
            // uncomment below once certain nohting causing error
            Time.timeScale = 1f;
        }
        
    }
}
