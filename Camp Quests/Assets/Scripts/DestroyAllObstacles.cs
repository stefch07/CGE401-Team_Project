using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAllObstacles : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
        {
            // Check if the collided object has the "Player" tag
            if (other.CompareTag("Player"))
            {
                // Find all objects with the "Obstacle" tag
                GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

                // Loop through each object and destroy it
                foreach (GameObject obstacle in obstacles)
                {
                    Destroy(obstacle);
                }
                
                Destroy(gameObject);
            }
        }
}
