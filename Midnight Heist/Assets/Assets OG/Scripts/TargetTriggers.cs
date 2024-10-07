using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTriggers : MonoBehaviour
{
    private bool isHit = false; // Track if the target has been hit
    public ScoreManager scoreManager; // Reference to the ScoreManager

    private void OnTriggerEnter(Collider other)
    {
        // Check if the target has not been hit and the collider is a projectile
        if (!isHit && other.CompareTag("Rock"))
        {
            scoreManager.AddScore(1); // Add to the score
            isHit = true; // Mark target as hit
  
        }
    }
}
