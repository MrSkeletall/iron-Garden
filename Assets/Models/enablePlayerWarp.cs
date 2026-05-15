using UnityEngine;

public class enablePlayerWarp : MonoBehaviour
{
    [Tooltip("Assign a GameObject representing the player's warp (will be SetActive(true)). If left empty, the script will try to enable a MonoBehaviour named 'PlayerWarp' on the player GameObject that entered the trigger.")]
    [SerializeField] private GameObject warpObject;
    private PlayerManager playerManager;

    [Tooltip("Tag used to detect the player. Make sure your player GameObject uses this tag.")]
    [SerializeField] private string playerTag = "Player";

    // Prevent multiple triggers
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (!other.CompareTag(playerTag)) return;

        // Try to enable the explicit warp object first
        if (warpObject != null)
        {
            warpObject.SetActive(true);
            Debug.Log($"enablePlayerWarp: Activated warp object '{warpObject.name}'.");
        }
        else
        {
            // Try to find a MonoBehaviour named "PlayerWarp" on the colliding object or its root
            bool enabledWarp = TryEnablePlayerWarpOn(other.gameObject);
            if (!enabledWarp && other.transform.root != other.transform)
            {
                enabledWarp = TryEnablePlayerWarpOn(other.transform.root.gameObject);
            }

            if (!enabledWarp)
            {
                Debug.LogWarning("enablePlayerWarp: No warpObject assigned and no 'PlayerWarp' MonoBehaviour found on the player. Assign a warpObject or add a PlayerWarp component.");
            }
        }

        triggered = true;

        // Disable this trigger GameObject so it doesn't trigger again
        gameObject.SetActive(false);
    }

    // Attempts to find a MonoBehaviour whose type name is "PlayerWarp" on the given GameObject,
    // and set its warp-obtained boolean to true. Falls back to enabling the component if no boolean found.
    private bool TryEnablePlayerWarpOn(GameObject go)
    {
        playerManager = go.GetComponent<PlayerManager>();
        if (playerManager == null)
        {
            Debug.LogWarning("enablePlayerWarp: No PlayerManager component found on the player GameObject.");
            return false;
        }

        playerManager.WarpObtained = true;
        return true;
    }
}
