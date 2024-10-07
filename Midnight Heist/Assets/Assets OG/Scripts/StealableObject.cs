using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealableObject : MonoBehaviour
{
    // The gold value of the item, based on its name
    // Change this to public or use SerializeField for Inspector visibility
    [SerializeField]
    private int goldValue;  // Make this variable visible in the Inspector

    private void Start()
    {
        // Ensure a spherical collider is present for interaction
        SphereCollider collider = gameObject.AddComponent<SphereCollider>();
        collider.isTrigger = true; // Make it a trigger for interactions
        collider.radius = 1.0f; // Adjust the radius as needed
    }

    public int GetGoldValue()
    {
        return goldValue;  // Return the item's gold value
    }
}
