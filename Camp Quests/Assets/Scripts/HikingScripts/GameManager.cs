using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject winUI;
    public GameObject redUI;
    public GameObject greenUI;
    public float uiDelayTime = 2f;

    void Start()
    {
        gameOverUI.SetActive(false);
        winUI.SetActive(false);
        redUI.SetActive(false);
        greenUI.SetActive(false);

    }

    public void OnPlayerWin()
    {
        winUI.SetActive(true);
        greenUI.SetActive(true);

    }

    public void OnPlayerLose()
    {
        gameOverUI.SetActive(true);
        redUI.SetActive(true);

    }
}
