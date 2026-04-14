using UnityEngine;
using UnityEngine.AI;

public class EnemyActions : MonoBehaviour
{
    public GameObject player;
    public EnemyShooting enemyShooting;
    public float wanderRange = 10f;
    public LayerMask obstacleLayer; // Set this to layers that obstruct vision

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

    public Vector3 FindCoverPosition()
    {
        // Simple implementation: try to find a position behind an obstacle
        for (int i = 0; i < 10; i++) // Try 10 times
        {
            Vector3 randomPos = GetRandomNavMeshPosition();
            if (IsInCover(randomPos))
            {
                return randomPos;
            }
        }
        return transform.position; // Fallback
    }

    private bool IsInCover(Vector3 position)
    {
        // Check if there's an obstacle between player and position
        Vector3 direction = position - player.transform.position;
        Ray ray = new Ray(player.transform.position, direction.normalized);
        float distance = direction.magnitude;
        if (Physics.Raycast(ray, distance, obstacleLayer))
        {
            return true;
        }
        return false;
    }

    public void ShootAtPlayer()
    {
        if (enemyShooting != null)
        {
            enemyShooting.ShootAtPlayer();
        }
    }
}
