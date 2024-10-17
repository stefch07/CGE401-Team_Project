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
    public float turnSpeed = 5f;       // Controls how fast guards turn in reaction to sound

    public Transform guardChest;       // Reference to the guard's chest position

    private NavMeshAgent agent;        // Reference to the NavMeshAgent component
    private bool isPlayerInSight;      // Boolean to check if the player is in sight
    private bool isPlayerInRange;      // Boolean to check if the player is in range to stop chasing
    private Vector3 soundSource;       // Stores the sound source position of rocks that impact walls/floors
    private bool investigatingSound;   // Checks if the guard is investigating a sound

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

        // Handle movement
        if (isPlayerInSight && isPlayerInRange && agent.isOnNavMesh)  // Check if agent is on NavMesh
        {
            // Move towards the player
            agent.SetDestination(player.position);
        }
        // If the guard "heard" a rock hit the wall or floor
        else if (investigatingSound && agent.isOnNavMesh)
        {
            InvestigateSoundLocation();
        }
        else
        {
            // Stay still
            if (agent.isOnNavMesh)
            {
                agent.ResetPath();
            }
        }
    }

    private void InvestigateSoundLocation()
    {
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(soundSource, path);

        // If valid path to where rock landed, allow guard to go therre
        if (path.status == NavMeshPathStatus.PathComplete)
        {
            agent.SetDestination(soundSource);
        }
        // Otherwise, if path is not valid, meaning no way to get there, do not investigate sound
        else
        {
            investigatingSound = false;
            agent.ResetPath();
        }
    }

    // Returns true if the guard can see any part of the player
    public bool CheckVisibility()
    {
        // Determine the height of the capsule based on whether the player is crouching
        float capsuleHeight = isCrouching ? crouchingHeight : standingHeight;

        // Calculate approximate positions for the player's "head" and "feet" based on the height
        Vector3 playerHead = player.position + Vector3.up * (capsuleHeight / 2);
        Vector3 playerFeet = player.position - Vector3.up * (capsuleHeight / 2);

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
