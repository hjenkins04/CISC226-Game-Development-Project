using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingEnemy : MonoBehaviour
{

    [SerializeField] public float moveSpeed = 3f;
    [SerializeField] public float detectionRange = 1f;
    [SerializeField] public float attackRange = 1f;
    private bool isPatrolling = true;

    private Transform player;
    public Transform playerHead;
    public Transform batDivePosition;
    private AIPath aiPath;

    [SerializeField] private Animator _anim;
    private bool attacking = false;
    private bool following = false;
    [SerializeField] public float attackDelay;

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
        if (Vector2.Distance(transform.position, player.position) <= detectionRange && isPatrolling) {
            isPatrolling = false;
            following = true;
            aiPath.enabled = true;
            gameObject.GetComponent<AIDestinationSetter>().target = playerHead;
        }

        if (isPatrolling) {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        } else {
            FacePlayer();
            if (Vector2.Distance(transform.position, playerHead.position) <= attackRange && !attacking) {
                attacking = true;
                _anim.SetTrigger("BatAttack");
                StartCoroutine(WaitForAttackAnimation(attackDelay));

                StartCoroutine(WaitForBatDivePosition(batDivePosition));
            }
        }
    }

    IEnumerator WaitForAttackAnimation(float delay)
    {
        // Wait for the attack animation to finish
        yield return new WaitForSeconds(delay);

        // Set the destination to batDivePosition after the delay
        gameObject.GetComponent<AIDestinationSetter>().target = batDivePosition;
    }

    IEnumerator WaitForBatDivePosition(Transform batDivePosition)
    {
        // Wait until the gameObject reaches batDivePosition
        while (Vector2.Distance(transform.position, batDivePosition.position) > 0.1f)
        {
            yield return null;
        }

        // Set attacking to false and change the target back to playerHead
        attacking = false;
        gameObject.GetComponent<AIDestinationSetter>().target = playerHead;
    }

    void FacePlayer()
    {
        float directionToPlayer = player.position.x - transform.position.x;

        // Flip only if the player is on the opposite side from the facing direction
        if ((directionToPlayer > 0 && transform.localScale.x > 0) || (directionToPlayer < 0 && transform.localScale.x < 0))
        {
            Flip();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("EnemyMovementCollider") && isPatrolling) {
            Vector3 newRotation = transform.eulerAngles;
            newRotation.y += 180f;
            transform.eulerAngles = newRotation;
        }
        if (other.CompareTag("Player") && attacking) {
            Debug.Log("player hit!");
        }
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
