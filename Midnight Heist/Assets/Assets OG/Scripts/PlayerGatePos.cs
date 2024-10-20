using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGatePos : MonoBehaviour
{
    public Transform player;      
    public Vector3 targetPosition; 
    public Vector3 targetRotation; 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TransportPlayer();
        }
    }

    void TransportPlayer()
    {
        player.position = targetPosition;                          
        player.rotation = Quaternion.Euler(targetRotation);          
    }
}
