using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject winUI;
    public GameObject redUI;
    public GameObject greenUI;

    public PlayerHiking player;

    void Start()
    {
        gameOverUI.SetActive(false);
        winUI.SetActive(false);
        redUI.SetActive(false);
        greenUI.SetActive(false);

        if (player == null)
        {
            player = FindObjectOfType<PlayerHiking>();
        }
    }

    public void OnPlayerWin()
    {
        winUI.SetActive(true);
        greenUI.SetActive(true);
        player.StopPlayerMovement();
    }

    public void OnPlayerLose()
    {
        gameOverUI.SetActive(true);
        redUI.SetActive(true);
        player.StopPlayerMovement();
    }
}
