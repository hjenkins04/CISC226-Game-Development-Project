using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSolidCollision : MonoBehaviour
{
    private FlyingEnemy parentEnemy;

    void Start()
    {
        parentEnemy = GetComponentInParent<FlyingEnemy>();    
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.CompareTag("Platform") && parentEnemy.isPatrolling) {
            parentEnemy.RotateEnemy();
        }
    }
}
