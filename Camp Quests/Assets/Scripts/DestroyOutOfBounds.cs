using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public float leftBound = 30;
    
    // Update is called once per frame
    void Update()
    {
        // Destroy object if it goes out of bounds
        if (transform.position.x > leftBound) {
            Destroy(gameObject);
        }
    }
}
