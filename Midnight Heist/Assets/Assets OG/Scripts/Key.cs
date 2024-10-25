using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public float interactionDistance = 2.0f; // Distance within which player can interact with the key
    private Transform player;

    void Start()
    {
        // Get the player's transform (assuming the player is tagged as "Player")
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Check if player is close enough to the item
        if (Vector3.Distance(player.position, transform.position) <= interactionDistance)
        {
            // If the 'E' key is pressed
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Make the key disappear
                gameObject.SetActive(false);
            }
        }
    }
}
