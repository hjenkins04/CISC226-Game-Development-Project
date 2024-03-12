using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems; // Required for event handling
using UnityEngine;

public class SoundOnHover : MonoBehaviour, IPointerEnterHandler
{

    private AudioSource sfxSource; // AudioSource reference

    // Start is called before the first frame update
    void Start()
    {   
        GameObject sfxSourceObj = GameObject.Find("SFX Source") ?? GameObject.FindGameObjectWithTag("SFXSourceTag");
        if (sfxSourceObj != null)
        {
            sfxSource = sfxSourceObj.GetComponent<AudioSource>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        sfxSource?.Play(); // Play the sound if sfxSource is not null
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
