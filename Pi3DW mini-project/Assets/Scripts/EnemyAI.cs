using UnityEngine;
using UnityEngine.AI; //NavMesh and pathfinding system

/// <summary>
/// Controls enemy patrol and chase behavior using a NavMeshAgent.
/// Enemy patrols between points and chases the player when close enough.
/// </summary>
public class EnemyAI : MonoBehaviour
{
    public Transform[] points; //Patrol points that the enemy moves between
    public bool randomizeStartPoint = false; //Randomize the initial patrol point
    public int startIndex = 0; // Manual starting index if not randomized
    public bool reversePath = false; // If true, enemy walks backward through the patrol route

    private int currentPoint = 0; //Tracks which patrol point the enemy is currently moving toward
    private NavMeshAgent agent; // NavMeshAgent used for movement and pathfinding

    public float chaseDistance = 7f; //distance at which the enemy stops patrolling and starts chasing the player
    public Transform player;  // Reference to the player's transform

    private Animator animator; // Animator controlling walking and chasing animations


    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Get NavMeshAgent from this GameObject
        animator = GetComponentInChildren<Animator>(); // Animator is on the child model
        
        // Set starting patrol point
        if (points != null && points.Length > 0)
        {
            if (randomizeStartPoint)
            {
                currentPoint = Random.Range(0, points.Length); // Choose random start
            }
            else
            {
                currentPoint = Mathf.Clamp(startIndex, 0, points.Length - 1); //Use specified index
            }

            agent.destination = points[currentPoint].position; // Move to the first patrol point
        }

        // Initialize animation states
        if (animator != null)
        {
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsChasing", false);
        }
    }

    void Update()
    {

        if (points == null || points.Length == 0 || agent == null) return; // to make everything is valid before updating
        
        float dist = Vector3.Distance(transform.position, player.position); // Distance from enemy to player

        
        // Chase behavior
        if (dist < chaseDistance)
        {
            agent.destination = player.position; // Move toward the player

            if (animator != null)
            {
                animator.SetBool("IsWalking", false); // Stop walking animation
                animator.SetBool("IsChasing", true); // Play chasing animation
            }
        }
        else
        {
            // Patrol when not chasing
            Patrol(); 
            if (animator != null)
            {
                animator.SetBool("IsChasing", false); // Stop chasing animation
                animator.SetBool("IsWalking", true); // Play walking animation
            }
        }
    }
    
    /// <summary>
    /// Moves the enemy along the patrol route, switching to the next point when close enough.
    /// </summary>
    void Patrol()
    {
        if (agent.pathPending) return; //Wait until path is fully calculated
        
        // If close to the current point, move to the next one
        if (agent.remainingDistance < 0.5f)
        {
            if (!reversePath)
            {
                currentPoint = (currentPoint + 1) % points.Length; // Move forward in the array
            }
            else
            {
                currentPoint = (currentPoint - 1 + points.Length) % points.Length; //Move backwards in the array

            }
            agent.destination = points[currentPoint].position; // New patrol target
        }
    }    
}
