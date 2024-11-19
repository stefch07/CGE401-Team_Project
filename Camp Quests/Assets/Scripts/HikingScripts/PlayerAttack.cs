using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damageAmount = 10;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1f);

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Log"))
            {
                DestroyLog(collider.gameObject);
            }
        }
    }

    void DestroyLog(GameObject log)
    {
        Destroy(log);
        Debug.Log("Log destroyed!");
    }
}
