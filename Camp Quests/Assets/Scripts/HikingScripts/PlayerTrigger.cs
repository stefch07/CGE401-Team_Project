using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Win"))
        {
            gameManager.OnPlayerWin();
        }
    }
}
