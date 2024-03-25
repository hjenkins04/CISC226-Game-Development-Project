using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingEnemy : MonoBehaviour
{

    [SerializeField] public float moveSpeed = 3f;
    [SerializeField] public float detectionRange = 1f;
    [SerializeField] public float attackRange = 1f;
    public bool isPatrolling = true;

    private Transform player;
    public Transform playerHead;
    public Transform batDivePosition;
    private AIPath aiPath;

    [SerializeField] private Animator _anim;
    public bool attacking = false;
    private bool following = false;
    [SerializeField] public float attackDelay;
    
    [SerializeField] public GameObject enemyMovementColliderPrefab;
    private GameObject leftCollider;
    private GameObject rightCollider;
    private float horizontalOffset = 5f;
    private float verticalOffset = 5f;

    // Start is called before the first frame update
    void Start()
    {
        CreateMovementColliders();
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

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void CreateMovementColliders() {
        GameObject colliderParent = GameObject.Find("MovementColliders");

        Vector3 leftPosition = transform.position - transform.right * horizontalOffset + Vector3.up * verticalOffset;
        Vector3 rightPosition = transform.position + transform.right * horizontalOffset + Vector3.up * verticalOffset;

        leftCollider = Instantiate(enemyMovementColliderPrefab, leftPosition, Quaternion.identity, colliderParent.transform);
        rightCollider = Instantiate(enemyMovementColliderPrefab, rightPosition, Quaternion.identity, colliderParent.transform);
    }

    public void RotateEnemy() {
        Vector3 newRotation = transform.eulerAngles;
        newRotation.y += 180f;
        transform.eulerAngles = newRotation;
    }
}
