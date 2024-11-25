using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSpawnManager : MonoBehaviour
{
    [Header("Log Spawning Settings")]
    public GameObject logPrefab;      
    public float spawnInterval = 2f;  
    public float spawnRangeX = 10f;    
    public float spawnYPosition = 0f; 

    private void Start()
    {
        InvokeRepeating(nameof(SpawnLog), 0f, spawnInterval);
    }

    void SpawnLog()
    {
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);

        Vector2 spawnPosition = new Vector2(randomX, spawnYPosition);

        Instantiate(logPrefab, spawnPosition, Quaternion.identity);
    }
}
