using UnityEngine;
using UnityEngine.AI;

public class ChaseState : EnemyState
{
    private EnemyManager enemyManager;
    private NavMeshAgent navAgent;
    private Transform player;
    private float lostPlayerTimer = 0f;
    private float lostPlayerDuration = 3f;

    public void OnStateEnter()
    {
        navAgent = enemyManager.GetNavMeshAgent();
        player = enemyManager.GetPlayerTransform();
        lostPlayerTimer = 0f;
        Debug.Log("Enemy entered Chase state");
    }

    public void OnStateExit()
    {
    }

    public void OnStateUpdate()
    {
        if (player == null)
        {
            enemyManager.SendStateChange(new IdleState());
            return;
        }

        // Check if in attack range
        float distanceToPlayer = Vector3.Distance(enemyManager.transform.position, player.position);
        if (distanceToPlayer <= enemyManager.attackRange)
        {
            enemyManager.SendStateChange(new AttackState());
            return;
        }

        // Check if can still see player
        if (enemyManager.CanSeePlayer())
        {
            lostPlayerTimer = 0f;
            navAgent.SetDestination(player.position);
        }
        else
        {
            lostPlayerTimer += Time.deltaTime;
            if (lostPlayerTimer >= lostPlayerDuration)
            {
                enemyManager.SendStateChange(new IdleState());
            }
        }
    }

    public void SetEnemyManager(EnemyManager manager)
    {
        enemyManager = manager;
    }
}
