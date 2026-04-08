using System;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    
    
    
    
    private PlayerState currentState;
    
    private PlayerManager playerManager;

    private void Start()
    {
        
        playerManager = GetComponent<PlayerManager>();
        EnterState(new GroundState());
    }


    void EnterState(PlayerState state)
    {
        currentState = state;
        currentState.SetPlayerManager(playerManager);
        currentState.OnStateEnter();
    }

    void ExitState(PlayerState state)
    {
        currentState.OnStateExit();
    }

    void UpdateState()
    {
        currentState.OnStateUpdate();
    }

    public void ChangeState(PlayerState state)
    {
        ExitState(currentState);
        
        EnterState(state);
        Debug.Log("Entered state " + state.ToString() + "From "  + currentState.ToString());
    }


    void Update()
    {
        UpdateState();
    }
    
    
  
    
    
   
}
