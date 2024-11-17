using UnityEngine;
using TMPro; // Required for TextMeshPro

public class MeditationTimer : MonoBehaviour
{
    // Total time in seconds (1 minute 30 seconds)
    public float totalTime = 90f;

    // Reference to the TMP Text component to display the timer
    public TextMeshProUGUI timerText;

    private bool timerRunning = true;

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            // Decrease the timer by the time since the last frame
            totalTime -= Time.deltaTime;

            // Stop the timer if it reaches zero
            if (totalTime <= 0)
            {
                totalTime = 0;
                timerRunning = false;

                // Trigger an event when the timer ends
                EndMeditationSession();
            }

            // Update the timer text
            UpdateTimerDisplay();
        }
    }

    // Update the timer text to show minutes and seconds
    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(totalTime / 60);
        int seconds = Mathf.FloorToInt(totalTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Method called when the timer ends
    private void EndMeditationSession()
    {
        Debug.Log("Meditation session complete!");
        timerText.text = "Session Complete!";
        // Add additional actions here, like stopping the game or displaying a message
    }
}