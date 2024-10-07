using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondFloorTutorial : MonoBehaviour
{
    public Collider secondFloorTrigger; // Trigger to enter the second floor
    public Collider secondFloorExitCollider; // Collider to block exit from the second floor
    public Collider secondFloorInvisCollider; // Invisible collider to prevent jumping down

    // Public references for room triggers
    [SerializeField] private Collider secondFloorRoomOneTrigger;
    [SerializeField] private Collider secondFloorRoomTwoTrigger;
    [SerializeField] private Collider secondFloorRoomThreeTrigger;
    [SerializeField] private Collider secondFloorRoomFourTrigger;

    private bool roomOneTriggered = false;
    private bool roomTwoTriggered = false;
    private bool roomThreeTriggered = false;
    private bool roomFourTriggered = false;

    private bool AllRoomsTriggered => roomOneTriggered && roomTwoTriggered && roomThreeTriggered && roomFourTriggered;

    void Start()
    {
        // Debug log for initialization
        Debug.Log("SecondFloorTutorial initialized.");

        // Ensure secondFloorTrigger is set as a trigger
        if (secondFloorTrigger != null)
        {
            secondFloorTrigger.isTrigger = true;
            Debug.Log("SecondFloorTrigger is set as a trigger.");
        }

        // Ensure the exit collider starts disabled
        if (secondFloorExitCollider != null)
        {
            secondFloorExitCollider.enabled = false;
            Debug.Log("SecondFloorExitCollider is initially disabled.");
        }

        // Ensure the invisible collider starts enabled
        if (secondFloorInvisCollider != null)
        {
            secondFloorInvisCollider.enabled = true;
        }
    }

    void Update()
    {
        // If all room triggers are activated, allow the player to exit
        if (AllRoomsTriggered)
        {
            UnlockExit();
        }
    }

    private void UnlockExit()
    {
        // Disable the exit collider to allow the player to leave the second floor
        if (secondFloorExitCollider != null)
        {
            secondFloorExitCollider.enabled = false;
            Debug.Log("Exit collider disabled, player can leave.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug log to check if the trigger is activated
        Debug.Log($"Trigger entered by: {other.gameObject.name}");

        // Check if the player collides with the second-floor trigger
        if (other.CompareTag("Player") && secondFloorTrigger != null)
        {
            Debug.Log("Player triggered SecondFloorTrigger.");
            EnableExitCollider(); // Enable the exit collider
        }

        // Check if the player collides with any room triggers
        if (other.CompareTag("Player"))
        {
            switch (other.gameObject.name)
            {
                case "SecondFloorRoomOneTrigger":
                    roomOneTriggered = true;
                    Debug.Log("Room One Triggered.");
                    break;
                case "SecondFloorRoomTwoTrigger":
                    roomTwoTriggered = true;
                    Debug.Log("Room Two Triggered.");
                    break;
                case "SecondFloorRoomThreeTrigger":
                    roomThreeTriggered = true;
                    Debug.Log("Room Three Triggered.");
                    break;
                case "SecondFloorRoomFourTrigger":
                    roomFourTriggered = true;
                    Debug.Log("Room Four Triggered.");
                    break;
            }
        }
    }

    private void EnableExitCollider()
    {
        // Enable exit collider only if not all room triggers are activated
        if (secondFloorExitCollider != null && !AllRoomsTriggered)
        {
            secondFloorExitCollider.enabled = true; // Enable the exit collider to block the player
            Debug.Log("Exit collider enabled, blocking exit.");
        }
    }
}
