using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondFloorTutorial : MonoBehaviour
{
    public Collider secondFloorExitCollider; // Collider to block exit from the second floor
    private int roomTriggersCount = 0; // Counter for activated room triggers

    void Start()
    {
        // Ensure the exit collider starts disabled
        if (secondFloorExitCollider != null)
        {
            secondFloorExitCollider.enabled = false;
            Debug.Log("SecondFloorExitCollider is initially disabled.");
        }
    }

    public void EnableExitCollider()
    {
        // Enable exit collider to block the player
        if (secondFloorExitCollider != null)
        {
            secondFloorExitCollider.enabled = true;
            Debug.Log("Exit collider enabled, blocking exit.");
        }
    }

    public void RoomTriggered(string roomName)
    {
        roomTriggersCount++;
        Debug.Log($"{roomName} activated. Total rooms triggered: {roomTriggersCount}");

        // If all rooms are triggered, unlock exit
        if (roomTriggersCount >= 4) // Adjust based on how many room triggers you have
        {
            UnlockExit();
        }
    }

    private void UnlockExit()
    {
        // Disable the exit collider to allow the player to leave
        if (secondFloorExitCollider != null)
        {
            secondFloorExitCollider.enabled = false; // Disable exit collider
            Debug.Log("Exit collider disabled, player can leave.");
        }
    }
}
