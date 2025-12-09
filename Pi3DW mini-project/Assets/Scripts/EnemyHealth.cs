using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 3;
    public GameObject deathEffect;
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Enemy took damage. Current health: " + health);

        if (health <= 0)
        {
            Debug.Log("Calling Die() on " + name);
            Die();
        }
    }
    void Die()
    {
        Debug.Log(name + " died.");

        if (animator != null)
        {
            animator.SetTrigger("Die");
            Debug.Log("Die trigger sent!");
        }

        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
        }
        Destroy(gameObject, 1.5f);
    }
}
