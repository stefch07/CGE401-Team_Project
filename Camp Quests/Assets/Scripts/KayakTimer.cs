using UnityEngine;
using UnityEngine.UI;
using TMPro; // Required for TextMeshPro

public class KayakTimer : MonoBehaviour
{
    // Total time in seconds (3 minutes)
    public float totalTime = 180f;

    // Reference to the TMP Text component to display the timer
    public TextMeshProUGUI timerText;

    private bool timerRunning = false;
    
    public GameObject timerPanel;

    // Update is called once per frame
    void Update()
    {
        if (!timerPanel.activeSelf) {
            timerRunning = true;
        }
        
        if (timerRunning)
        {
            // Decrease the timer by the time since the last frame
            totalTime -= Time.deltaTime;

            // Stop the timer if it reaches zero
            if (totalTime <= 0)
            {
                totalTime = 0;
                timerRunning = false;
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
}
