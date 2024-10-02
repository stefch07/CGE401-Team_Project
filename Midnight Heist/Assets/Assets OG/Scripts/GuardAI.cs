/*
 Contributors: Treasure Keys
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : MonoBehaviour
{
    public Transform player;           // Reference to the player's position
    public float sightRange = 15f;     // The range at which the guard can see the player
    public float stopChasingDistance = 20f;  // Distance at which the guard will stop chasing
    public float movementSpeed = 3.5f; // Guard movement speed
    public float turnSpeed = 5f; // Controls how fast guards turn in reaction to sound

    private NavMeshAgent agent;        // Reference to the NavMeshAgent component
    private bool isPlayerInSight;      // Boolean to check if the player is in sight
    private bool isPlayerInRange;      // Boolean to check if the player is in range to stop chasing

    private Vector3 soundSource;       // Stores the sound source position of rocks that impact walls/floors
    private bool investigatingSound;   // Checks if the guard is investigating a sound

    // Public property to expose the visibility state
    public bool IsPlayerInSight => isPlayerInSight; // Expose visibility check

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
        else if (investigatingSound && agent.isOnNavMesh) {

            // Turn towards the sound source
            Vector3 direction = (soundSource - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);

            agent.SetDestination(soundSource);

            if (Vector3.Distance(transform.position, soundSource) < 1f) {
                investigatingSound = false;
                agent.ResetPath();
            }
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

    // Returns true if the guard can see the player (not blocked by obstacles)
    public bool CheckVisibility()
    {
        // Compute direction to the player
        Vector3 directionToPlayer = player.position - transform.position;

        // Cast a ray towards the player
        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, sightRange))
        {
            // Check if the ray hit the player (and not an obstacle)
            if (hit.collider.transform == player)
            {
                return true;
            }
        }
        return false;
    }

    // Gets called when a guard hears a sound
    public void InvestigateSound(Vector3 soundPosition)
    {
        soundSource = soundPosition;
        investigatingSound = true;
    }
}
