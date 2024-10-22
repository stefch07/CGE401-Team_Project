using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : MonoBehaviour
{
    public int keyCount = 0;                  // Player's key count
    public Transform closedRotationTransform; // Transform for the closed state
    public Transform openRotationTransform;   // Transform for the open state
    public float rotationSpeed = 2f;          // Speed at which the door rotates
    private bool isOpen = false;              // Whether the door is currently open

    // Update is called once per frame
    void Update()
    {
        // Check if player presses 'E' and has a key
        if (Input.GetKeyDown(KeyCode.E) && keyCount > 0)
        {
            // Toggle the door state
            isOpen = !isOpen;

            // Rotate the door based on its state
            if (isOpen)
            {
                StartCoroutine(RotateDoor(openRotationTransform.rotation));
            }
            else
            {
                StartCoroutine(RotateDoor(closedRotationTransform.rotation));
            }
        }
    }

    // Coroutine to smoothly rotate the door
    IEnumerator RotateDoor(Quaternion targetRotation)
    {
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            yield return null;
        }
        transform.rotation = targetRotation; // Snap to final rotation
    }
}
