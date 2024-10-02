using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAnimator : MonoBehaviour
{
    private Animator animator;
    private GuardAI guardAI;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        guardAI = GetComponent<GuardAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (guardAI != null)
        {
            float speed = guardAI.AgentVelocity.magnitude;
            animator.SetFloat("Speed", speed);
        }
    }
}
