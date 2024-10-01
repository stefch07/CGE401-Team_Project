using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Must add this to use TMP_Text
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TMP_Text textbox;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    
    // Set these references in the inspector
    public GameObject continueButton;
    public GameObject skipButton;
    public GameObject tavernkeeperButton;
    public GameObject dialogPanel;
    
    void OnEnable()
    {
        continueButton.SetActive(false);
        skipButton.SetActive(false);
        StartCoroutine(Type());
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
        //skipButton.SetActive(true);
    }
    
    public void NextSentence()
    {
        continueButton.SetActive(false);
        skipButton.SetActive(false);
        if (index < sentences.Length - 1)
        {
            index++;
            textbox.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textbox.text = "";
            tavernkeeperButton.SetActive(false);
            dialogPanel.SetActive(false);
        }
    }
    
    public void SkipSentences() {
        continueButton.SetActive(false);
        skipButton.SetActive(false);
        tavernkeeperButton.SetActive(false);
        dialogPanel.SetActive(false);
    }
}
