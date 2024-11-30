using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingObject : MonoBehaviour
{
    public HealthSystem healthSystem;
    public AudioClip healingSound;
    public AudioSource audioSource;
    
    private void Start() {
        healthSystem = GameObject.FindGameObjectWithTag("HealthSystem").GetComponent<HealthSystem>();
        audioSource = GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // Check if the triggering object has the tag "Player"
        if (other.CompareTag("Player"))
        {
            healthSystem.Heal();
            audioSource.PlayOneShot(healingSound);
            // Destroy this object
            Destroy(gameObject);
        }
    }
}
