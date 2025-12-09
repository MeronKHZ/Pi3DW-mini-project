using UnityEngine;

/// <summary>
/// Handles enemy health, taking damage, death animation,
/// spawning death effects, and destroying the enemy.
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    public int health = 3; //How much health the enemy starts with
    public GameObject deathEffect; //Effect to play when the enemy dies (smoke effect)
    private Animator animator; //Animator used for playing the death animation

    private void Start()
    {
        // Get the Animator component from the child model (the zombie mesh)
        animator = GetComponentInChildren<Animator>();
    }

    // <summary>
    /// Reduces enemy health by the given amount.
    /// If health reaches zero, the enemy dies.
    /// </summary>
    public void TakeDamage(int amount)
    {
        health -= amount; /// Subtract health
        Debug.Log("Enemy took damage. Current health: " + health);

        if (health <= 0)
        {
            Debug.Log("Calling Die() on " + name);
            Die(); // Kill the enemy
        }
    }

    /// <summary>
    /// Plays death animation, spawns death effect, and removes enemy.
    /// </summary>
    void Die()
    {
        Debug.Log(name + " died.");

        if (animator != null)
        {
            animator.SetTrigger("Die"); //Trigger death animation
            Debug.Log("Die trigger sent!");
        }

        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, transform.rotation); // Spawn the death VFX at the enemy's position
        }
        Destroy(gameObject, 1.5f); // Destroy enemy object after a short delay to allow animation/VFX
    }
}
