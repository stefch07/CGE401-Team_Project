using System.Collections;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TMP_Text textbox;
    public string[] sentences;
    private int index = 0;
    public float typingSpeed;

    public GameObject continueButton;
    public GameObject skipButton;
    public GameObject dialogPanel;

    public ConcentrationMeter concentrationMeter; // Reference to start the timer
    public MeditationTimer meditationTimer; // Alternative reference for MeditationTimer

    public bool isTextFullyDisplayed = false;

    void OnEnable()
    {
        index = 0;
        continueButton.SetActive(false);
        skipButton.SetActive(false);
        isTextFullyDisplayed = false;

        if (sentences.Length > 0)
        {
            StartCoroutine(Type());
        }
        else
        {
            Debug.LogWarning("No sentences found in the array.");
            dialogPanel.SetActive(false);
            StartTimer(); // Start the timer if no sentences exist
        }
    }

    IEnumerator Type()
    {
        textbox.text = "";
        foreach (char letter in sentences[index])
        {
            textbox.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        continueButton.SetActive(true);
        skipButton.SetActive(true);
        isTextFullyDisplayed = true;
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);
        skipButton.SetActive(false);
        isTextFullyDisplayed = false;

        if (index < sentences.Length - 1)
        {
            index++;
            textbox.text = "";
            StartCoroutine(Type());
        }
        else
        {
            EndDialog();
        }
    }

    public void SkipSentences()
    {
        continueButton.SetActive(false);
        skipButton.SetActive(false);
        EndDialog();
    }

    private void EndDialog()
    {
        dialogPanel.SetActive(false);
        isTextFullyDisplayed = true;
        StartTimer();
    }

    private void StartTimer()
    {
        if (concentrationMeter != null)
        {
            concentrationMeter.StartTimer();
        }

        if (meditationTimer != null)
        {
            meditationTimer.StartTimer();
        }
    }
}