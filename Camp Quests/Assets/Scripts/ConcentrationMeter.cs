using UnityEngine;
using TMPro;

public class ConcentrationMeter : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    public AudioSource soundEffectSource;
    private float totalTime = 90f;
    private int spaceScore = 0;
    private bool timerRunning = false; // Initially false
    
    private float spaceBarCooldown = 1f; // Cooldown time in seconds
    private float lastSpaceBarPressTime = 0f; // Time of the last Space bar press

    void Start() {
        UpdateScoreText();
        UpdateTimerDisplay();
    }

    void Update()
    {
        // Handle the timer countdown
        if (timerRunning)
        {
            totalTime -= Time.unscaledDeltaTime; // Use unscaledDeltaTime to ignore timeScale

            if (totalTime <= 0)
            {
                totalTime = 0;
                timerRunning = false;
                EndMeditationSession();
            }

            UpdateTimerDisplay();
        }

        // Handle the Space key press
        if (Input.GetKeyDown(KeyCode.Space) && MeditationTimer.timerRunning && Time.time >= lastSpaceBarPressTime + spaceBarCooldown)
        {
            spaceScore++;
            
            lastSpaceBarPressTime = Time.time; // Update the time of the last press
            soundEffectSource.Play();
            UpdateScoreText();
        }
    }

    public void StartTimer()
    {
        Time.timeScale = 1; // Ensure timeScale is 1 to avoid freezes
        timerRunning = true;
    }

    public void ResetSession()
    {
        totalTime = 90f;
        spaceScore = 0;
        timerRunning = false;

        UpdateScoreText();
        UpdateTimerDisplay();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "You lost your concentration " + spaceScore + " times";
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(totalTime / 60);
        int seconds = Mathf.FloorToInt(totalTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void EndMeditationSession()
    {
        Debug.Log("Meditation session complete!");
        timerText.text = "Session Complete!";
        
        // Display the final message with the concentration score
        scoreText.text = "Your mind wandered " + spaceScore + " times";
    }
}
