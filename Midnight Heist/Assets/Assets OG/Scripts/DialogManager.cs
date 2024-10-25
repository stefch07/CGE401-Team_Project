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

    public NPCAnimator initialNPC; // Assign this in the Inspector for the initial NPC
    private NPCAnimator activeNPC; // The NPC currently in dialog w/ player
    
    public GameObject deleteThis;

    private void Start()
    {
        if (dialogPanel.activeSelf && initialNPC != null)
        {
            SetActiveNPC(initialNPC);
        }
    }

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
        if (PlayerController.hasDiedOrWon) {
            skipButton.SetActive(true);
        }
        else {
            skipButton.SetActive(false);
        }
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);
        skipButton.SetActive(false);

        // Call the talking animation directly on the active NPC
        if (activeNPC != null)
        {
            Debug.Log($"Calling PlayTalkingAnimation on active NPC: {activeNPC.gameObject.name}");
            activeNPC.PlayTalkingAnimation();
        }
        else
        {
            Debug.LogWarning("No active NPC set in DialogManager.");
        }

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
            ClearActiveNPC();

            // Destroy the object when the dialog ends
            if (deleteThis != null)
            {
                Destroy(deleteThis);
            }
        }
    }

    // Set the active NPC's animator
    public void SetActiveNPC(NPCAnimator npc)
    {
        Debug.Log($"SetActiveNPC called. Active NPC set to: {npc.gameObject.name}");
        activeNPC = npc;
    }

    // Clear the active NPC ref at the end of dialogue
    private void ClearActiveNPC()
    {
        Debug.Log("Clearing active NPC.");
        activeNPC = null;
    }

    public void SkipSentences()
    {
        continueButton.SetActive(false);
        skipButton.SetActive(false);
        tavernkeeperButton.SetActive(false);
        dialogPanel.SetActive(false);
    }
}
