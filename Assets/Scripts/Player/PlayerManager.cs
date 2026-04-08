using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private PlayerStateManager  playerStateManager;
    private PlayerInput input;
    private CharacterController cc;
    private Camera mainCamera;
    public float p_gravity = Constants.GRAVITY;
    public float moveSpeed = Constants.MOVE_SPEED;
    public float sprintSpeed = Constants.SPRINT_SPEED;
    public float airSpeed = Constants.AIR_SPEED;
    public float jumpPower = Constants.JUMP_POWER;
    public float rotationSpeed = Constants.ROTATION_SPEED;
    private bool isGrounded = false;
    public GameObject PlayerHead;
    private float verticalRotation = 0f;
    public GameObject warpIndicatorPrefab;
    private GameObject warpIndicatorInstance;
    public float maxWarpDistance = Constants.MAX_WARP_DISTANCE;
    public LayerMask impassableLayer;
    public bool WarpObtained = false;


    //visual
    private ParticleSystem warpParticle;
    private pAnimation animator;
    

    private void Awake()
    {
        playerStateManager = gameObject.GetComponent<PlayerStateManager>();
        input = gameObject.GetComponent<PlayerInput>();
        cc = gameObject.GetComponent<CharacterController>();
        warpParticle = gameObject.GetComponentInChildren<ParticleSystem>();
        mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
         moveSpeed = Constants.MOVE_SPEED;
     sprintSpeed = Constants.SPRINT_SPEED;
     airSpeed = Constants.AIR_SPEED;
     jumpPower = Constants.JUMP_POWER;
     rotationSpeed = Constants.ROTATION_SPEED;
    }
    
    
    
    
    
    

    private void Update()
    {
        Rotate();
    }


    public CharacterController GetCharacterController()
    {
        return cc;                  
    }

    public PlayerStateManager GetStateManager()
    {
        return playerStateManager;           

    }

    public bool JumpPressed()
    {
        return input.JumpInput();
    }

    public bool SprintPressed()
    {
        return input.SprintInput();
    }
  

    public Vector2 getPlayerMovement()
    {
        return input.GetMoveValue();
    }

    public Vector3 GetCameraRelativeMoveDirection(Vector2 inputDirection)
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        if (mainCamera == null)
            return new Vector3(inputDirection.x, 0, inputDirection.y);

        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();

        return cameraForward * inputDirection.y + cameraRight * inputDirection.x;
    }



    public pAnimation GetAnimator()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<pAnimation>();
        }
        return animator;
    }
    
    
    

    public bool IsFalling()
    {
        return !cc.isGrounded && cc.velocity.y < 0;
    }

    public void SendStateChange(PlayerState newState)
    {
         playerStateManager.ChangeState(newState);
    }

    public void Rotate()
    {
        Vector2 lookValue = input.GetLookValue();
        float rotateAmount = lookValue.x * rotationSpeed * Time.deltaTime;
        float verticalDelta = lookValue.y * rotationSpeed * Time.deltaTime;

        verticalRotation -= verticalDelta;
        verticalRotation = Mathf.Clamp(verticalRotation, Constants.VERTICAL_ROTATION_MIN, Constants.VERTICAL_ROTATION_MAX);

        PlayerHead.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        transform.Rotate(0, rotateAmount, 0);
    }

    public Vector3 CalculateWarpPoint()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, maxWarpDistance))
        {
            // Check if hit object is impassable
            if (((1 << hit.collider.gameObject.layer) & impassableLayer) != 0)
            {
                // Place player in front of the impassable object
                Vector3 warpPoint = hit.point - ray.direction * (cc.radius + Constants.WARP_OFFSET);
                return warpPoint;
            }
            else
            {
                // Warp to the hit point
                return hit.point;
            }
        }
        else
        {
            // No hit, warp to max distance
            return ray.GetPoint(maxWarpDistance);
        }
    }

    public void WarpToPoint(Vector3 targetPoint)
    {
        cc.enabled = false;
        transform.position = targetPoint;
        cc.enabled = true;
    }

    public void ShowWarpIndicator(Vector3 position)
    {
        if (warpIndicatorInstance == null && warpIndicatorPrefab != null)
        {
            warpIndicatorInstance = Instantiate(warpIndicatorPrefab);
        }

        if (warpIndicatorInstance != null)
        {
            warpIndicatorInstance.transform.position = position;
        }
    }

    public void HideWarpIndicator()
    {
        if (warpIndicatorInstance != null)
        {
            Destroy(warpIndicatorInstance);
            warpIndicatorInstance = null;
        }
    }

    public bool WarpPressed()
    {
        return WarpObtained && input.WarpInput();
    }
    
    public void PlayWarpParticle()
    {
        if (warpParticle != null)
        {
            warpParticle.Play();
            
        }
        else
        {
            Debug.Log("No particle system found");
        }
    }
    
    
    

}
