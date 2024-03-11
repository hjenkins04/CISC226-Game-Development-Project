using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string sceneName)
    {
        if (!ControlsMenu.controlVisible && !SettingsMenu.settingVisible){
            SceneManager.LoadScene(sceneName);
            Time.timeScale = 1;
        }
        
    }
}
