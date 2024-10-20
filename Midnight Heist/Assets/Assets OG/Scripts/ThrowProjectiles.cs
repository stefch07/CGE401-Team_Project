using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThrowProjectiles : MonoBehaviour
{
    // References
    public Transform playerCamera;
    public Transform attackPoint;
    public GameObject objectToThrow;
    public DialogManager dialogManager; // Dialog manager reference
    public GameObject gameManager;
    private PauseMenu pauseMenu;

    // Settings
    public int totalThrows;
    public float throwCooldown;

    // Throwing
    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float throwUpwardForce;

    // Distraction related
    public float distractionRange = 10f; // Range of the distraction sphere trigger

    bool readyToThrow;
    
    public TMP_Text textbox;

    private void Start()
    {
        readyToThrow = true;

        // Get reference to pauseMenu from gameManager
        if (gameManager != null)
        {
            pauseMenu = gameManager.GetComponent<PauseMenu>();
        }

        // Initialize totalThrows if necessary
        totalThrows = 0; // Start with zero rocks
    }

    private void Update()
    {
        // Disables ability for player to throw things and enables mouse movement/visibility if dialogPanel is active
        if (dialogManager.dialogPanel.activeSelf || PauseMenu.isPaused)
        {
            readyToThrow = false; // Player can't throw projectiles if a dialogue box or pause menu is open
        }
        else
        {
            readyToThrow = true;
        }

        if (Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0)
        {
            Throw();
        }
        
        textbox.text = "× " + totalThrows;
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

        // Set distractionRange for the projectile's collisions
        ProjectileCollisions projectileCollisions = projectile.GetComponent<ProjectileCollisions>();
        if (projectileCollisions != null)
        {
            projectileCollisions.distractionRange = distractionRange;
        }

        // Implement throwCooldown
        Invoke(nameof(ResetThrow), throwCooldown);

    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }

    public void AddRocks(int amount)
    {
        totalThrows += amount;
        Debug.Log("Current rock count: " + totalThrows);
    }

}
