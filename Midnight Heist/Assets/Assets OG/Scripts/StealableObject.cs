using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealableObject : MonoBehaviour
{
    // The gold value of the item, based on its name
    [SerializeField]
    private int goldValue;  // Existing gold value for regular items
    [SerializeField]
    private int rockCount;  // Number of rocks to be collected if the object is a Rock_Bag

    // Serialized field to adjust the collider radius in the Inspector
    [SerializeField]
    private SphereCollider sphereCollider;

    private void Start()
    {
        // Ensure a sphereical collider is present and configure it from Inspector
        if (sphereCollider == null)
        {
            sphereCollider = gameObject.AddComponent<SphereCollider>();
            sphereCollider.isTrigger = true;
        }

        // Ensure a spherical collider is present for interaction
        sphereCollider.radius = sphereCollider.radius;
    }

    public int GetGoldValue()
    {
        return goldValue;  // Return the item's gold value
    }

    public int GetRockCount()
    {
        return rockCount; // Return the number of rocks if this is a Rock_Bag
    }
}
