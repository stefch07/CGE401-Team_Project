using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondFloorTriggerExit : MonoBehaviour
{
    private BoxCollider boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject colliderObject = GameObject.FindGameObjectWithTag("Collider");
            if (colliderObject != null)
            {
                Destroy(colliderObject);
            }
        }
    }
}
