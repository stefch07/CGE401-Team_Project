using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManagerHiking : MonoBehaviour
{
    public TMP_Text dialogueText;
    public GameObject imageObject;
    private Queue<string> dialogueQueue;
    private bool isDialogueComplete = false;

    [System.Serializable]
    public class DialogueObject
    {
        public List<string> dialogueLines;

        public DialogueObject(List<string> lines)
        {
            dialogueLines = lines;
        }
    }

    void Start()
    {
        dialogueQueue = new Queue<string>();
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        isDialogueComplete = false;
        dialogueQueue.Clear();

        foreach (string line in dialogueObject.dialogueLines)
        {
            dialogueQueue.Enqueue(line);
        }

        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (dialogueQueue.Count == 0)
        {
            isDialogueComplete = true;
            dialogueText.text = "";

            gameObject.SetActive(false);

            if (imageObject != null)
            {
                imageObject.SetActive(false);
            }

            PlayerHiking player = FindObjectOfType<PlayerHiking>();
            if (player != null)
            {
                player.OnDialogueComplete();
            }
            return;
        }

        string line = dialogueQueue.Dequeue();
        dialogueText.text = line;
    }

    public bool IsDialogueComplete()
    {
        return isDialogueComplete;
    }
}
