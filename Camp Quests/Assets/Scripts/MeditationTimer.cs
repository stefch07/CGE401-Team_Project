using UnityEngine;
using TMPro;

public class MeditationTimer : MonoBehaviour
{
    public float totalTime = 90f;
    public TextMeshProUGUI timerText;
    private bool timerRunning = false;
    private int mindWanderCount = 0;

    public TextMeshProUGUI scoreText;

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
                EndMeditationSession();
            }
            UpdateTimerDisplay();
        }

        // Handle the Space key press (simulating mind wandering)
        if (Input.GetKeyDown(KeyCode.Space))
        {
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
