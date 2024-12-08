using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHiking : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float jumpForce = 5f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private bool isGrounded;

    private bool isGameOver = false;
    public GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing! Please add one to the Player GameObject.");
        }
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGameOver)
        {
            rb.velocity = Vector2.zero;
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
    }

    void Jump()
    {
        if (rb == null) return;

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void StopPlayerMovement()
    {
        isGameOver = true;
    }

    public void ResumePlayerMovement()
    {
        isGameOver = false;
    }
}
