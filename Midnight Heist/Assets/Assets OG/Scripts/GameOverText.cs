using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour
{
    public void MainGameButton() {
        SceneManager.LoadScene("MainGame");
    }
    
    public void TutorialButton() {
        SceneManager.LoadScene("TutorialScene");
    }
    
    public void MainMenuButton() {
        SceneManager.LoadScene("MainMenu");
    }
}
