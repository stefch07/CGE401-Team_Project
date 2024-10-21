using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogPanel : MonoBehaviour
{
    public Text dialogText; // Reference to the TextMeshPro text component

    private void Start()
    {
        // Initially, hide the dialog text
        dialogText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Show the dialog text when the player enters the trigger zone
            dialogText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Hide the dialog text when the player exits the trigger zone
            dialogText.gameObject.SetActive(false);
        }
    }
}
