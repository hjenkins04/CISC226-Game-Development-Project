using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 5f;
    public float fireRate = 2f;
    public float moveSpeed = 2f;

    private Transform player;

    private float nextFireTime;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;
            FireProjectile();
        }
    }

    void FireProjectile()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("EnemyMovementCollider")) {
            Vector3 newRotation = transform.eulerAngles;
            newRotation.y += 180f;
            transform.eulerAngles = newRotation;
        }
    }
}