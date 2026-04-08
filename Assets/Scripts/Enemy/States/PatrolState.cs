using UnityEngine;
using UnityEngine.AI;

public class PatrolState : EnemyState
{
    private EnemyManager enemyManager;
    private NavMeshAgent navAgent;
    private Vector3 currentPatrolPoint;
    private float patrolRadius = 10f;

    public void OnStateEnter()
    {
        navAgent = enemyManager.GetNavMeshAgent();
        SetNewPatrolPoint();
        Debug.Log("Enemy entered Patrol state");
    }

    public void OnStateExit()
    {
    }

    public void OnStateUpdate()
    {
        // Check for player in detection range
        if (enemyManager.CanSeePlayer())
        {
            enemyManager.SendStateChange(new ChaseState());
            return;
        }

        // If reached patrol point, idle briefly then set new point
        if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            enemyManager.SendStateChange(new IdleState());
        }
    }

    void SetNewPatrolPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += enemyManager.transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, NavMesh.AllAreas))
        {
            currentPatrolPoint = hit.position;
            navAgent.SetDestination(currentPatrolPoint);
        }
    }

    public void SetEnemyManager(EnemyManager manager)
    {
        enemyManager = manager;
    }
}
