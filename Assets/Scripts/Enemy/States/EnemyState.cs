using UnityEngine;

public interface EnemyState
{
    void OnStateEnter();
    void OnStateExit();
    void OnStateUpdate();
    void SetEnemyManager(EnemyManager enemyManager);
}
