using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    private AudioSource backgroundMusicSource; // For background music
    public AudioSource soundEffectSource;     // For the sound effect

    void Start()
    {
        // Get the AudioSource for background music attached to the same GameObject
        backgroundMusicSource = GetComponent<AudioSource>();

        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.Play();
        }
        else
        {
            Debug.LogError("Background music AudioSource component missing!");
        }

        // Check if the sound effect source is assigned
        if (soundEffectSource == null)
        {
            Debug.LogError("Sound effect AudioSource not assigned in Inspector!");
        }
    }

    void Update()
    {
        // Play the sound effect when the Space key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (soundEffectSource != null)
            {
                soundEffectSource.Play();
            }
            else
            {
                Debug.LogError("Sound effect AudioSource is missing!");
            }
        }
    }
}