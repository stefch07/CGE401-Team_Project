using UnityEngine;

public class LadderNPCInteraction : MonoBehaviour
{
    public float interactionDistance = 2.0f; // Distance within which the player can interact
    private Transform player;

    void Start()
    {
        // Find the player by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Check if player is close enough and presses 'E'
        if (Vector3.Distance(player.position, transform.position) <= interactionDistance && Input.GetKeyDown(KeyCode.E))
        {
            // Save interaction status
            PlayerPrefs.SetInt("LadderUnlocked", 1);
            PlayerPrefs.Save();
            Debug.Log("Ladder unlocked!");
        }
    }
}
