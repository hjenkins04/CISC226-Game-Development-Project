using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth;
    public float health;

     public TextMeshProUGUI coinText; // Reference to your TextMeshPro object

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
        if(health <= 0)
        {
            die();
        }

        int coinsInt = (int)coins;

        if (coinText != null){
            coinText.text = coinsInt.ToString();
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
