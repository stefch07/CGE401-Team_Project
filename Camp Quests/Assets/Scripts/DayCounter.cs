using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DayCounter : MonoBehaviour
{
    private int currentDay = 0;  // Start at 0
    private int totalDays = 3;
    public Text dayText;
    private bool hasVisitedCampfire = false;

    void Start()
    {
        UpdateDayText();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "HubWorld" && currentDay < totalDays)  // Only increment if currentDay is less than totalDays
        {
            currentDay++;
            UpdateDayText();
            Debug.Log("Back to Hub World. Day " + currentDay);
        }
    }

    public void VisitCampfireStories()
    {
        if (!hasVisitedCampfire)
        {
            if (currentDay < totalDays)  // Check if the current day is less than totalDays
            {
                currentDay++;
                hasVisitedCampfire = true;
                UpdateDayText();
            }
            else
            {
                Debug.Log("No days left. You can't continue the adventure.");
            }
        }
        else
        {
            Debug.Log("You have already visited the campfire.");
        }
    }

    void UpdateDayText()
    {
        if (currentDay == 1)
        {
            dayText.text = "Day: " + currentDay;
        }
        else
        {
            dayText.text = "Days: " + currentDay;
        }
    }
}
