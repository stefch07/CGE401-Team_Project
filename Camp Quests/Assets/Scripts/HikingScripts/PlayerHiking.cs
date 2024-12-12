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
    private bool isGrounded;

    private bool isGameOver = false;
    public GameManager gameManager;

    public TMP_Text dialogueText;
    private bool isDialogueComplete = false;
    private DialogueManagerHiking dialogueManager;

    

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

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing! Please add one to the Player GameObject.");
        }

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is missing! Please add one to the Player GameObject.");
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

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
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
    }

    void Jump()
    {
        if (rb == null) return;

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void StartDialogue()
    {
        DialogueManagerHiking.DialogueObject introDialogue = new DialogueManagerHiking.DialogueObject(
            new List<string>
            {
                "Welcome to the hiking adventure!",
                "First you need to know how to move around!",
                "Move Left and Right with AD or Left/Right Arrow Keys.",
                "Jump over logs with W or Space Bar to avoid losing hearts",
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
