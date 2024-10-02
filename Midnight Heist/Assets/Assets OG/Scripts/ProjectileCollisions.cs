using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisions : MonoBehaviour
{
    public float distractionRange;


    private void OnCollisionEnter(Collision collision)
    {
        // Check for nearby guards within the distraction range of the rock
        Collider[] guardsInRange = Physics.OverlapSphere(transform.position, distractionRange);

        foreach (Collider guard in guardsInRange)
        {
            GuardAI guardAI = guard.GetComponent<GuardAI>();

            // If a guard is found in range, make them turn
            if (guardAI != null)
            {
                guardAI.InvestigateSound(transform.position);
            }
        }

        // Destroy the projectile when it hits any object with collisions
        Destroy(gameObject);
    }
}
