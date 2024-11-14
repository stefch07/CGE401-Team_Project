using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float typewriterSpeed = 50f; // Allows the speed of the typewriter effect to be editable in the Inspector

    // Driver method, runs the coroutine
    public Coroutine Run(string textToType, TMP_Text textLabel) // Args: string we want to type, the label we want to type it on
    {
        return StartCoroutine(TypeText(textToType, textLabel)); // Args: takes the text we want to type in + where we want it typed
    }

    // Coroutine, responsible for drawing the text on-screen
    private IEnumerator TypeText(string textToType, TMP_Text textLabel) // Args: same as above Run()
    {
        textLabel.text = string.Empty; // Removes "New Text"

        float t = 0; // Elapsed time since we've begun writing
        int charIndex = 0; // Floored value of t that will measure how many characters we want to type out on screen at the given frame

        while (charIndex < textToType.Length)
        {
            t += Time.deltaTime * typewriterSpeed; // Increment over time * speed
            charIndex = Mathf.FloorToInt(t); // Store floored value of timer t
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length); // ... shouldn't ever be longer than the text to type

            textLabel.text = textToType.Substring(0, charIndex); // Writes text out

            yield return null;
        }

        textLabel.text = textToType;
    }
}
