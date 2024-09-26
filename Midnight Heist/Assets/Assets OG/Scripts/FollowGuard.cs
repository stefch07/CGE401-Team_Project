/*
 Contributors: Treasure Keys
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGuard : MonoBehaviour
{
    public Transform guard;            // Reference to the guard's position
    public float floatHeight = 2f;     // Height above the guard's head where the capsule should float
    public GuardAI guardAI;            // Reference to the GuardAI script to check visibility

    private Vector3 offset;            // Offset to position the capsule above the guard

    void Start()
    {
        // Calculate the offset to keep the capsule floating above the guard's head
        offset = new Vector3(0, floatHeight, 0);
    }

    void Update()
    {
        // Set the capsule's position to float above the guard
        transform.position = guard.position + offset;

        // Optionally, add visual feedback for visibility based on guardAI's visibility check
        if (guardAI.IsPlayerInSight)  // Accessing the property
        {
            // Change the capsule color to green when the player is visible
            GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            // Change the capsule color to red when the player is not visible
            GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
