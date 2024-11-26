using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmObject : MonoBehaviour
{
    public HealthSystem healthSystem;
    
    private void Start() {
        healthSystem = GameObject.FindGameObjectWithTag("HealthSystem").GetComponent<HealthSystem>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // Check if the triggering object has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // Destroy this object
            Destroy(gameObject);
            healthSystem.TakeDamage();
        }
    }
}
