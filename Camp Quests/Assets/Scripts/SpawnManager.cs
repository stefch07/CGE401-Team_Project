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
        
        while (spawnPosX == 10) {
            SpawnRandomPrefab();
            
            float randomDelay = Random.Range(2f, 3f);
            
            yield return new WaitForSeconds(randomDelay);
        }
    }
    
    void SpawnRandomPrefab() {
        // pick a random animal index
        int prefabIndex = Random.Range(0, prefabsToSpawn.Length);
        
        // generate a random spawn position
        Vector3 spawnPos = new Vector3(spawnPosX, Random.Range(downBound, upBound), spawnPosZ);
        
        // spawn our animal
        //Instantiate(prefabsToSpawn[prefabIndex], spawnPos, prefabsToSpawn[prefabIndex].transform.rotation);
        Instantiate(prefabsToSpawn[prefabIndex], spawnPos, Quaternion.Euler(0, 180, 0));
    }
}
