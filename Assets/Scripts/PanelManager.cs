using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{

    public GameObject Panel;
    
    // Start is called before the first frame update
    void Start()
    {
        OpenPanel();
    }

    public void OpenPanel(){
         if (Panel != null){
            Panel.SetActive(true);
        }

    }

    public void ClosePanel()
{
    if (Panel != null)
    {
        Panel.SetActive(false);
    }
}
    // Update is called once per frame
    void Update()
    {
        if (TextVisibilty.interactable == false){
            ClosePanel();
        }
        else{
            OpenPanel();
        }
    }
}
