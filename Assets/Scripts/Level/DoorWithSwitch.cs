using UnityEngine;

public class DoorWithSwitch : InteractableObject
{
    public GameObject Door;
    private bool isDoorOpen = false;

    public override void Interact()
    {
        ToggleDoor();
    }

    void ToggleDoor()
    {
        isDoorOpen = !isDoorOpen;

        if (Door != null)
        {
            Door.SetActive(!isDoorOpen);
            Debug.Log($"Door {(isDoorOpen ? "opened" : "closed")}");
        }
    }
}
