using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth;
    public float health;

    float coins;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health == 0)
        {
            die();
        }
    }

    void die()
    {
        //respawn at checkpoint

        coins = 0;
    }
}
