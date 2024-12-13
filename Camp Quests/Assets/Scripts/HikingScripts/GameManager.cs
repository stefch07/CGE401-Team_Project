using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject winUI;
    public GameObject redUI;
    public GameObject greenUI;
    public GameObject hubButton;

    public PlayerHiking playerHiking;

    void Start()
    {
        gameOverUI.SetActive(false);
        winUI.SetActive(false);
        redUI.SetActive(false);
        greenUI.SetActive(false);
        hubButton.SetActive(false);

        if (playerHiking == null)
        {
            playerHiking = FindObjectOfType<PlayerHiking>();
        }
    }

    public void OnPlayerWin()
    {
        winUI.SetActive(true);
        greenUI.SetActive(true);
        hubButton.SetActive(true);
        playerHiking.StopPlayerMovement();
    }

    public void OnPlayerLose()
    {
        gameOverUI.SetActive(true);
        redUI.SetActive(true);
        playerHiking.StopPlayerMovement();
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        redUI.SetActive(true);
        hubButton.SetActive(true);

        if (playerHiking != null)
        {
            playerHiking.StopPlayerMovement();
        }

        Debug.Log("Game Over! Player has died.");
    }
}
