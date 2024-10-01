using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowProjectiles : MonoBehaviour
{
    // References
    public Transform playerCamera;
    public Transform attackPoint;
    public GameObject objectToThrow;
    public DialogManager dialogManager; // Dialog manager reference

    // Settings
    public int totalThrows;
    public float throwCooldown;

    // Throwing
    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow;

    private void Start()
    {
        readyToThrow = true;
    }

    private void Update()
    {
        // Disables ability for player to throw things and enables mouse movement/visibility if dialogPanel is active
        if (dialogManager.dialogPanel.activeSelf)
        {
            readyToThrow = false; // Player can't throw projectiles if a dialogue boxis open
        }
        else
        {
            readyToThrow = true;
        }

        if (Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0)
        {
            Throw();
        }
    }

    private void Throw()
    {
        readyToThrow = false;

        // Instantiate object to throw
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, playerCamera.rotation);

        // Get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // Calculate direction to center of screen
        Vector3 forceDirection = playerCamera.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        // Add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        // Implement throwCooldown
        Invoke(nameof(ResetThrow), throwCooldown);

        // Attach the ProjectileCollisions script to throwing projectiles
        projectile.AddComponent<ProjectileCollisions>();

    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }

}