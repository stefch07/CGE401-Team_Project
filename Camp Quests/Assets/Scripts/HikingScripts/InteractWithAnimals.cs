using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractWithAnimals : MonoBehaviour
{
    [SerializeField] private string interactionMessage = "You pet the animal. It seems happy!";
    [SerializeField] private Text interactionText;
    private bool isPlayerInZone = false;
    private GameObject currentAnimal;

    void Start()
    {
        if (interactionText != null)
        {
            interactionText.text = "";
        }
    }

    void Update()
    {
        if (isPlayerInZone && Input.GetKeyDown(KeyCode.E))
        {
            InteractWithAnimal();
        }
    }

    private void InteractWithAnimal()
    {
        if (currentAnimal != null && interactionText != null)
        {
            interactionText.text = interactionMessage;
            StartCoroutine(ClearMessageAfterDelay(2f));
        }
    }

    private IEnumerator ClearMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (interactionText != null)
        {
            interactionText.text = "";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Animals"))
        {
            isPlayerInZone = true;
            currentAnimal = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Animals"))
        {
            isPlayerInZone = false;
            currentAnimal = null;
            if (interactionText != null)
            {
                interactionText.text = "";
            }
        }
    }
}
