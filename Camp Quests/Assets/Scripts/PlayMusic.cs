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
          
        }

        // Check if the sound effect source is assigned
        if (soundEffectSource == null)
        {
            
        }
    }
}
