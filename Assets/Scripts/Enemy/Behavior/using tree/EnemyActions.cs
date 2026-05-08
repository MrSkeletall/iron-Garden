using UnityEngine;
using UnityEngine.AI;

public class EnemyActions : MonoBehaviour
{
    public GameObject player;
    public EnemyShooting enemyShooting;
    public float wanderRange = 3f;
    public LayerMask obstacleLayer; // Set this to layers that obstruct vision and provide cover
    public float coverSearchRange = 15f; // Range to search for cover objects
    public float coverDistance = 2f; // Distance to position behind the cover object

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (enemyShooting == null)
        {
            enemyShooting = GetComponent<EnemyShooting>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetRandomNavMeshPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRange;
        randomDirection += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, wanderRange, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return transform.position;
    }

    public Vector3 FindMoveToPlayerPosition()
    {
        if (player == null)
        {
            return transform.position;
        }

        // Calculate direction towards the player
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

        // Move in the direction of the player within wander distance
        Vector3 targetPos = transform.position + directionToPlayer * wanderRange;

        // Sample this position on the NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPos, out hit, wanderRange, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return transform.position;
    }

    public Vector3 FindCoverPosition()
    {
        // Find all objects on the obstacle layer within search range
        Collider[] coverObjects = Physics.OverlapSphere(transform.position, coverSearchRange, obstacleLayer);
        
        if (coverObjects.Length == 0)
        {
            return transform.position; // Fallback if no cover found
        }

        // Try to find a valid position near each cover object
        foreach (Collider coverCollider in coverObjects)
        {
            Vector3 coverPosition = FindPositionNearCover(coverCollider);
            
            if (coverPosition != Vector3.zero)
            {
                return coverPosition;
            }
        }

        return transform.position; // Fallback
    }

    /// <summary>
    /// Finds a position near the given cover object on the NavMesh
    /// </summary>
    private Vector3 FindPositionNearCover(Collider coverCollider)
    {
        // Get a position near the cover object
        Vector3 coverPos = coverCollider.bounds.center + Random.insideUnitSphere * coverDistance;

        // Sample this position on the NavMesh
        NavMeshHit navHit;
        if (NavMesh.SamplePosition(coverPos, out navHit, coverDistance * 2, NavMesh.AllAreas))
        {
            return navHit.position;
        }

        return Vector3.zero; // Invalid position
    }

    /// <summary>
    /// Checks if there is an obstacle obstructing the enemy's view of the player
    /// </summary>
    public bool CanSeePlayer()
    {
        if (player == null)
        {
            return true; // Cannot see player if player doesn't exist
        }

        // Calculate direction from enemy to player
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        // Raycast from enemy to player
        Ray ray = new Ray(transform.position, directionToPlayer.normalized);
        
        // Check if raycast hits an obstacle before reaching the player
        if (Physics.Raycast(ray, distanceToPlayer, obstacleLayer))
        {
            return true; // Player is obstructed
        }

        return false; // Player is visible
    }

    public void ShootAtPlayer()
    {
        if (enemyShooting != null)
        {
            enemyShooting.ShootAtPlayer();
        }
    }
}
