using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimator : MonoBehaviour
{
    // Reference to the Animator component
    private Animator animator;
    // Reference to the DialogManager
    public DialogManager dialogManager;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the dialogue box is open
        if (dialogManager.dialogPanel.activeSelf)
        {
            // Start the talking animation if it is not currently playing
            animator.SetTrigger("talk");
        }
        else
        {
            animator.SetTrigger("stopTalk");
        }
    }
}
