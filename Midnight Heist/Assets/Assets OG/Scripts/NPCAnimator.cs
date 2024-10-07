using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimator : MonoBehaviour
{
    // Reference to the Animator component
    private Animator animator;

    // Time to wait before stopping the talking animation
    public float stopDelay = 1.5f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayTalkingAnimation()
    {
        Debug.Log($"PlayTalkingAnimation called on NPC: {gameObject.name}");

        // Start the talking animation when the continue button is pressed
        animator.SetTrigger("talk");

        // Start the coroutine to stop talking after the delay
        StartCoroutine(StopTalkingWithDelay());
    }

    private IEnumerator StopTalkingWithDelay()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(stopDelay);

        // Stop the talking animation
        animator.SetTrigger("stopTalk");
    }
}