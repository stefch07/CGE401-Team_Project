using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InteractWithAnimals : MonoBehaviour
{
    [SerializeField] private string interactionMessage = "You pet the animal. It seems happy!";
    [SerializeField] private Text interactionText;
    [SerializeField] private Slider satisfactionSlider;  // Satisfaction bar slider
    private bool isPlayerInZone = false;
    private GameObject currentAnimal;
    private float satisfaction = 0f;  // Track satisfaction progress
    private float maxSatisfaction = 13f;  // Total satisfaction (13 = 100%)
    private float satisfactionPerInteraction;

    void Start()
    {
        satisfactionPerInteraction = 100f / maxSatisfaction;  // Calculate how much each interaction increases satisfaction
        if (interactionText != null)
        {
            interactionText.text = "";
        }

        if (satisfactionSlider != null)
        {
            satisfactionSlider.maxValue = 100f;  // Satisfaction slider max value is 100%
            satisfactionSlider.value = 0f;  // Start with 0 satisfaction
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
            satisfaction += satisfactionPerInteraction;  // Increase satisfaction
            satisfaction = Mathf.Min(satisfaction, 100f);  // Ensure satisfaction doesn't exceed 100%
            satisfactionSlider.value = satisfaction;  // Update the slider value
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
