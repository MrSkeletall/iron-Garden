using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInput : MonoBehaviour
{
    //input actions 
    private InputAction move;
    private InputAction jump;
    private InputAction Look;
    private InputAction interact;
    private InputAction crouch;
    private InputAction sprint;
    private InputAction fire;
    private InputAction rightClick;
    
    
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        move = InputSystem.actions.FindAction("Move");
        jump = InputSystem.actions.FindAction("Jump");
        Look = InputSystem.actions.FindAction("Look");
        interact = InputSystem.actions.FindAction("Interact");
        crouch = InputSystem.actions.FindAction("Crouch");
        sprint = InputSystem.actions.FindAction("Sprint");
        fire = InputSystem.actions.FindAction("Attack");
        rightClick = InputSystem.actions.FindAction("RightClick");
        
    }

    public bool JumpInput()
    {
        
        return jump.WasPressedThisFrame();
        

    }
   

    public bool SprintInput()
    {
        return sprint.IsPressed();
    }

    public Vector2 GetMoveValue()
    {
        return move.ReadValue<Vector2>();
    }

    public Vector2 GetLookValue()
    {
        return Look.ReadValue<Vector2>();
    }

    public bool WarpInput()
    {
        return rightClick.IsPressed();
    }

    public bool InteractInput()
    {
        return interact.WasPressedThisFrame();
    }

}
