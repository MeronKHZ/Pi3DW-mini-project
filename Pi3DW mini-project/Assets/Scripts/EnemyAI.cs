using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    public Transform[] points;
    private int currentPoint = 0;
    private NavMeshAgent agent;
    public float chaseDistance = 7f;
    public Transform player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = points[0].position;
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist < chaseDistance)
        {
            agent.destination = player.position; // Chase player
        }
        else
        {
            Patrol(); // Patrol between points
        }
    }

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentPoint = (currentPoint + 1) % points.Length;
            agent.destination = points[currentPoint].position;
        }
    }
}