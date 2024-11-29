using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KayakController : MonoBehaviour
{
    public float verticalInput;
    public float horizontalInput;
    public float speed = 10.0f;
    public float xRange = 5.0f;
    public float yRange = 2.5f;
    public float val1 = 1.0f;
    public float val2 = 1.0f;
    
    public AudioSource backgroundMusic;
    
    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        transform.Translate(Vector3.up * verticalInput * Time.deltaTime * speed);
        
        // keep player in bounds
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        
        if (transform.position.y < (-yRange - val1)) {
            transform.position = new Vector3(transform.position.x, (-yRange - val1), transform.position.z);
        }
        
        if (transform.position.y > (yRange - val2)) {
            transform.position = new Vector3(transform.position.x, (yRange - val2), transform.position.z);
        }
    }
}
