using UnityEngine;
using UnityEngine.AI;

public class AttackState : EnemyState
{
    private EnemyManager enemyManager;
    private NavMeshAgent navAgent;
    private Transform player;
    private float attackTimer = 0f;
    private float attackCooldown = 1.5f;

    public void OnStateEnter()
    {
        navAgent = enemyManager.GetNavMeshAgent();
        navAgent.isStopped = true;
        player = enemyManager.GetPlayerTransform();
        attackTimer = 0f;
        Debug.Log("Enemy entered Attack state");
    }

    public void OnStateExit()
    {
        navAgent.isStopped = false;
    }

    public void OnStateUpdate()
    {
        if (player == null)
        {
            enemyManager.SendStateChange(new IdleState());
            return;
        }

        float distanceToPlayer = Vector3.Distance(enemyManager.transform.position, player.position);

        // If player is out of attack range, chase them
        if (distanceToPlayer > enemyManager.attackRange)
        {
            enemyManager.SendStateChange(new ChaseState());
            return;
        }

        // Face the player
        Vector3 directionToPlayer = (player.position - enemyManager.transform.position).normalized;
        directionToPlayer.y = 0;
        if (directionToPlayer != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        // Attack timer
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCooldown)
        {
            PerformAttack();
            attackTimer = 0f;
        }
    }

    void PerformAttack()
    {
        Debug.Log("Enemy attacks player!");
        // Implement attack logic here (damage player, play animation, etc.)
    }

    public void SetEnemyManager(EnemyManager manager)
    {
        enemyManager = manager;
    }
}
