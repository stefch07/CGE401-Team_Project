using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KayakController : MonoBehaviour
{
    public float verticalInput;
    public float speed = 10.0f;
    public float yRange = 10;
    
    // Update is called once per frame
    void Update()
    {
        // Get vertical input
        verticalInput = Input.GetAxis("Vertical");
        
        // transform vertically with input
        transform.Translate(Vector3.up * verticalInput * Time.deltaTime * speed);
        
        // keep player in bounds
        if (transform.position.y < -yRange) {
            transform.position = new Vector3(transform.position.x, -yRange, transform.position.z);
        }
        
        if (transform.position.y > yRange) {
            transform.position = new Vector3(transform.position.x, yRange, transform.position.z);
        }
    }
}
