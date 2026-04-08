using UnityEngine;

public class InteractCheck : MonoBehaviour
{
    public float interactRadius = 2f;
    public LayerMask interactableLayer;
    private PlayerInput input;

    void Start()
    {
        input = GetComponent<PlayerInput>();
    }

    void Update()
    {
        if (input.InteractInput())
        {
            CheckForInteractables();
        }
    }

    void CheckForInteractables()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactRadius, interactableLayer);

        if (hitColliders.Length > 0)
        {
            // Find the closest interactable
            float closestDistance = Mathf.Infinity;
            InteractableObject closestInteractable = null;

            foreach (Collider col in hitColliders)
            {
                InteractableObject interactable = col.GetComponent<InteractableObject>();
                if (interactable != null)
                {
                    float distance = Vector3.Distance(transform.position, col.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestInteractable = interactable;
                    }
                }
            }

            if (closestInteractable != null)
            {
                closestInteractable.Interact();
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
