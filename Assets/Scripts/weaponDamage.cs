using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponDamage : MonoBehaviour
{
    public float damage = 1;
    public float knockbackForce;
    public float jumpForce = 3f;

    public GameObject player;

    Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        // Check if the trigger collision involves the enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("Enemy Hit");
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();

            enemyStats.health = enemyStats.health - damage;

            //Enemy Damage Flash
            other.gameObject.GetComponent<DamageFlash>()?.FlashDamage();

            _rb = other.GetComponent<Rigidbody2D>();

            Vector2 directionToYeti = transform.position - player.transform.position;
            directionToYeti.Normalize();

            Vector2 knockback = new Vector2(directionToYeti.x * knockbackForce, jumpForce);
            _rb.velocity = knockback;
        }
    }
}
