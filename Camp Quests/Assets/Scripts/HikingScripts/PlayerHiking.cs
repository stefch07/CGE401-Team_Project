using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHiking : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float jumpForce = 5f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    public AudioSource musicSource;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isGrounded;

    private bool isGameOver = false;
    public GameManager gameManager;

    public TMP_Text dialogueText;
    private bool isDialogueComplete = false;
    private DialogueManagerHiking dialogueManager;

    private bool canJump = true; // Prevents jump spamming
    public float jumpCooldown = 0.5f; // Cooldown time in seconds

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
        dialogueManager = FindObjectOfType<DialogueManagerHiking>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing! Please add one to the Player GameObject.");
        }

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is missing! Please add one to the Player GameObject.");
        }

        if (animator == null)
        {
            Debug.LogError("Animator component is missing! Please add one to the Player GameObject.");
        }

        if (musicSource == null)
        {
            Debug.LogError("AudioSource is not assigned! Please attach an AudioSource component to the Player GameObject and assign it in the Inspector.");
            return;
        }

        musicSource.loop = true;
        musicSource.playOnAwake = true;
        musicSource.volume = 0.5f;
        musicSource.Play();

        StartDialogue();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGameOver)
        {
            rb.velocity = Vector2.zero;
            animator.SetFloat("Speed", 0);
            return;
        }

        if (!isDialogueComplete)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                dialogueManager.DisplayNextLine();
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            Jump();
        }

        MovePlayer();
    }

    void MovePlayer()
    {
        if (rb == null) return;

        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * walkSpeed, rb.velocity.y);

        if (moveX > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveX < 0)
        {
            spriteRenderer.flipX = true;
        }

        animator.SetFloat("Speed", Mathf.Abs(moveX));
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("VerticalSpeed", rb.velocity.y);
    }

    void Jump()
    {
        if (rb == null) return;

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        animator.SetTrigger("Jump");
        canJump = false;
        StartCoroutine(JumpCooldown());
    }

    IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }

    public void TakeDamage()
    {
        if (isGameOver) return;

        animator.SetTrigger("Hurt");
    }

    public void Die()
    {
        isGameOver = true;
        animator.SetBool("IsDead", true);
        rb.velocity = Vector2.zero; 
        this.enabled = false; 
        gameManager.GameOver();
    }

    void StartDialogue()
    {
        DialogueManagerHiking.DialogueObject introDialogue = new DialogueManagerHiking.DialogueObject(
            new List<string>
            {
                "Welcome to the hiking adventure!" +
                "(Click Space Bar to Keep Reading)",
                "First you need to know how to move around!",
                "Move Left and Right with AD or Left/Right Arrow Keys.",
                "Jump over logs with Space Bar to avoid losing hearts",
                "Press E to interact with animals to gain Satisfaction!",
                "Enjoy your hiking trip!"
            }
        );

        dialogueManager.ShowDialogue(introDialogue);
    }

    public void StopPlayerMovement()
    {
        isGameOver = true;
        isDialogueComplete = false;
    }

    public void ResumePlayerMovement()
    {
        isGameOver = false;
        isDialogueComplete = true;
    }

    public void OnDialogueComplete()
    {
        ResumePlayerMovement();
    }
}
