using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisions : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Destroy the projectile when it hits any object with collisions
        Destroy(gameObject);
    }
}
