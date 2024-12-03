using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("DemoDay");
    }
    
    public void PlayRealGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void GoToOptionsMenu()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void CreditsButton()
    {
        SceneManager.LoadScene("Credits"); // scene 4 in case it doesn't work
    }

    public void GoToLevelSelect()
    {
        SceneManager.LoadScene("LevelSelector");
    }
}
