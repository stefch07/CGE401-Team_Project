// Contributors: John Nguyen, Treasure Keys, Tyler Ptaszkowski

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // GameManager Reference + PauseMenu Reference
    public GameObject gameManager;
    private PauseMenu pauseMenu;

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
    }

    private void Update()
    {
        // Disables player movement and enables mouse movement/visibility if dialogPanel is active
        if (dialogManager.dialogPanel.activeSelf || PauseMenu.isPaused) {
            canMove = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else {
            canMove = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

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
    }
       

}
