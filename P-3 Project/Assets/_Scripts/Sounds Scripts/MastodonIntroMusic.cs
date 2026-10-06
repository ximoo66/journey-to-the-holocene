using UnityEngine;
/// Author: Omid Ameri
/// Project: Journey to the Holocene (ER-P3)
public class MastodonIntroMusic : MonoBehaviour
{
    
    [SerializeField] private float fadeDuration = 1.5f; // Duration of fade-in and fade-out
    private bool isPlaying = false; // To track if music is currently playing

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlaying)
        {
            isPlaying = true;
            SoundManager.Instance.PlayMusic("The Mastodon Awakening", fadeDuration);
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
