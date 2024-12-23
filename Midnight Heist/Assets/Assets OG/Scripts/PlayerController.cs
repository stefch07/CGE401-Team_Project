// Contributors: John Nguyen, Treasure Keys, Tyler Ptaszkowski

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // General Notes:
    // This script handles the movement and camera controls for a player character in a 3D environment. 
    // The player can walk, run, jump, and rotate their camera. Movement is based on input and is affected 
    // by gravity when jumping or falling. The camera rotation is limited to prevent excessive vertical rotation.
    // A CharacterController component is required for handling the player's movement.

    public Camera playerCamera;  // Reference to the player's camera, used for rotation control.
    public float walkSpeed = 6.0f;  // Walking speed when not running.
    public float runSpeed = 12.0f;  // Running speed when the player holds the sprint key.
    public float jumpPower = 7.0f;  // The initial upward velocity applied when jumping.
    public float gravity = 15.0f;  // Gravitational force applied when the player is not grounded.

    // Crouching variables
    public float crouchSpeed = 3.0f;  // Movement speed while crouching.
    public float crouchHeight = 1.0f;  // Character controller height when crouching.
    public float standingHeight = 2.0f;  // Character controller height when standing.
    public float crouchScaleFactor = 0.75f;  // The scale factor applied when crouching.
    public Vector3 crouchCenter = new Vector3(0, 0.5f, 0);  // Center of character collider when crouching.
    public Vector3 standingCenter = new Vector3(0, 1.0f, 0);  // Center of character collider when standing.
    public Vector3 cameraCrouchPosition = new Vector3(0, 0.5f, 0);  // Camera position when crouched.
    public Vector3 cameraStandingPosition = new Vector3(0, 0.75f, 0);  // Camera position when standing.

    public bool isCrouching { get; private set; }

    public float lookSpeed = 2.0f;  // The speed at which the player can rotate their camera.
    public float lookXLimit = 45.0f;  // Limits the vertical angle of camera rotation to avoid extreme tilting.

    Vector3 moveDirection = Vector3.zero;  // The direction the player moves in, calculated from input.
    float rotationX = 0;  // Tracks the player's vertical camera rotation.

    public bool canMove = true;  // Determines if the player is allowed to move or rotate.

    CharacterController characterController;  // Reference to the CharacterController component, used for movement handling.

    private Vector3 originalScale;
    
    // DialogManager reference
    public DialogManager dialogManager;

    // GateDialogManager reference
    public GateDialogManager gateDialogManager;

    // GameManager Reference + PauseMenu Reference
    public GameObject gameManager;
    private PauseMenu pauseMenu;

    // Gold value to keep track of collected items
    public static int goldCounter;
    // Value for keeping track of rocks
    private int rockCounter;
    
    private bool isInDialogZone = false;  // Tracks if the player is in the dialog zone
    private GameObject currentDialogZone;  // Stores reference to the DialogZone's GameObject (with attached Canvas)
    
    public TMP_Text textbox;
    
    public GameObject panel;
    
    public static bool hasDiedOrWon = false;
    
    public GameObject notifPanel;
    private bool isPanelActive = false;
    
    private void Start()
    {
        // Initialize the character controller and set the cursor to be locked and invisible.
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor to the center of the screen.
        Cursor.visible = false;  // Hide the cursor during gameplay.

        // Store the original scale of the player.
        originalScale = transform.localScale;

        // Get reference to pauseMenu from the game manager
        if (gameManager != null)
        {
            pauseMenu = gameManager.GetComponent<PauseMenu>();
        }
        
        panel.SetActive(false);
        
        goldCounter = 0;

        RestartPlayerState();
       
    }

    private void RestartPlayerState()
    {
        moveDirection = Vector3.zero; // Reset movement direction
        canMove = true; // Allow movement
        isCrouching = false; // Reset crouching state

    }
    private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("DialogZone"))
            {
                isInDialogZone = true;
                currentDialogZone = other.gameObject;  // Store the DialogZone GameObject reference
                Debug.Log("Entered DialogZone.");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("DialogZone"))
            {
                isInDialogZone = false;
                currentDialogZone = null;  // Clear the reference when exiting the DialogZone
                Debug.Log("Exited DialogZone.");
            }
        }

    private bool previousPausedState = false;
    private bool previousDialogState = false;

    private void Update()
    {
        bool isDialogActive = dialogManager.dialogPanel.activeSelf;
        bool isPaused = PauseMenu.isPaused;

        // Update canMove and cursor states only when dialog or pause state changes
        if (isDialogActive != previousDialogState || isPaused != previousPausedState)
        {
            if (isDialogActive || isPaused)
            {
                canMove = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                panel.SetActive(false);  // Disable panel
            }
            else
            {
                canMove = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                panel.SetActive(true);  // Enable panel
            }

            previousDialogState = isDialogActive;
            previousPausedState = isPaused;
        }

        textbox.text = goldCounter + "G/230G";

        #region Handles Running and Crouching
        // Forward and right movement are calculated based on the player's current facing direction.
        Vector3 forward = transform.TransformDirection(Vector3.forward);  // Convert local forward direction to world space.
        Vector3 right = transform.TransformDirection(Vector3.right);  // Convert local right direction to world space.

        // Press left shift to run, C to crouch
         bool isRunning = Input.GetKey(KeyCode.LeftShift);  // Check if the player is holding the sprint key.
         bool isCrouching = Input.GetKey(KeyCode.C); // Check if the player is holding the crouch key.

        float curSpeed = walkSpeed; // Normal walking speed

        // Conditions to check if the player is 1) able to move and 2) is running or crouching
        // Adjusts speed and height accordingly
        if (canMove && isCrouching && !isRunning)
        {
            curSpeed = crouchSpeed;
            characterController.height = crouchHeight;  // Reduce character height
            characterController.center = crouchCenter;  // Adjust character center
            playerCamera.transform.localPosition = cameraCrouchPosition; // Move camera down to match first-person height

            transform.localScale = new Vector3(originalScale.x, originalScale.y * crouchScaleFactor, originalScale.z);
        }
        else if (canMove && isRunning && !isCrouching)
        {
            curSpeed = runSpeed;  // Set running speed
        }
        else
        {
            curSpeed = walkSpeed;  // Default to walking speed
            characterController.height = standingHeight;  // Reset to standing height
            characterController.center = standingCenter;  // Reset center
            playerCamera.transform.localPosition = cameraStandingPosition;

            // Reset the player scale when standing
            transform.localScale = originalScale;
        }

        float curSpeedX = canMove ? curSpeed * Input.GetAxis("Vertical") : 0;  // Calculate forward/backward movement.
        float curSpeedY = canMove ? curSpeed * Input.GetAxis("Horizontal") : 0;  // Calculate left/right movement.
        float movementDirectionY = moveDirection.y;  // Preserve the current vertical movement direction (important for gravity and jumping).

        moveDirection = (forward * curSpeedX) + (right * curSpeedY);  // Combine movement in the forward and right directions.

        #endregion


        #region Handles Jumping
        // Handle jumping when the player is grounded.
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;  // Apply upward velocity when the player jumps.
        }
        else
        {
            moveDirection.y = movementDirectionY;  // Maintain current vertical velocity when not jumping.
        }

        // Apply gravity if the player is in the air.
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;  // Decrease vertical velocity over time due to gravity.
        }

        #endregion

        #region Handles Rotation
        // Move the player character based on the calculated movement direction.
        characterController.Move(moveDirection * Time.deltaTime);  // Apply movement to the character controller, scaled by delta time.

        // Handle player and camera rotation if movement is allowed.
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;  // Adjust the vertical camera rotation based on mouse input.
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);  // Limit vertical camera rotation to prevent extreme angles.
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);  // Apply the vertical rotation to the camera.
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);  // Rotate the player horizontally based on mouse input.
        }

        #endregion

        #region Handles Interacting with Objects (NPCs, Stealable Items, Equipment)

        if (canMove && Input.GetKeyDown(KeyCode.E))
        {
            // Check for overlapping colliders, specifically items or NPCs
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.0f);
            bool itemCollected = false;

            foreach (var hitCollider in hitColliders)
            {
                // Check if the collided object is a StealableObject
                if (hitCollider.CompareTag("StealableItem"))
                {
                    StealableObject stealableObject = hitCollider.GetComponent<StealableObject>();
                    if (stealableObject != null)
                    {
                        itemCollected = true;
                        
                        // Check if the object is a Rock_Bag
                        if (stealableObject.gameObject.name == "Rock_Bag")
                        {
                            // Collect rocks instead of gold
                            int rocksCollected = stealableObject.GetRockCount();
                            FindObjectOfType<ThrowProjectiles>().AddRocks(rocksCollected);
                            Destroy(hitCollider.gameObject);
                            Debug.Log("Collected Rock_Bag with " + rocksCollected + " rocks.");
                        }
                        else // For regular stealable items that have gold values
                        {
                            // Update gold counter based on the item's name
                            goldCounter += stealableObject.GetGoldValue();
                            Destroy(hitCollider.gameObject);
                            Debug.Log("Collected item: " + stealableObject.gameObject.name);
                            Debug.Log("Current gold: " + goldCounter);
                        }
                    }
                }
            }
            if (!itemCollected) {
                StartCoroutine(ShowNotificationPanel());
            }
        }
        
        IEnumerator ShowNotificationPanel() {
            if (!isPanelActive)
            {
                isPanelActive = true;
                notifPanel.SetActive(true);
                yield return new WaitForSeconds(2);
                notifPanel.SetActive(false);
                isPanelActive = false;
            }
        }
        
        if (isInDialogZone && Input.GetKeyDown(KeyCode.E))
                {
                    // Find the Canvas component attached to the DialogZone's GameObject
                    Canvas dialogCanvas = currentDialogZone.GetComponentInChildren<Canvas>();

                    if (dialogCanvas != null)
                    {
                        // Toggle the canvas's active state
                        dialogCanvas.gameObject.SetActive(!dialogCanvas.gameObject.activeSelf);
                        Debug.Log("Toggled dialog canvas.");
                        // Disable player movement
                        canMove = false;
                    }
                    else
                    {
                        Debug.LogWarning("No Canvas found in the DialogZone.");
                    }
                }

        #endregion
    }


}
