using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the triggering object has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // Destroy this object
            Destroy(gameObject);
        }
    }
}
