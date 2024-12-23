using System.Collections;
using UnityEngine;
using TMPro;

public class DialogManagerHubWorld : MonoBehaviour
{
    public TMP_Text textbox;
    public string[] sentences;
    private int index = 0;
    public float typingSpeed;

    public GameObject continueButton;
    public GameObject dialogPanel;
    
    public static bool hasSeen = false;

    void OnEnable()
    {
        index = 0;
        continueButton.SetActive(false);

        if (sentences.Length > 0)
        {
            if (!hasSeen) {
                StartCoroutine(Type());
            }
            else {
                dialogPanel.SetActive(false);
            }
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
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);

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
            hasSeen = true;
        }
    }
}
