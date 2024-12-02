using UnityEngine;

public class ClockManager : MonoBehaviour
{
    public DialogManager dialogManager;
    public bool clockIsRunning = false;

    void Start()
    {
        // Freeze time initially
        Time.timeScale = 0;
    }

    void Update()
    {
        if (dialogManager.isTextFullyDisplayed && !clockIsRunning)
        {
            StartClock();
        }
    }

    void StartClock()
    {
        clockIsRunning = true;
        // Resume time, but make sure timer uses unscaled time for independent countdown
        Time.timeScale = 1;
        Debug.Log("Clock has started.");
    }
}