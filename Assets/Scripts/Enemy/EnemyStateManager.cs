using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    private EnemyState currentState;
    private EnemyManager enemyManager;

    private void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        EnterState(new PatrolState());
    }

    void EnterState(EnemyState state)
    {
        currentState = state;
        currentState.SetEnemyManager(enemyManager);
        currentState.OnStateEnter();
    }

    void ExitState(EnemyState state)
    {
        currentState.OnStateExit();
    }

    void UpdateState()
    {
        currentState.OnStateUpdate();
    }

    public void ChangeState(EnemyState state)
    {
        ExitState(currentState);
        EnterState(state);
        Debug.Log($"Enemy changed to {state.GetType().Name}");
    }

    void Update()
    {
        UpdateState();
    }
}
