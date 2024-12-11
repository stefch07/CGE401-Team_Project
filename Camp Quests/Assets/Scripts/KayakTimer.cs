using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KayakTimer : MonoBehaviour
{
    // Total time in seconds (2 minutes)
    public static float totalTime = 120f;

    // Reference to the Text component to display the timer
    public Text timerText;

    private bool timerRunning = false;
    
    public GameObject timerPanel;
    
    public HealthSystem healthSystem;
    
    public static bool hasSeen = false;
    
    public RelaxationMeter relaxationMeter;
    
    void Start() {
        healthSystem = GameObject.FindGameObjectWithTag("HealthSystem").GetComponent<HealthSystem>();
        relaxationMeter = GameObject.FindObjectOfType<RelaxationMeter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!healthSystem.gameOver)
        {
            timerRunning = true;
        }
            
        if (timerRunning)
        {
            // Decrease the timer by the time since the last frame
            totalTime -= Time.deltaTime;

            // Stop the timer if it reaches zero
            if (totalTime <= 0f)
            {
                relaxationMeter.relaxationBar.value += 34;
                timerRunning = false;
                totalTime = 120f;
                hasSeen = true;
                if (relaxationMeter.relaxationBar.value >= 100) {
                    SceneManager.LoadScene("WinScreen");
                }
                else {
                    SceneManager.LoadScene("DemoDay");
                }
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
