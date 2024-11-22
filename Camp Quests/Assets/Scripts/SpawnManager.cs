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
    
    void Start() {
        // get a reference to the health system script
        healthSystem = GameObject.FindGameObjectWithTag("HealthSystem").GetComponent<HealthSystem>();
        
        // InvokeRepeating("SpawnRandomPrefab", 2, 1.5f);
        
        StartCoroutine(SpawnRandomPrefabWithCoroutine());
    }
    
    IEnumerator SpawnRandomPrefabWithCoroutine() {
        // add a 3 second delay before first spawning objects
        yield return new WaitForSeconds(3f);
        
        while (true) {
            SpawnRandomPrefab();
            
            float randomDelay = Random.Range(2f, 3f);
            
            yield return new WaitForSeconds(randomDelay);
        }
    }
    
    void SpawnRandomPrefab() {
        // pick a random index
        int randomInt = Random.Range(1, 10);
        int prefabIndex = 0;
        
        if (randomInt <= 3) {
            prefabIndex = 0;
        }
        else if (randomInt <= 5) {
            prefabIndex = 1;
        }
        else if (randomInt <= 7) {
            prefabIndex = 2;
        }
        else if (randomInt <= 8) {
            prefabIndex = 3;
        }
        else if (randomInt <= 9) {
            prefabIndex = 4;
        }
        else {
            prefabIndex = 5;
        }
        
        // generate a random position
        Vector3 spawnPos = new Vector3(spawnPosX, Random.Range(downBound, upBound), spawnPosZ);
        
        // spawn our object
        //Instantiate(prefabsToSpawn[prefabIndex], spawnPos, prefabsToSpawn[prefabIndex].transform.rotation);
        Instantiate(prefabsToSpawn[prefabIndex], spawnPos, Quaternion.Euler(0, 180, 0));
    }
}
