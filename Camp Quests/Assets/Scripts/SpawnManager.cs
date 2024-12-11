using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // set this array of references in the inspector
    public GameObject[] prefabsToSpawn;
    
    // variables for spawn position
    public float downBound = -10;
    public float upBound = 10;
    public float spawnPosX = 0;
    public float spawnPosZ = 0;
    
    public HealthSystem healthSystem;
    
    public ObjectMovement objectMovement;
    
    void Start() {
        // get a reference to the health system script
        healthSystem = GameObject.FindGameObjectWithTag("HealthSystem").GetComponent<HealthSystem>();
        
        //GameObject objectMovementGameObject = new GameObject("ObjectMovementObject");
        //objectMovement = objectMovementGameObject.AddComponent<ObjectMovement>();
        
        //objectMovement.lowerBound = 1.0f;
        //objectMovement.upperBound = 4.0f;
        
        // InvokeRepeating("SpawnRandomPrefab", 2, 1.5f);
        
        StartCoroutine(SpawnRandomPrefabWithCoroutine());
        StartCoroutine(UpdateBoundsRoutine());
    }
    
    IEnumerator SpawnRandomPrefabWithCoroutine() {
        // add a 3 second delay before first spawning objects
        yield return new WaitForSeconds(3f);
        
        while (true) {
            if (!healthSystem.gameOver) {
                SpawnRandomPrefab();
                
                float randomDelay = Random.Range(2f, 3f);
                
                yield return new WaitForSeconds(randomDelay);
            }
        }
    }
    
    IEnumerator UpdateBoundsRoutine() {
        while (!healthSystem.gameOver) {
            // Wait for 20 seconds
            yield return new WaitForSeconds(20f);
            
            // Increase bounds
            //objectMovement.lowerBound += 0.25f;
            //objectMovement.upperBound += 0.25f;
        }
    }
    
    void SpawnRandomPrefab() {
        // pick a random index
        int randomInt = 0;
        if (KayakTimer.totalTime > 60f) {
            randomInt = Random.Range(1, 12);
        }
        else {
            randomInt = Random.Range(1, 14);
        }
        int prefabIndex = 0;
        
        if (randomInt <= 2) {
            prefabIndex = 0;
        }
        else if (randomInt <= 4) {
            prefabIndex = 1;
        }
        else if (randomInt <= 6) {
            prefabIndex = 2;
        }
        else if (randomInt <= 8) {
            prefabIndex = 3;
        }
        else if (randomInt <= 9) {
            prefabIndex = 4;
        }
        else if (randomInt <= 10) {
            prefabIndex = 5;
        }
        else if (randomInt <= 12) {
            prefabIndex = 6;
        }
        else {
            prefabIndex = 7;
        }
        
        // generate a random position
        Vector3 spawnPos = new Vector3(spawnPosX, Random.Range(downBound, upBound), spawnPosZ);
        
        // spawn our object
        //Instantiate(prefabsToSpawn[prefabIndex], spawnPos, prefabsToSpawn[prefabIndex].transform.rotation);
        Instantiate(prefabsToSpawn[prefabIndex], spawnPos, Quaternion.Euler(0, 180, 0));
    }
}
