using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageOnCollision : MonoBehaviour
{
    public int playerHealth = 100;
    public int damageAmount = 10;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Log"))
        {
            TakeDamage(damageAmount);
        }
    }

    void TakeDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player has died.");
        Destroy(gameObject);
    }
}
