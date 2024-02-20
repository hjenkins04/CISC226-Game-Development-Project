using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingEnemy : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float detectionRange = 1f;
    private bool isPatrolling = true;

    private Transform player;
    private AIPath aiPath;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        aiPath = GetComponent<AIPath>();
        aiPath.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) <= detectionRange) {
            isPatrolling = false;
            aiPath.enabled = true;
            Debug.Log("in range!");
        }

        if (isPatrolling) {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("EnemyMovementCollider") && isPatrolling) {
            Vector3 newRotation = transform.eulerAngles;
            newRotation.y += 180f;
            transform.eulerAngles = newRotation;
        }
    }
}
