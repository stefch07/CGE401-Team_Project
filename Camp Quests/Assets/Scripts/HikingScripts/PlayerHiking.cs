using UnityEngine;

public class PlayerHiking : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float jumpForce = 5f;

    private Rigidbody2D rb;
    private bool isGrounded;

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
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded)
        {
            Jump();
        }
    }

    void MovePlayer()
    {
        if (rb == null) return;

        float moveX = Input.GetAxis("Horizontal");

        // Move the player left or right
        Vector2 move = new Vector2(moveX, 0f);
        rb.MovePosition(rb.position + move * walkSpeed * Time.deltaTime);
    }

    void Jump()
    {
        if (rb == null) return;

        // Apply a vertical force for the jump
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
