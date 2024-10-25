using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToFinalLevel : MonoBehaviour
{
    private Vector3 targetPosition = new Vector3(-157.32f, 6.02f, 25.44f);
    private Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
    public Transform playerPosition;      // Reference to the player’s position

    // Trigger event when the player enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Move the player to the target position and rotate
            if (playerPosition != null)
            {
                playerPosition.position = targetPosition;   // Move the player
                playerPosition.rotation = targetRotation;    // Rotate the player
            }
        }
    }
}
