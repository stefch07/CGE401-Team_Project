using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageOnCollision : MonoBehaviour
{
    public HealthSystemX healthSystem;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Log"))
        {
            if (healthSystem != null)
            {
                healthSystem.TakeDamage();
                Destroy(collision.gameObject);
            }

        }
    }
}
