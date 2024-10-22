using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Canvas dialogCanvas; // Assign the Canvas in the Inspector
    private PlayerController playerController;
    private bool isInDialogZone = false;

    void Start()
    {
        dialogCanvas.gameObject.SetActive(false); // Make sure the Canvas is initially hidden
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
        }
    }

    void ShowDialog()
    {
        // Set player movement to false
        if (playerController != null)
        {
            playerController.canMove = false;
        }

        // Show the dialog Canvas
        dialogCanvas.gameObject.SetActive(true);
    }

    public void HideDialog()
    {
        // Hide the dialog Canvas
        dialogCanvas.gameObject.SetActive(false);

        // Set player movement back to true
        playerController.canMove = true;
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
            isInDialogZone = false;
            HideDialog(); // Hide dialog if the player leaves the zone
        }
    }
}
