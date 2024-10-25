using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : MonoBehaviour
{
    public int keyCount = 0;                  // Player's key count
    public Transform closedRotationTransform; // Transform for the closed state
    public Transform openRotationTransform;   // Transform for the open state
    public float rotationSpeed = 2f;          // Speed at which the door rotates
    public float openDistance = 3f;           // Distance within which the door will open
    private bool isOpen = false;              // Whether the door is currently open
    private Transform player;

    void Start()
    {
        // Get the player's transform (assuming the player is tagged as "Player")
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is close enough and has a key
        if (!isOpen && keyCount > 0 && Vector3.Distance(player.position, transform.position) <= openDistance)
        {
            // Open the door if conditions are met
            isOpen = true;
            StartCoroutine(RotateDoor(openRotationTransform.rotation));
        }
        else if (isOpen && Vector3.Distance(player.position, transform.position) > openDistance)
        {
            // Close the door when the player walks away
            isOpen = false;
            StartCoroutine(RotateDoor(closedRotationTransform.rotation));
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
