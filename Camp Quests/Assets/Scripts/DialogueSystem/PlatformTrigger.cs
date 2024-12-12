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

    void OnCollisionEnter(Collision collision)
    {
        if (platformM == null || platformK == null || platformH == null || platformCS == null)
        {
            Debug.LogError("One or more Text elements are not assigned in the Inspector!");
            return;
        }

        platformM.gameObject.SetActive(false);
        platformK.gameObject.SetActive(false);
        platformH.gameObject.SetActive(false);
        platformCS.gameObject.SetActive(false);

        switch (collision.gameObject.tag)
        {
            case "PlatformM":
                platformM.text = "Are you ready to go Meditation?";
                platformM.gameObject.SetActive(true);
                break;

            case "PlatformK":
                platformK.text = "Are you ready to go Kayaking?";
                platformK.gameObject.SetActive(true);
                break;

            case "PlatformH":
                platformH.text = "Are you ready to go Hiking?";
                platformH.gameObject.SetActive(true);
                break;

            case "PlatformCS":
                platformCS.text = "Are you ready for Camp Stories?";
                platformCS.gameObject.SetActive(true);
                break;

            default:
                Debug.Log("Unknown platform collision.");
                break;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (platformM != null) platformM.gameObject.SetActive(false);
        if (platformK != null) platformK.gameObject.SetActive(false);
        if (platformH != null) platformH.gameObject.SetActive(false);
        if (platformCS != null) platformCS.gameObject.SetActive(false);
    }
}
