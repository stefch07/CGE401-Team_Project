using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GateDialogManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    public string[] sentences;
    private int index = 0;
    public float typingSpeed = 0.05f;

    public GameObject yesButton;
    public GameObject noButton;
    public GameObject dialoguePanel;

    void Start()
    {
        dialoguePanel.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);

        yesButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnYesClicked);
        noButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnNoClicked);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialoguePanel.SetActive(true);
            StartCoroutine(Type());
        }
    }

    IEnumerator Type()
    {
        dialogueText.text = "";
        foreach (char letter in sentences[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        if (index < sentences.Length - 1)
        {
            index++;
            StartCoroutine(Type());
        }
        else
        {
            dialogueText.text = "Are you ready for the heist?";
            yesButton.SetActive(true);
            noButton.SetActive(true);
        }
    }

    void OnYesClicked()
    {
        SceneManager.LoadScene(4);  
    }

    void OnNoClicked()
    {
        dialoguePanel.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
    }
}
