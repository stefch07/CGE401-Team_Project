using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireManager : MonoBehaviour
{
    [SerializeField] private DialogueActivator[] npcActivators;
    private int currentNightIndex = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateNPCs();
    }

    public void OnStoryComplete()
    {
        currentNightIndex++;

        if (currentNightIndex < npcActivators.Length)
        {
            UpdateNPCs();
        }
        else
        {
            Debug.Log("All stories have been completed!");
        }
    }

    private void UpdateNPCs()
    {
        for (int i = 0; i < npcActivators.Length; i++)
        {
            npcActivators[i].gameObject.SetActive(i == currentNightIndex);
        }
    }
}
