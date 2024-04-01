using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The projectile animator")]
    [SerializeField] private Animator _anim;
    [Tooltip("The projectile sprite")]
    [SerializeField] SpriteRenderer snowballSpriteRenderer;
    [Tooltip("The explosion particles")]
    [SerializeField] private ParticleSystem explosionParticles;
    [Tooltip("The projectiles lifetime")]
    [SerializeField]  private float lifetime = 2f;
    [Tooltip("The player layer")]
    [SerializeField] private LayerMask playerLayer;
    [Tooltip("All layers that the projectile should impact")]
    [SerializeField]  private LayerMask impactLayers;
    [Tooltip("Projectile damage")]
    [SerializeField] public float damage;

    private bool isAlive = true;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object is the player layer
        if (IsInLayerMask(other.gameObject.layer, playerLayer) && isAlive)
        {
            isAlive = false;
            // handle player impact
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            playerStats.health = playerStats.health - damage;

            _anim.SetTrigger(ProjectileImpact);
            StartCoroutine(DestroyAfterAnimation());

        }
        // Check if the collided object belongs to one of the ImpactLayers
        else if (IsInLayerMask(other.gameObject.layer, impactLayers) && isAlive)
        {
            isAlive = false;
            _anim.SetTrigger(ProjectileImpact);
            StartCoroutine(DestroyAfterAnimation());
        }
    }

    IEnumerator DestroyAfterAnimation()
    {
        // Hide the snowball sprite
        snowballSpriteRenderer.enabled = false;

        // Play the particle effect
        explosionParticles.Play();

        // Wait for the particle system
        yield return new WaitForSeconds(explosionParticles.main.duration);

        // Destroy the projectile
        Destroy(gameObject);
    }

    // Helper method to check if a layer is within a given LayerMask
    private bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }

    private static readonly int ProjectileImpact = Animator.StringToHash("ProjectileImpact");

}