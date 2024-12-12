using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSwitcherWithButton : MonoBehaviour
{
    public Button yesButtonM;
    public Button yesButtonH;
    public Button yesButtonK;
    public Button yesButtonCS;

    public string sceneM = "Meditation";
    public string sceneH = "Hiking";
    public string sceneK = "KayakMinigame";
    public string sceneCS = "CampfireStories";

    void Start()
    {
        if (yesButtonM != null)
            yesButtonM.onClick.AddListener(() => LoadScene(sceneM));

        if (yesButtonH != null)
            yesButtonH.onClick.AddListener(() => LoadScene(sceneH));

        if (yesButtonK != null)
            yesButtonK.onClick.AddListener(() => LoadScene(sceneK));

        if (yesButtonCS != null)
            yesButtonCS.onClick.AddListener(() => LoadScene(sceneCS));
    }

    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
