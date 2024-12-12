using System;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;
    private bool dialogueComplete = false;
    private PlayerMovement currentPlayer;

    public void UpdateDialogueObject(DialogueObject dialogueObject)
    {
        this.dialogueObject = dialogueObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") & other.TryGetComponent(out PlayerMovement player))
        {
            player.Interactable = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") & other.TryGetComponent(out PlayerMovement player))
        {
            if (player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;
            }
        }
    }

    public void Interact(PlayerMovement player)
    {
        currentPlayer = player;
        
        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            if (responseEvents.DialogueObject == dialogueObject)
            {
                player.DialogueUI.AddResponseEvents(responseEvents.Events);
                break;
            }
        }

        player.DialogueUI.OnDialogueEnd += HandleDialogueEnd;
        player.DialogueUI.ShowDialogue(dialogueObject);
    }

    private void HandleDialogueEnd()
    {
        if (!dialogueComplete)
        {
            dialogueComplete = true;

            if (currentPlayer != null)
            {
                currentPlayer.DialogueUI.OnDialogueEnd -= HandleDialogueEnd;
            }

            if (FindObjectOfType<CampfireManager>() is CampfireManager campfireManager)
            {
                campfireManager.OnStoryComplete();
            }
        }
    }
}
