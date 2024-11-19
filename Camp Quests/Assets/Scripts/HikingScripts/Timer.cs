using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeLimit = 30f;
    private float timeRemaining;
    public Text timerText;

    void Start()
    {
        timeRemaining = timeLimit;
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        else
        {
            timeRemaining = 0;
            TimerEnded();
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        int seconds = Mathf.FloorToInt(timeToDisplay);
        timerText.text = string.Format("{0:00}", seconds);
    }

    void TimerEnded()
    {
        Debug.Log("Timer ended!");
    }
}
