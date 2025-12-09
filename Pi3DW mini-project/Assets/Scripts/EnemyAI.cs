using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    public Transform[] points;
    public bool randomizeStartPoint = false;
    public int startIndex = 0;
    public bool reversePath = false;

    private int currentPoint = 0;
    private NavMeshAgent agent;

    public float chaseDistance = 7f;
    public Transform player;

    private Animator animator;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        if (points != null && points.Length > 0)
        {
            if (randomizeStartPoint)
            {
                currentPoint = Random.Range(0, points.Length);
            }
            else
            {
                currentPoint = Mathf.Clamp(startIndex, 0, points.Length - 1);
            }

            agent.destination = points[currentPoint].position;
        }

        if (animator != null)
        {
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsChasing", false);
        }
    }

    void Update()
    {
        if (points == null || points.Length == 0 || agent == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist < chaseDistance)
        {
            agent.destination = player.position; // Chase player

            if (animator != null)
            {
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsChasing", true);
            }
        }
        else
        {
            Patrol(); // Patrol between points
            if (animator != null)
            {
                animator.SetBool("IsChasing", false);
                animator.SetBool("IsWalking", true);
            }
        }
    }

    void Patrol()
    {
        if (agent.pathPending) return;

        if (agent.remainingDistance < 0.5f)
        {
            if (!reversePath)
            {
                currentPoint = (currentPoint + 1) % points.Length;
            }
            else
            {
                currentPoint = (currentPoint - 1 + points.Length) % points.Length;
            }
            agent.destination = points[currentPoint].position;
        }
    }    
}