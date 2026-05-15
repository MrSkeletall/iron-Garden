using UnityEngine;

public class AirState : PlayerState
{
    
    
    
    
    PlayerManager playerManager;
    CharacterController pController;
    PlayerStateManager stateManager;
    private Vector3 playerVelocity;
    private pAnimation animator;
    private bool velocitySet = false;
    
    /*public AirState(PlayerManager playerManager)
    {
        this.playerManager = playerManager;
    }*/
    public void OnStateEnter()
    {
        // If velocity wasn't set from GroundState, use current controller velocity
        if (!velocitySet)
        {
            playerVelocity = pController.velocity;
            Debug.Log("AirState: Velocity not set, using controller velocity");
        }
        else
        {
            Debug.Log($"AirState: Velocity set from GroundState: {playerVelocity}");
        }

        animator = playerManager.GetAnimator();
        if (animator != null)
        {
            animator.SetBool("Idle", false);
            animator.SetTrigger("Jump");
        }
    }

    public void SetPlayerVelocity(Vector3 velocity)
    {
        playerVelocity = velocity;
        velocitySet = true;
    }

    public void OnStateExit()
    {
        //animator.SetBool("Idle", true);
    }

    public void OnStateUpdate()
    {
        if (pController.isGrounded && playerVelocity.y <= 0)
        {
            Debug.Log("AirState: Grounded, transitioning to GroundState");
            playerManager.SendStateChange(new GroundState());
            return;
        }
        
        // Check for warp input
        if (playerManager.WarpPressed())
        {
            playerManager.SendStateChange(new WarpState());
            return;
        }
        
        Vector2 direction = playerManager.getPlayerMovement();
        
        AirMovement(direction);
        
    }

    void AirMovement(Vector2 direction)
    {
         
        bool groundedPlayer = pController.isGrounded;
        bool jumpPressed = playerManager.JumpPressed();
        
        if (groundedPlayer)
        {
            // Slight downward velocity to keep grounded stable
            if (playerVelocity.y < -2f)
                playerVelocity.y = -2f;
        }

        // Read input
        Vector2 input = playerManager.getPlayerMovement();
        Vector3 move = playerManager.GetCameraRelativeMoveDirection(input);
        

        

        // Jump using WasPressedThisFrame()
        if (groundedPlayer && jumpPressed)
        {
            //playerVelocity.y = Mathf.Sqrt(playerManager.jumpPower * -2f * playerManager.p_gravity);
        }

        // Apply gravity with faster fall speed for snappier feel
        float gravityMultiplier = playerVelocity.y < 0 ? Constants.GRAVITY_MULTIPLIER_FALLING : 1f;
        playerVelocity.y += playerManager.p_gravity * gravityMultiplier * Time.deltaTime;

        // Blend air movement with jump momentum
        Vector3 airMove = move * playerManager.airSpeed;
        playerVelocity.x = Mathf.Lerp(playerVelocity.x, airMove.x, Time.deltaTime * 5f);
        playerVelocity.z = Mathf.Lerp(playerVelocity.z, airMove.z, Time.deltaTime * 5f);

        // Move
        pController.Move(playerVelocity * Time.deltaTime);
    }

    public void SetPlayerManager(PlayerManager playerManager)
    {
        this.playerManager = playerManager; 
        pController = playerManager.GetCharacterController();
        PlayerStateManager stateManager = this.playerManager.GetStateManager();
    }
}
