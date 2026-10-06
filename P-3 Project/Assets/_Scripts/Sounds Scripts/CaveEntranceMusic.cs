using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management
/// Author: Omid Ameri
/// Project: Journey to the Holocene (ER-P3)
public class CaveEntranceMusic : MonoBehaviour
{
    private bool hasPlayed = false; // To ensure the sound plays only once

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed) // Check if the player enters the trigger
        {
            hasPlayed = true; // Prevent replaying the sound
            SoundManager.Instance.SetVolume("music", 2f); // Set volume for music
            SoundManager.Instance.PlayMusic("Cave Music v1"); // Play the cave music
            Debug.Log("Player entered the cave. Cave music playing.");
        }
    }

    // Stop the music when the scene changes
    private void OnDisable()
    {
        // Stops the currently playing music if the scene is about to change or object is disabled
        SoundManager.Instance.StopMusic();
    }
}
