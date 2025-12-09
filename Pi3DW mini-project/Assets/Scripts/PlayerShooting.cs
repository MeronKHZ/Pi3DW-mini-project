using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class PlayerShooting : MonoBehaviour
{

    [SerializeField]
    private bool AddBulletSpread = true;
    [SerializeField]
    private Vector3 BulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField]
    private ParticleSystem ShootingSystem;
    [SerializeField]
    private Transform BulletSpawnPoint;
    [SerializeField]
    private GameObject ImpactEffectPrefab;
    [SerializeField]
    private TrailRenderer BulletTrail;
    [SerializeField] 
    private float ShootDelay = 0.5f;
    [SerializeField]
    private LayerMask Mask = ~0;
    private Animator Animator;
    private float LastShootTime;

    private void Awake()
    {
        Animator = GetComponent<Animator>();

    }
private void Update()
    {
        if (Input.GetMouseButtonDown(0))  
        {
            Shoot();
            Debug.Log("Shoot called");

        }
    }

    public void Shoot()
    {
        if (LastShootTime + ShootDelay > Time.time)
            return;

        LastShootTime = Time.time;

        if (Animator != null)
            Animator.SetBool("IsShooting", true);

        if (ShootingSystem != null)
            ShootingSystem.Play();

        Vector3 direction = GetDirection();

        if (Physics.Raycast(BulletSpawnPoint.position, direction, out RaycastHit hit, Mathf.Infinity, Mask))
        {
            Debug.Log("Shot hit: " + hit.collider.name);


            if (BulletTrail != null)
            {
                TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity);
                StartCoroutine(SimpleTrail(trail, hit.point));
            }


            if (ImpactEffectPrefab != null)
            {
                Instantiate(ImpactEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            }

            EnemyHealth enemy = hit.collider.GetComponentInParent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
            }
        }

        if (Animator != null)
            Animator.SetBool("IsShooting", false);
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = transform.forward;

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
    private IEnumerator SimpleTrail(TrailRenderer trail, Vector3 hitPoint)
    {
        if (trail == null)
            yield break;

        Vector3 startPosition = trail.transform.position;
        float t = 0f;
        float duration = 0.05f;

        while (t < 1f && trail != null)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hitPoint, t);
            t += Time.deltaTime / duration;
            yield return null;
        }

        if (trail != null)
        {
            trail.transform.position = hitPoint;
            Destroy(trail.gameObject, trail.time);
        }
    }

}
