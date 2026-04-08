using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private EnemyStateManager enemyStateManager;
    private Transform player;

    [Header("Detection Settings")]
    public float detectionRange = 15f;
    public float fieldOfView = 120f;
    public LayerMask playerLayer;
    public LayerMask obstructionLayer;

    [Header("Combat Settings")]
    public float attackRange = 2f;
    public float attackDamage = 10f;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyStateManager = GetComponent<EnemyStateManager>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public NavMeshAgent GetNavMeshAgent()
    {
        return navMeshAgent;
    }

    public EnemyStateManager GetStateManager()
    {
        return enemyStateManager;
    }

    public Transform GetPlayerTransform()
    {
        return player;
    }

    public void SendStateChange(EnemyState newState)
    {
        enemyStateManager.ChangeState(newState);
    }

    public bool CanSeePlayer()
    {
        if (player == null)
            return false;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if player is within detection range
        if (distanceToPlayer > detectionRange)
            return false;

        // Check if player is within field of view
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        if (angleToPlayer > fieldOfView / 2f)
            return false;

        // Check for obstructions
        if (Physics.Raycast(transform.position + Vector3.up, directionToPlayer, distanceToPlayer, obstructionLayer))
            return false;

        return true;
    }

    void OnDrawGizmosSelected()
    {
        // Draw detection range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Draw attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Draw field of view
        Vector3 fovLine1 = Quaternion.AngleAxis(fieldOfView / 2f, transform.up) * transform.forward * detectionRange;
        Vector3 fovLine2 = Quaternion.AngleAxis(-fieldOfView / 2f, transform.up) * transform.forward * detectionRange;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);
    }
}
