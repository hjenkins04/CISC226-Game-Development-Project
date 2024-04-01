using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatTriggerCollision : MonoBehaviour
{
    private FlyingEnemy parentEnemy;
    [SerializeField] public float damage = 3f;
    [SerializeField] private float attackCooldown = 1.5f;
    private float lastAttackTime = 0f;

    void Start()
    {
        parentEnemy = GetComponentInParent<FlyingEnemy>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (Time.time - lastAttackTime >= attackCooldown) // Check if cooldown has elapsed
        {
            if (other.CompareTag("Player"))
            {
                PlayerStats playerStats = other.GetComponent<PlayerStats>();
                playerStats.health -= damage;
                lastAttackTime = Time.time;
            }
        }
        if (parentEnemy.isPatrolling && other.CompareTag("EnemyMovementCollider")) {
            parentEnemy.RotateEnemy();
        }
    }
}
