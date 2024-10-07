using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondFloorTrigger : MonoBehaviour
{
    public SecondFloorTutorial tutorial; // Reference to the tutorial manager
    private bool isTriggered = false; // To track if this trigger has been activated

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            Debug.Log($"{gameObject.name} triggered by {other.gameObject.name}.");
            tutorial.EnableExitCollider(); // Notify tutorial manager
        }
    }
}

