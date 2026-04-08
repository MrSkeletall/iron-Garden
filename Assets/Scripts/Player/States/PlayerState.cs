

public interface PlayerState
{
    
    
    public void OnStateEnter();

    public void OnStateExit();
    
    public void OnStateUpdate();

    public void SetPlayerManager(PlayerManager playerManager);
}
