/*
Contributors: John, Treasure Keys
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class EnemyVisibility : MonoBehaviour
{
    // Object we're looking for
    public Transform target = null;

    // If object is more than this distance away, we can't see it
    public float maxDistance = 10.0f;

    // Angle of arc of visibility
    [Range(0f, 360f)]
    public float angle = 45f;

    // If true, visualize changes in visibility by changing material color
    [SerializeField] private bool visualize = true;

    // Property that other classes can access to determine if we can currently see our target
    public bool targetIsVisible { get; private set; }

    // Player height variables
    public float standingHeight = 2f;  // Height of the player when standing
    public float crouchingHeight = 1f; // Height of the player when crouching

    // Variables for movement detection
    private Vector3 lastPosition;
    private float movementThreshold = 0.01f; // Movement threshold for detecting motion
    private Renderer guardRenderer; // Store reference to the Renderer component

    void Start()
    {
        // Initialize the lastPosition and get the Renderer component
        lastPosition = transform.position;
        guardRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        targetIsVisible = CheckVisibility();

        // Check if the guard is moving
        bool isMoving = Vector3.Distance(transform.position, lastPosition) > movementThreshold;
        lastPosition = transform.position; // Update lastPosition to current position

        // Calculate distance to the target
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (visualize)
        {
            // Update color based on movement and visibility
            if (isMoving)
            {
                SetColor(Color.red); // Turn red if moving
            }
            else if (targetIsVisible && distanceToTarget < maxDistance) // Check visibility and distance
            {
                SetColor(Color.yellow); // Target is visible and within range
            }
            else if (CheckVisibilityToPoint(target.position))
            {
                SetColor(Color.yellow); // Close but not visible (crouched, etc.)
            }
            else
            {
                SetColor(Color.green); // Not close or not visible
            }
        }
    }

    // Set the color of the guard
    private void SetColor(Color color)
    {
        guardRenderer.material.color = color;
    }

    // Returns true if this object can see the specified position
    public bool CheckVisibilityToPoint(Vector3 worldPoint)
    {
        // Determine player height based on crouching state
        float playerHeight = IsPlayerCrouching() ? crouchingHeight : standingHeight;

        // Calculate the position to check based on player height
        Vector3 playerCheckPosition = new Vector3(worldPoint.x, worldPoint.y + playerHeight, worldPoint.z);

        // Calculate the direction from our location to the point
        var directionToTarget = playerCheckPosition - transform.position;

        // Calculate the number of degrees from the forward direction
        var degreesToTarget = Vector3.Angle(transform.forward, directionToTarget);

        // Target is within the arc if it's within half of the specified angle
        var withinArc = degreesToTarget < (angle / 2);
        if (!withinArc) return false;

        // Figure out distance to the target
        var distanceToTarget = directionToTarget.magnitude;
        var rayDistance = Mathf.Min(maxDistance, distanceToTarget);

        // Create a new ray going from current location, in the specified direction
        var ray = new Ray(transform.position, directionToTarget);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            return hit.collider.transform == target; // Check if we hit the target
        }
        return true; // Unobstructed line of sight
    }

    // Returns true if a straight line can be drawn between this object and the target
    public bool CheckVisibility()
    {
        return CheckVisibilityToPoint(target.position);
    }

    // Checks if the player is crouching
    private bool IsPlayerCrouching()
    {
        // Ensure target is assigned and has a PlayerController component
        if (target != null)
        {
            var playerController = target.GetComponent<PlayerController>();
            if (playerController != null)
            {
                return playerController.isCrouching; // Ensure isCrouching exists in PlayerController
            }
        }
        return false; // Default to not crouching if target is null or doesn't have PlayerController
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(EnemyVisibility))]
public class EnemyVisibilityEditor : Editor
{
    private void OnSceneGUI()
    {
        var visibility = target as EnemyVisibility;

        Handles.color = new Color(1, 1, 1, 0.1f);
        var forwardPointMinusHalfAngle = Quaternion.Euler(0, -visibility.angle / 2, 0) * visibility.transform.forward;
        Vector3 arcStart = forwardPointMinusHalfAngle * visibility.maxDistance;

        Handles.DrawSolidArc(
            visibility.transform.position,
            Vector3.up,
            arcStart,
            visibility.angle,
            visibility.maxDistance
        );

        Handles.color = Color.white;
        Vector3 handlePosition = visibility.transform.position + visibility.transform.forward * visibility.maxDistance;
        visibility.maxDistance = Handles.ScaleValueHandle(
            visibility.maxDistance,
            handlePosition,
            visibility.transform.rotation,
            1,
            Handles.ConeHandleCap,
            0.25f
        );
    }
}
#endif
