using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public GameObject ladderObject;          // Reference to the ladder GameObject
    public float interactionDistance = 2.0f; // Distance within which the player can interact with the NPC
    private Transform player;
    private GameObject ladderNPC;            // Reference to the LadderNPC

    void Start()
    {
        // Set the ladder to be initially invisible
        ladderObject.SetActive(false);

        // Find the player and LadderNPC by their tags
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ladderNPC = GameObject.FindGameObjectWithTag("LadderNPC");
    }

    void Update()
    {
        // Check if player is close enough to the NPC and presses 'E'
        if (ladderNPC != null && Vector3.Distance(player.position, ladderNPC.transform.position) <= interactionDistance && Input.GetKeyDown(KeyCode.E))
        {
            // Make the ladder appear
            ladderObject.SetActive(true);
        }
    }
}
