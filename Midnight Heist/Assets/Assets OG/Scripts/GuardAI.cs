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

    private NavMeshAgent agent;        // Reference to the NavMeshAgent component
    private bool isPlayerInSight;      // Boolean to check if the player is in sight
    private bool isPlayerInRange;      // Boolean to check if the player is in range to stop chasing

    // Public property to expose visibility check
    public bool IsPlayerInSight => isPlayerInSight;

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

    private void OnDrawGizmosSelected()
    {
        // Draw sight range sphere in the Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        // Draw stop chasing range sphere
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stopChasingDistance);
    }
}
