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

    // Public flag to signal when text is fully displayed
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
        }
    }

    IEnumerator Type()
    {
        textbox.text = "";
        foreach (char letter in sentences[index])
        {
            textbox.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        continueButton.SetActive(true);
        skipButton.SetActive(true);
        isTextFullyDisplayed = true;  // Text is now fully displayed
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);
        skipButton.SetActive(false);
        isTextFullyDisplayed = false;  // Reset flag as new text is loading

        if (index < sentences.Length - 1)
        {
            index++;
            textbox.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textbox.text = "";
            dialogPanel.SetActive(false);
        }
    }

    public void SkipSentences()
    {
        continueButton.SetActive(false);
        skipButton.SetActive(false);
        dialogPanel.SetActive(false);
        isTextFullyDisplayed = true; // Mark as fully displayed
    }
}
