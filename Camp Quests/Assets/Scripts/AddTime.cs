using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTime : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the triggering object has the tag "Player"
        if (other.CompareTag("Player"))
        {
            KayakTimer.totalTime += 10f;
            // Destroy this object
            Destroy(gameObject);
        }
    }
}
