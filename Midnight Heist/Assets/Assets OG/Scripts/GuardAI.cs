/*
Contributors: Treasure Keys
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : MonoBehaviour
{
    public Transform player;           // Reference to the player's position (center of the capsule)
    public float standingHeight = 2f;  // Height of the player when standing
    public float crouchingHeight = 1f; // Height of the player when crouching
    public float sightRange = 15f;     // The range at which the guard can see the player
    public float stopChasingDistance = 20f;  // Distance at which the guard will stop chasing
    public float movementSpeed = 3.5f; // Guard movement speed
    public float returnSpeed = 2f; // Speed guards use to return to their original position
    public float turnSpeed = 30f;       // Controls how fast guards turn in reaction to sound
    public float minDistanceBetweenGuards = 2f; // Minimum distance between guards when investigating sound

    public Transform guardChest;       // Reference to the guard's chest position

    private static List<GuardAI> activeGuards = new List<GuardAI>();
    private NavMeshAgent agent;        // Reference to the NavMeshAgent component
    private bool isPlayerInSight;      // Boolean to check if the player is in sight
    private bool isPlayerInRange;      // Boolean to check if the player is in range to stop chasing
    private Vector3 soundSource;       // Stores the sound source position of rocks that impact walls/floors
    private bool investigatingSound;   // Checks if the guard is investigating a sound
    public float investigationTime = 5f; // Time in seconds a guard will investigate sound before stopping
    private Vector3 originalPosition;
    private bool returningToOriginalPosition = false; // Flag to track if returning to original position
    private bool investigationComplete = false;

    private bool isCrouching;          // Tracks if the player is crouching

    // Public property to expose the visibility state
    public bool IsPlayerInSight => isPlayerInSight; // Expose visibility check
    public NavMeshAgent Agent => agent;
    public Vector3 AgentVelocity => agent.velocity;

    void Start()
    {
        // Get the NavMeshAgent component from the guard
        agent = GetComponent<NavMeshAgent>();
        // Set the initial movement speed
        agent.speed = movementSpeed;
        originalPosition = transform.position; // Set original position for guard
        activeGuards.Add(this); // Add this guard to the list of active guards in the scene
    }

    private void OnDestroy()
    {
        activeGuards.Remove(this); // Remove guard from the list when destroyed
    }

    void Update()
    {
        // Calculate the distance between the guard and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is crouching
        isCrouching = CheckIfPlayerIsCrouching();

        // Check if the player is within sight range
        isPlayerInSight = CheckVisibility();

        // Check if the player is within the stop chasing range
        isPlayerInRange = distanceToPlayer <= stopChasingDistance;

        // Handle chasing the player if in sight and in range
        if (isPlayerInSight && isPlayerInRange && agent.isOnNavMesh)
        {
            agent.SetDestination(player.position);
        }
        // Handle sound investigation
        else if (investigatingSound && agent.isOnNavMesh)
        {
            // This guard should still investigate the sound if it is not currently returning
            if (!returningToOriginalPosition)
            {
                // Check if another guard is closer to the sound source
                GuardAI closestGuard = GetClosestGuardToSound();
                if (closestGuard != null && closestGuard != this && Vector3.Distance(closestGuard.transform.position, soundSource) < minDistanceBetweenGuards)
                {
                    // If another guard is closer, return to the original position
                    ReturnToOriginalPosition();
                }
                else
                {
                    // If this guard is the closest or no one is close enough, investigate the sound
                    InvestigateSoundLocation();
                }
            }
        }
        // Handle returning to the original position if no sound or player in sight
        else if (returningToOriginalPosition && agent.isOnNavMesh)
        {
            // If the guard is near their original position, stop and reset speed
            if (Vector3.Distance(transform.position, originalPosition) < 0.5f)
            {
                agent.speed = movementSpeed;
                returningToOriginalPosition = false;
                agent.ResetPath();
            }
        }
        // Reset path when idle
        else if (agent.isOnNavMesh)
        {
            agent.ResetPath();
        }
    }

    private GuardAI GetClosestGuardToSound()
    {
        GuardAI closestGuard = null;
        float closestDistance = float.MaxValue;

        foreach (GuardAI guard in activeGuards)
        {
            float distance = Vector3.Distance(guard.transform.position, soundSource);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestGuard = guard;
            }
        }

        return closestGuard;
    }

    private void InvestigateSoundLocation()
    {
        if (investigationComplete)
        {
            // Another guard has already completed the investigation
            ReturnToOriginalPosition();
            return;
        }

        // Set the destination to the sound source, rather than just calculating a path
        if (agent.isOnNavMesh)
        {
            agent.SetDestination(soundSource);  // Ensure guard moves to the sound source
        }

        // Check if the guard is close enough to the sound source (within 0.5f range)
        if (Vector3.Distance(transform.position, soundSource) <= 0.5f && agent.remainingDistance <= 0.5f)
        {
            StartCoroutine(LookAroundAndReturn());
        }
    }

    private IEnumerator LookAroundAndReturn()
    {
        // Pause guard movement
        agent.ResetPath();
        investigatingSound = false;  // Stop investigating after arriving

        float rotateSpeed = 45f;
        float investigationDuration = 2f;

        // Rotate in place for the given duration
        for (float time = 0; time < investigationDuration; time += Time.deltaTime)
        {
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
            yield return null;
        }

        // Mark the investigation as complete once this guard finishes.
        investigationComplete = true;

        // After looking around, return to the original position
        ReturnToOriginalPosition();
    }

    private void ReturnToOriginalPosition()
    {
        if (agent.isOnNavMesh)
        {
            agent.speed = returnSpeed;
            agent.SetDestination(originalPosition);
            returningToOriginalPosition = true;
        }

        // Once guard reaches their original position
        if (Vector3.Distance(transform.position, originalPosition) < 0.5f)
        {
            agent.speed = movementSpeed;
            returningToOriginalPosition = false;
            agent.ResetPath();

            // This guard is done investigating
            investigatingSound = false;

            // If all guards are back at their spots, reset the investigation complete flag
            if (AreAllGuardsAtOriginalPosition())
            {
                investigationComplete = false; // Allow future investigations
            }
        }
    }

    private bool AreAllGuardsAtOriginalPosition()
    {
        GuardAI[] allGuards = FindObjectsOfType<GuardAI>();

        foreach (GuardAI guard in allGuards)
        {
            if (Vector3.Distance(guard.transform.position, guard.originalPosition) > 0.5f)
            {
                return false;
            }
        }

        return true;
    }

    // Returns true if the guard can see any part of the player
    public bool CheckVisibility()
    {
        // Determine the height of the capsule based on whether the player is crouching
        float capsuleHeight = isCrouching ? crouchingHeight : standingHeight;

        // Calculate approximate positions for the player's "head" and "feet" based on the height
        Vector3 playerHead = player.position + Vector3.up * (capsuleHeight / 2);
        Vector3 playerFeet = player.position - Vector3.up * (capsuleHeight / 2);

        // Check if the player is behind the guard
        Vector3 guardToPlayer = player.position - transform.position;
        float dotProduct = Vector3.Dot(guardToPlayer.normalized, transform.forward);

        // If the dot product is negative, the player is behind the guard (angle > 90 degrees)
        if (dotProduct < 0)
        {
            return false; // Player is behind the guard, cannot be seen
        }

        // Cast rays from the guard's chest towards the player's center, head, and feet
        bool canSeeCenter = CastRayToPlayerPart(player.position);  // Center of the capsule
        bool canSeeHead = CastRayToPlayerPart(playerHead);         // Top of the capsule
        bool canSeeFeet = CastRayToPlayerPart(playerFeet);         // Bottom of the capsule

        // Return true if any part of the player's capsule is visible
        return canSeeCenter || canSeeHead || canSeeFeet;
    }

    // Helper method to cast a ray from the guard's chest to a specific player part (center, head, feet)
    private bool CastRayToPlayerPart(Vector3 targetPosition)
    {
        Vector3 directionToTarget = targetPosition - guardChest.position;

        RaycastHit hit;
        if (Physics.Raycast(guardChest.position, directionToTarget, out hit, sightRange))
        {
            // Check if the ray hit the player (and not an obstacle)
            if (hit.collider.transform == player)
            {
                return true;
            }
        }
        return false;
    }

    // This method should be connected to your player's crouch logic
    private bool CheckIfPlayerIsCrouching()
    {
        // Return true if the player is crouching (this should come from your player's movement or input script)
        return false;  // Replace this with actual crouch detection logic from your player controller
    }

    // Gets called when a guard hears a sound
    public void InvestigateSound(Vector3 soundPosition)
    {
        soundSource = soundPosition;
        investigatingSound = true;
    }
}
