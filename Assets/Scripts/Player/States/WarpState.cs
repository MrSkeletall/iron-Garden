using UnityEngine;

public class WarpState : PlayerState
{

    private PlayerManager PlayerManager;
    private CharacterController pController;
    private Vector3 playerVelocity;
    private Vector3 targetWarpPoint;
    private bool isWarpReady = false;

    public void OnStateEnter()
    {
        // Show indicator and calculate warp point
        targetWarpPoint = PlayerManager.CalculateWarpPoint();
        PlayerManager.ShowWarpIndicator(targetWarpPoint);
        isWarpReady = true;

        // Slow down time
        Time.timeScale = 0.3f;

        // Stop vertical movement
        //playerVelocity = Vector3.zero;
    }

    public void OnStateExit()
    {
        // Hide indicator when exiting state
        PlayerManager.HideWarpIndicator();
        isWarpReady = false;
        

        // Restore normal time
        Time.timeScale = 1.0f;
    }

    public void OnStateUpdate()
    {
        // Prevent falling - freeze vertical velocity
        playerVelocity.y = 0;
        pController.Move(playerVelocity * Time.deltaTime);

        // Update warp point position while aiming
        targetWarpPoint = PlayerManager.CalculateWarpPoint();
        PlayerManager.ShowWarpIndicator(targetWarpPoint);

        // Check if warp button is released to execute warp
        if (!PlayerManager.WarpPressed() && isWarpReady)
        {
            PlayerManager.WarpToPoint(targetWarpPoint);
            PlayerManager.SendStateChange(new GroundState());
            PlayerManager.PlayWarpParticle();
        }
    }


    public void SetPlayerManager(PlayerManager playerManager)
    {
        PlayerManager =  playerManager;
        pController = PlayerManager.GetCharacterController();
    }

}
