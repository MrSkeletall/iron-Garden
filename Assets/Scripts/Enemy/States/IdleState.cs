using UnityEngine;
using UnityEngine.AI;

public class IdleState : EnemyState
{
    private EnemyManager enemyManager;
    private NavMeshAgent navAgent;
    private float idleTimer = 0f;
    private float idleDuration = 2f;

    public void OnStateEnter()
    {
        navAgent = enemyManager.GetNavMeshAgent();
        navAgent.isStopped = true;
        idleTimer = 0f;
        Debug.Log("Enemy entered Idle state");
    }

    public void OnStateExit()
    {
        navAgent.isStopped = false;
    }

    public void OnStateUpdate()
    {
        idleTimer += Time.deltaTime;

        // Check for player in detection range
        if (enemyManager.CanSeePlayer())
        {
            enemyManager.SendStateChange(new ChaseState());
            return;
        }

        // After idle duration, start patrolling
        if (idleTimer >= idleDuration)
        {
            enemyManager.SendStateChange(new PatrolState());
        }
    }

    public void SetEnemyManager(EnemyManager manager)
    {
        enemyManager = manager;
    }
}
