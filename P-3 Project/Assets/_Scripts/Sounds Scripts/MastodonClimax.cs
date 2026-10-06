using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Omid Ameri
/// Project: Journey to the Holocene (ER-P3)
/// Handles the climax music for the Mastodon scene, fading in and out based on player presence.
/// </summary>
public class MastodonClimax : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1.5f; // Duration of fade-in and fade-out
    [SerializeField] private float volume = 0.8f; // Desired volume level for music
    private bool isPlaying = false; // To track if music is currently playing

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlaying)
        {
            isPlaying = true;

            // Set volume before playing music
            SoundManager.Instance.SetVolume("music", volume);
            SoundManager.Instance.PlayMusic("climax.masto", fadeDuration);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isPlaying)
        {
            isPlaying = false;
            SoundManager.Instance.StopMusic(fadeDuration);
        }
    }
}
