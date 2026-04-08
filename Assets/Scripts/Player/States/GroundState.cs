using UnityEngine;
using UnityEngine.InputSystem.Switch;

public class GroundState : PlayerState
{
    
    
    
    
    private PlayerManager PlayerManager;
    private CharacterController pController;
    private Vector3 playerVelocity;
    
    
    public void OnStateEnter()
    {
        playerVelocity = pController.velocity;
    }

    public void OnStateExit()
    {
        
    }

    public void OnStateUpdate()
    {
        // Check for warp input
        if (PlayerManager.WarpPressed())
        {
            PlayerManager.SendStateChange(new WarpState());
            return;
        }

        bool groundedPlayer = pController.isGrounded;
        bool jumpPressed = PlayerManager.JumpPressed();

        if (groundedPlayer)
        {
            // Slight downward velocity to keep grounded stable
            if (playerVelocity.y < -2f)
                playerVelocity.y = -2f;
        }

        // Read input
        Vector2 input = PlayerManager.getPlayerMovement();
        Vector3 move = PlayerManager.GetCameraRelativeMoveDirection(input);

        float currentSpeed = PlayerManager.SprintPressed() ? PlayerManager.sprintSpeed : PlayerManager.moveSpeed;

        // Jump - transition to AirState
        if (groundedPlayer && jumpPressed)
        {
            Debug.Log("Jump pressed! Transitioning to AirState");
            playerVelocity.y = Mathf.Sqrt(PlayerManager.jumpPower * -2f * PlayerManager.p_gravity);

            // Apply horizontal boost in movement direction
            Vector3 horizontalBoost = move.normalized * currentSpeed * Constants.JUMP_HORIZONTAL_BOOST;
            playerVelocity.x = horizontalBoost.x;
            playerVelocity.z = horizontalBoost.z;

            Debug.Log($"Jump velocity: {playerVelocity}");

            AirState airState = new AirState();
            airState.SetPlayerVelocity(playerVelocity);
            PlayerManager.SendStateChange(airState);
            return;
        }

        // Check if falling
        if (PlayerManager.IsFalling())
        {
            AirState airState = new AirState();
            airState.SetPlayerVelocity(playerVelocity);
            PlayerManager.SendStateChange(airState);
            return;
        }

        // Apply gravity
        playerVelocity.y += PlayerManager.p_gravity * Time.deltaTime;

        // Move
        Vector3 finalMove = move * currentSpeed + Vector3.up * playerVelocity.y;
        pController.Move(finalMove * Time.deltaTime);

    }

    public void SetPlayerManager(PlayerManager playerManager)
    {
        PlayerManager =  playerManager;
        pController = PlayerManager.GetCharacterController();
    }
}
