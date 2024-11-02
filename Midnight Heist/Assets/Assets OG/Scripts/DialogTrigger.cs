using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Canvas dialogCanvas; // Assign the Canvas in the Inspector
    public PlayerController playerController;
    private bool isInDialogZone = false;
    public GameObject panel;
    public GameObject deleteThis;

    private Collider npcCollider; // Reference to the NPC's collider

    void Start()
    {
        dialogCanvas.gameObject.SetActive(false); // Make sure the Canvas is initially hidden
        npcCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (isInDialogZone && Input.GetKeyDown(KeyCode.T))
        {
            ShowDialog();
        }
        
        if (dialogCanvas.gameObject.activeSelf) {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            panel.SetActive(false);
        }
        
        /*if (deleteThis == null) {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
            playerController.canMove = true;
            panel.SetActive(true);
        }*/
    }

    void ShowDialog()
    {
        // Set player movement to false
        if (playerController != null && deleteThis != null)
        {
            playerController.canMove = false;
        }

        // Disable the NPC collider to prevent re-entry
        if (npcCollider != null)
        {
            npcCollider.enabled = false;
        }

        // Show the dialog Canvas
        dialogCanvas.gameObject.SetActive(true);
    }

    public void HideDialog()
    {
        // Hide the dialog Canvas
        dialogCanvas.gameObject.SetActive(false);

        // Reset the dialog zone state
        isInDialogZone = false; // Ensure this is set to false when hiding the dialog
        
        if (deleteThis != null)
        {
            Destroy(deleteThis);
        }
        
        // Set cursor state
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerController.canMove = true;
        panel.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<PlayerController>();
            isInDialogZone = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInDialogZone = false; // Update the state
            // Hide dialog if the player exits the zone while dialog is active
            if (dialogCanvas.gameObject.activeSelf)
            {
                HideDialog();
            }
        }
    }
}
