using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))] // to make sure this object has an Animator componen (required for shooting animations)

public class PlayerShooting : MonoBehaviour
{

    [SerializeField]
    private bool AddBulletSpread = true; // to add small random variation to the bullet direction, so the shot doesn't always hit the exact point I click
    [SerializeField]
    private Vector3 BulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField]
    private ParticleSystem ShootingSystem; // Particle system for muzzle flash / shooting vfx
    [SerializeField]
    private Transform BulletSpawnPoint; // Position from which the bullet is fired
    [SerializeField]
    private GameObject ImpactEffectPrefab; // Effect spawned where the bullet hits (impact and sparks)
    [SerializeField]
    private TrailRenderer BulletTrail; // Visual trail that follows the bullet's path
    [SerializeField] 
    private float ShootDelay = 0.5f; // Delay between shots
    [SerializeField]
    private LayerMask Mask = ~0; // Which layers the raycast can interact with
    private Animator Animator; // Animator for playing shooting animations
    private float LastShootTime; //// Saves the time the last shot happened to control how fast the player shoots

    private void Awake()
    {
        Animator = GetComponent<Animator>(); /// Get the Animator component on this GameObject

    }
private void Update()
    {
        // Left mouse button pressed → fire a shot
        if (Input.GetMouseButtonDown(0))  
        {
            Shoot();
            Debug.Log("Shoot called");

        }
    }
    
    /// <summary>
    /// Attempts to fire a shot. Checks fire rate, plays animations,
    /// casts the ray, spawns effects, and damages enemies.
    /// </summary>
    public void Shoot()
    {
    
        // Stops the player from shooting again if the delay hasn't passed yet

        if (LastShootTime + ShootDelay > Time.time) 
            return;

        LastShootTime = Time.time;
        
        // Play the shooting animation if the Animator exists
        if (Animator != null)
            Animator.SetBool("IsShooting", true);
            
        // Play muzzle flash particle effect
        if (ShootingSystem != null)
            ShootingSystem.Play();

        Vector3 direction = GetDirection(); // Calculate bullet direction
        
        // Cast a ray to detect what the bullet hits
        if (Physics.Raycast(BulletSpawnPoint.position, direction, out RaycastHit hit, Mathf.Infinity, Mask))
        {
            Debug.Log("Shot hit: " + hit.collider.name);


            // Create the bullet trail visual
            if (BulletTrail != null)
            {
                TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity);
                StartCoroutine(SimpleTrail(trail, hit.point));
            }

            // Spawn impact effect at the hit location
            if (ImpactEffectPrefab != null)
            {
                Instantiate(ImpactEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            }

            // If the hit object has an EnemyHealth component, damage it
            EnemyHealth enemy = hit.collider.GetComponentInParent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
            }
        }
        
        // Turns off the shooting animation after firing (if Animator exists)
        if (Animator != null)
            Animator.SetBool("IsShooting", false);
    }

    /// <summary>
    /// Calculates bullet direction, adding random spread if enabled.
    /// </summary>
    private Vector3 GetDirection()
    {
        Vector3 direction = transform.forward; // base direction is forward from the shooting object

        // Add random inaccuracy to simulate weapon spread
        if (AddBulletSpread)
        {
            direction += new Vector3(
                Random.Range(-BulletSpreadVariance.x, BulletSpreadVariance.x),
                Random.Range(-BulletSpreadVariance.y, BulletSpreadVariance.y),
                Random.Range(-BulletSpreadVariance.z, BulletSpreadVariance.z)
            );

            direction.Normalize();
        }

        return direction;
    }

     /// <summary>
    /// Moves the bullet trail from the spawn point to the hit point.
    /// </summary>
    private IEnumerator SimpleTrail(TrailRenderer trail, Vector3 hitPoint)
    {
        if (trail == null)
            yield break;

        Vector3 startPosition = trail.transform.position;
        float t = 0f;
        float duration = 0.05f;
        
        // Slowly animates the trail using Lerp (linear interpolation) toward the hit position until it reaches it
        while (t < 1f && trail != null)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hitPoint, t);
            t += Time.deltaTime / duration;
            yield return null;
        }

        if (trail != null)
        {
            // Make sure the trail ends at the hit point and then remove it after it fades
            trail.transform.position = hitPoint;
            Destroy(trail.gameObject, trail.time);
        }
    }

}
