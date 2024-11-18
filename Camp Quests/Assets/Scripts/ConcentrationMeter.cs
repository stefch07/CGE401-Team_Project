using UnityEngine;
using TMPro;

public class ConcentrationMeter : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    private AudioSource soundEffectSource;
    private float totalTime = 90f;
    private int spaceScore = 0;
    private bool timerRunning = true;

    void Start()
    {
        // Get the second AudioSource component for sound effects
        AudioSource[] audioSources = GetComponents<AudioSource>();
        if (audioSources.Length > 1)
        {
            soundEffectSource = audioSources[1]; // Use the second AudioSource
        }
        else
        {
           // Debug.LogError("No second AudioSource found!");
        }

        UpdateScoreText();
        UpdateTimerDisplay();
    }

    void Update()
    {
        // Handle the timer countdown
        if (timerRunning)
        {
            totalTime -= Time.deltaTime;

            if (totalTime <= 0)
            {
                totalTime = 0;
                timerRunning = false;
                EndMeditationSession();
            }

            UpdateTimerDisplay();
        }

        // Handle the Space key press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceScore++;

            // Play sound effect if AudioSource is available
            if (soundEffectSource != null && !soundEffectSource.isPlaying)
            {
                soundEffectSource.Play();
            }

            UpdateScoreText();
        }
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
    }
}