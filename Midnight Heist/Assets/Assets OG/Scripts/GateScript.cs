using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GateScript : MonoBehaviour
{
    public TMP_Text dialogueText;
    public string message = "Are you ready for the heist?";
    public float typingSpeed = 0.05f;
    public GameObject dialoguePanel;
    public Button yesButton;
    public Button noButton;
    private bool isShowingMessage = false;

    void Start()
    {
        dialoguePanel.SetActive(false);
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);

        yesButton.onClick.AddListener(OnYesClicked);
        noButton.onClick.AddListener(OnNoClicked);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding has the "Gate" tag
        if (other.CompareTag("Gate") && !isShowingMessage)
        {
            StartCoroutine(TypeMessage(message));
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Hide dialogue if the player exits the trigger while the message is displayed
        if (other.CompareTag("Gate") && isShowingMessage)
        {
            HideDialogue();
        }
    }

    IEnumerator TypeMessage(string message)
    {
        isShowingMessage = true;
        dialoguePanel.SetActive(true);
        dialogueText.text = "";

        foreach (char letter in message.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
    }

    void OnYesClicked()
    {
        SceneManager.LoadScene(5);
    }

    void OnNoClicked()
    {
        HideDialogue();
    }

    private void HideDialogue()
    {
        dialoguePanel.SetActive(false);
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        isShowingMessage = false; // Resetting the message state
    }
}
