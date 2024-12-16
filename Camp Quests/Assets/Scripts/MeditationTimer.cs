using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MeditationTimer : MonoBehaviour
{
    public float totalTime = 90f;
    public TextMeshProUGUI timerText;
    public static bool timerRunning = false;
    private int mindWanderCount = 0;

    public TextMeshProUGUI scoreText;
    
    private float spaceBarCooldown = 1f; // Cooldown time in seconds
    private float lastSpaceBarPressTime = 0f; // Time of the last Space bar press

    void Start()
    {
        UpdateTimerDisplay();
        UpdateScoreText();
    }

    void Update()
    {
        if (timerRunning)
        {
            // Use Time.unscaledDeltaTime to keep the timer running even when the game is paused
            totalTime -= Time.unscaledDeltaTime;

            if (totalTime <= 0)
            {
                totalTime = 0;
                timerRunning = false;
                SceneManager.LoadScene("HubWorld");
                EndMeditationSession();
            }
            UpdateTimerDisplay();
        }

        // Handle the Space key press with cooldown
        if (Input.GetKeyDown(KeyCode.Space) && timerRunning && Time.time >= lastSpaceBarPressTime + spaceBarCooldown)
        {
            lastSpaceBarPressTime = Time.time; // Update the time of the last press
            mindWanderCount++;
            UpdateScoreText();
        }
    }

    public void StartTimer()
    {
        timerRunning = true;
        Time.timeScale = 1; // Ensure time flows normally if it was previously paused
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(totalTime / 60);
        int seconds = Mathf.FloorToInt(totalTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Your mind wandered " + mindWanderCount + " times";
    }

    private void EndMeditationSession()
    {
        Debug.Log("Meditation session complete!");
        timerText.text = "Session Complete!";
        UpdateScoreText();
    }
}
