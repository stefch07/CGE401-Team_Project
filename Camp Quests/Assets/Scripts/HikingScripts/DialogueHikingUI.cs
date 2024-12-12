using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHikingUI : MonoBehaviour
{
    [System.Serializable]
    public class DialogueObject
    {
        public List<string> dialogueLines;

        public DialogueObject(List<string> lines)
        {
            dialogueLines = lines;
        }
    }

    public Text dialogueText;
    private bool isDialogueComplete = false;
    private Queue<string> dialogueQueue;
    private DialogueObject currentDialogueObject;

    void Start()
    {
        dialogueQueue = new Queue<string>();
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        currentDialogueObject = dialogueObject;
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
