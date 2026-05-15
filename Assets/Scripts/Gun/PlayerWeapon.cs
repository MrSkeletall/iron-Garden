using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    
    public GameObject projectilePrefab;
    public GameObject chargedProjectilePrefab;

    public Transform projectileSpawn;
    public Camera mainCamera;
    
    [SerializeField] private float shotSpeed = 10f;
    [SerializeField] private float chargeThreshold = 1f;
    private float chargeTime;
    private bool isCharging;
    private float fireRate;
    private InputAction fire;
    pAnimation animator;
    
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        fire = InputSystem.actions.FindAction("Attack");
        animator = gameObject.GetComponentInChildren<pAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fire.WasPressedThisFrame())
        {
            isCharging = true;
            chargeTime = 0f;
        }

        if (isCharging)
        {
            chargeTime += Time.deltaTime;
        }

        if (fire.WasReleasedThisFrame())
        {
            if (chargeTime >= chargeThreshold)
            {
                FireChargedProjectile();
            }
            else
            {
                FireProjectile();
            }
            isCharging = false;
        }
    }
    
    public void FireProjectile()
    {
        //animator.SetTrigger("Attack");
        GameObject shotProjectile = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
        Rigidbody rb = shotProjectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = CalculateDirection();
            rb.linearVelocity = direction * shotSpeed;
        }
        Destroy(shotProjectile, 10f);
    }
    
    private Vector3 CalculateDirection()
    {
        //calculate direction from the spawn point of the projectile to the center point of the camera in world space
        //third person shots should still be fired from the player's position in this game'
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Ray ray = mainCamera.ScreenPointToRay(screenCenter);

        // Check if ray hits something
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Aim at the hit point
            Vector3 direction = (hit.point - projectileSpawn.position).normalized;
            return direction;
        }
        else
        {
            // If no hit, use a far point along the ray
            Vector3 targetPoint = ray.GetPoint(100f);
            Vector3 direction = (targetPoint - projectileSpawn.position).normalized;
            return direction;
        }
    }
    
    public void FireChargedProjectile()
    {
        //animator.SetTrigger("Attack");
        GameObject shotProjectile = Instantiate(chargedProjectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
        Rigidbody rb = shotProjectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = CalculateDirection();
            rb.linearVelocity = direction * shotSpeed;
        }
        Destroy(shotProjectile, 10f);
    }
}
