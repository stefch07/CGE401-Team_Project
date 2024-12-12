using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CampfireState", menuName = "Game/CampfireState")]
public class CampfireState : ScriptableObject
{
    public int currentNightIndex = 0; // Tracks which NPC should appear next
    public bool allStoriesCompleted = false; // Optional: Tracks completion of stories
}
