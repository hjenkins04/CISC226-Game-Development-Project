using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth;
    public float health;

    public float coins = 0;
    public GameObject checkpointContollerObject;
    private CheckpointController checkpointController;
    // Start is called before the first frame update
    void Start()
    {
        checkpointController = checkpointContollerObject.GetComponent<CheckpointController>();
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

    private void die()
    {
        //respawn at checkpoint
        checkpointController.RespawnAtLatestCheckpoint();

        coins = 0;
        health = maxHealth;
    }
}
