using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatTriggerCollision : MonoBehaviour
{
    private FlyingEnemy parentEnemy;

    void Start()
    {
        parentEnemy = GetComponentInParent<FlyingEnemy>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (parentEnemy.isPatrolling && other.CompareTag("EnemyMovementCollider")) {
            parentEnemy.RotateEnemy();
        }
    }
}
