using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public float speed;
    private float elapsedTime = 0f;
    
    public float lowerBound = 1.0f;
    public float upperBound = 4.0f;
    
    void Start() {
        speed = Random.Range(lowerBound, upperBound);
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
        
        elapsedTime += Time.deltaTime;
        
        if (elapsedTime >= 20f)
        {
            elapsedTime = 0f;
            lowerBound += 0.25f;
            upperBound += 0.25f;
            speed = Random.Range(lowerBound, upperBound);
        }
    }
}
