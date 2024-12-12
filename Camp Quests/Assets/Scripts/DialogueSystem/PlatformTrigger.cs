using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformTrigger : MonoBehaviour
{
    public Text platformM;
    public Text platformK;
    public Text platformH;
    public Text platformCS;

    private bool canDisplayM = false;
    private bool canDisplayK = false;
    private bool canDisplayH = false;
    private bool canDisplayCS = false;

    void Update()
    {
        // Check for input and display text if conditions are met
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canDisplayM && platformM != null)
            {
                platformM.gameObject.SetActive(true);
            }
            if (canDisplayK && platformK != null)
            {
                platformK.gameObject.SetActive(true);
            }
            if (canDisplayH && platformH != null)
            {
                platformH.gameObject.SetActive(true);
            }
            if (canDisplayCS && platformCS != null)
            {
                platformCS.gameObject.SetActive(true);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Ensure all UI elements are assigned
        if (platformM == null || platformK == null || platformH == null || platformCS == null)
        {
            Debug.LogError("One or more Text elements are not assigned in the Inspector!");
            return;
        }

        // Reset all text visibility conditions
        canDisplayM = false;
        canDisplayK = false;
        canDisplayH = false;
        canDisplayCS = false;

        // Check the tag of the collided platform and set the appropriate condition
        switch (collision.gameObject.tag)
        {
            case "PlatformM":
                canDisplayM = true;
                platformM.gameObject.SetActive(false);
                platformM.text = "Are you ready to go Meditation?";
                break;

            case "PlatformK":
                canDisplayK = true;
                platformK.gameObject.SetActive(false);
                platformK.text = "Are you ready to go Kayaking?";
                break;

            case "PlatformH":
                canDisplayH = true;
                platformH.gameObject.SetActive(false);
                platformH.text = "Are you ready to go Hiking?";
                break;

            case "PlatformCS":
                canDisplayCS = true;
                platformCS.gameObject.SetActive(false);
                platformCS.text = "Are you ready for Camp Stories?";
                break;

            default:
                Debug.Log("Unknown platform collision.");
                break;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Hide all texts when the player exits a platform and reset conditions
        if (platformM != null) platformM.gameObject.SetActive(false);
        if (platformK != null) platformK.gameObject.SetActive(false);
        if (platformH != null) platformH.gameObject.SetActive(false);
        if (platformCS != null) platformCS.gameObject.SetActive(false);

        canDisplayM = false;
        canDisplayK = false;
        canDisplayH = false;
        canDisplayCS = false;
    }
}
