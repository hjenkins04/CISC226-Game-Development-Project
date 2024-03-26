using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyStats : MonoBehaviour
{
    public float maxHealth;
    public float health;

    public float coinDrop;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
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
        Destroy(enemy);
        //Drop coins
        GameObject.Find("Player").GetComponent<PlayerStats>().coins += coinDrop;
    }
}