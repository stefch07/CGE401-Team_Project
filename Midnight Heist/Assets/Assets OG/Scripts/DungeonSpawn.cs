using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSpawn : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    public Vector3 spawnLocation = new Vector3(-57.4f, 4.21f, 20.54f); // Desired spawn location

    // Start is called before the first frame update
    void Start()
    {
        // Set the player's position to the spawn location
        if (player != null)
        {
            player.transform.position = spawnLocation;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
