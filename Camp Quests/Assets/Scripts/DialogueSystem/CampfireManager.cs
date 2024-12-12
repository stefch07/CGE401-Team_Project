using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CampfireManager : MonoBehaviour
{
    [SerializeField] private DialogueActivator[] npcActivators;
    [SerializeField] private CampfireState campfireState;
    private static bool isFirstStart = true; // Static flag to track if it's the first start of the game
    
    // Start is called before the first frame update
    void Start()
    {
        // Reset the currentNightIndex to 0 when the game restarts
        if (isFirstStart)
        {
            campfireState.currentNightIndex = 0;
            isFirstStart = false;
            Debug.Log($"CampfireManager Start - Current Night Index: {campfireState.currentNightIndex}");
        }
        
        // Ensure the CampfireState is not destroyed on scene load
        if (campfireState == null)
        {
            // Try to find an existing CampfireState if not already set
            campfireState = FindObjectOfType<CampfireState>();

            if (campfireState == null)
            {
                Debug.LogWarning("No CampfireState found, creating a new one");
                campfireState = ScriptableObject.CreateInstance<CampfireState>();
            }
            else
            {
                DontDestroyOnLoad(campfireState);
            }
        }
        
        Debug.Log($"CampfireManager Start - Current Night Index: {campfireState.currentNightIndex}");
        UpdateNPCs();
    }

    void Awake()
    {
        // Ensure the CampfireState persists across scenes
        DontDestroyOnLoad(gameObject);
    }

    public void OnStoryComplete()
    {
        Debug.Log($"Before transitioning to HubWorld: Current Night Index: {campfireState.currentNightIndex}");
        
        campfireState.currentNightIndex++;

        if (campfireState.currentNightIndex < npcActivators.Length)
        {
            Debug.Log($"Transitioning to HubWorld. Current Night Index: {campfireState.currentNightIndex}");
            SceneManager.LoadScene("HubWorld");
        }
        else
        {
            campfireState.allStoriesCompleted = true;
            Debug.Log("All stories have been completed!");
            
            // Optional: Can comment this out, but transition to the HubWorld if all stories are done
            SceneManager.LoadScene("HubWorld");
        }
    }

    private void UpdateNPCs()
    {
        for (int i = 0; i < npcActivators.Length; i++)
        {
            npcActivators[i].gameObject.SetActive(i == campfireState.currentNightIndex);
            
            // Log which NPC is being activated
            if (i == campfireState.currentNightIndex)
            {
                Debug.Log($"Activating NPC: {npcActivators[i].gameObject.name}");
            }
        }
    }
}
