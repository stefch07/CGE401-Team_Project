using System.Collections;
using UnityEngine;

public class TipZone : MonoBehaviour
{
    public GameObject tipPanel; // Reference to the panel GameObject
    private bool isPlayerInZone = false;

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the "Player" tag
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the object leaving the trigger has the "Player" tag
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;
        }
    }

    void Update()
    {
        // Check if the player is in the zone and presses "G"
        if (isPlayerInZone && Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(ShowTipPanel());
        }
    }

    IEnumerator ShowTipPanel()
    {
        tipPanel.SetActive(true); // Activate the panel
        yield return new WaitForSeconds(2); // Wait for 2 seconds
        tipPanel.SetActive(false); // Deactivate the panel
    }
}
