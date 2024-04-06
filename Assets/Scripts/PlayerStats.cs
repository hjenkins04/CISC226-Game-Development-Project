using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FrostFalls;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth;
    public float health;

    public TextMeshProUGUI coinText;
    public float deathAnimationDuration = 1.5f;
    public float coins = 0;
    public PlayerController playerController;
    public GameObject checkpointContollerObject;
    private CheckpointController checkpointController;
    private Animator _anim;


    void Start()
    {
        health = maxHealth;
        checkpointController = checkpointContollerObject.GetComponent<CheckpointController>();
        _anim = GetComponentInChildren<Animator>();
        playerController = GetComponent<PlayerController>();
    }


    void Update()
    {
        _anim.SetBool("Dead", playerController.IsPlayerDead);

        if (health <= 0 && !playerController.IsPlayerDead)
        {
            StartCoroutine(DieAfterAnimation());
        }

        int coinsInt = (int)coins;

        if (coinText != null){
            coinText.text = coinsInt.ToString();
        }
    }

    IEnumerator DieAfterAnimation()
    {
        _anim.SetTrigger("Die");

        playerController.IsPlayerDead = true;

        // Wait for death animation to complete
        yield return new WaitForSeconds(deathAnimationDuration);

        Die();
    }

    private void Die()
    {
        //respawn at checkpoint
        playerController.IsPlayerDead = false;
        checkpointController.RespawnAtLatestCheckpoint();

        coins = 0;
        health = maxHealth;
    }
}
