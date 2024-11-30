using UnityEngine;
using UnityEngine.UI;

public class KayakTimer : MonoBehaviour
{
    // Total time in seconds (3 minutes)
    public static float totalTime = 180f;

    // Reference to the Text component to display the timer
    public Text timerText;

    private bool timerRunning = false;
    
    public GameObject timerPanel;
    public HealthSystem healthSystem;
    
    void Start() {
        healthSystem = GameObject.FindGameObjectWithTag("HealthSystem").GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        timerRunning = true;
        
        if (timerRunning && !healthSystem.gameOver)
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
