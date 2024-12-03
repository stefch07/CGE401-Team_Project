using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;
    
    public DialogueUI DialogueUI => dialogueUI;
    
    public IInteractable Interactable { get; set; }
    
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private Vector3 movement;

   
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }
    // Update is called once per frame
    void Update()
    {
        if (dialogueUI.IsOpen) return;
        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");

        movement = movement.normalized * moveSpeed;


        if(moveSpeed == 0)
        {
            moveSpeed = 0;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interactable?.Interact(this);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("KayakMinigame");
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("Meditation");
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene("WinScreen");
        }
    }

    void FixedUpdate()
    {
        Vector3 newPosition = rb.position + movement * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }
}
