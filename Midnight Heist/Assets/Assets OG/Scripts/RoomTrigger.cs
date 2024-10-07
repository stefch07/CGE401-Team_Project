using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public SecondFloorTutorial tutorial; // Reference to the tutorial manager

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"{gameObject.name} triggered by {other.gameObject.name}.");
            tutorial.RoomTriggered(gameObject.name); // Notify tutorial manager
        }
    }
}
