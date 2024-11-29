using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    public CoinScore coinScore;
    public AudioClip coinSound;
    public AudioSource audioSource;
    
    private void Start() {
        coinScore = GameObject.FindGameObjectWithTag("CoinScore").GetComponent<CoinScore>();
        audioSource = GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // Check if the triggering object has the tag "Player"
        if (other.CompareTag("Player"))
        {
            coinScore.Inc();
            audioSource.PlayOneShot(coinSound);
            // Destroy this object
            Destroy(gameObject, coinSound.length - 0.7f);
        }
    }
}
