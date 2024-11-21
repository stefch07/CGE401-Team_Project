using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float typewriterSpeed = 50f; // Allows the speed of the typewriter effect to be editable in the Inspector
    
    public bool isRunning { get; private set; }

    private readonly List<Punctuation> punctuations = new List<Punctuation>()
    {
        
        new Punctuation(new HashSet<char>() { '.', '!', '?' }, 0.6f),
        new Punctuation(new HashSet<char>() { ',', ';', ':' }, 0.3f),
    };

    private Coroutine typingCoroutine;
    
    public void Run(string textToType, TMP_Text textLabel) // Args: string we want to type, the label we want to type it on
    {
        typingCoroutine = StartCoroutine(TypeText(textToType, textLabel)); // Args: takes the text we want to type in + where we want it typed
    }

    public void Stop()
    {
        StopCoroutine(typingCoroutine);
        isRunning = false;
    }

    // Coroutine, responsible for drawing the text on-screen
    private IEnumerator TypeText(string textToType, TMP_Text textLabel) // Args: same as above Run()
    {
        isRunning = true;
        textLabel.text = string.Empty; // Removes "New Text"

        float t = 0; // Elapsed time since we've begun writing
        int charIndex = 0; // Floored value of t that will measure how many characters we want to type out on screen at the given frame

        while (charIndex < textToType.Length)
        {
            int lastCharIndex = charIndex;
            
            t += Time.deltaTime * typewriterSpeed; // Increment over time * speed
            charIndex = Mathf.FloorToInt(t); // Store floored value of timer t
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length); // ... shouldn't ever be longer than the text to type

            for (int i = lastCharIndex; i < charIndex; i++)
            {
                bool isLast = i >= textToType.Length - 1;
                
                textLabel.text = textToType.Substring(0, i + 1); // Writes text out

                if (IsPunctuation(textToType[i], out float waitTime) && !isLast && !IsPunctuation(textToType[i+1], out _))
                {
                    yield return new WaitForSeconds(waitTime);
                }
            }

            yield return null;
        }

        isRunning = false;
    }

    private bool IsPunctuation(char character, out float waitTime)
    {
        foreach (Punctuation punctuationCategory in punctuations)
        {
            if (punctuationCategory.Punctuations.Contains(character))
            {
                waitTime = punctuationCategory.WaitTime;
                return true;
            }
        }
        
        waitTime = default;
        return false;
    }

    private readonly struct Punctuation
    {
        public readonly HashSet<char> Punctuations;
        public readonly float WaitTime;

        public Punctuation(HashSet<char> punctuation, float waitTime)
        {
            Punctuations = punctuation;
            WaitTime = waitTime;
        }
    }
}
