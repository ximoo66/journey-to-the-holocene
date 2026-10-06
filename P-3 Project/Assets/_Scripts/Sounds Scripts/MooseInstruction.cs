using UnityEngine;
/// Author: Omid Ameri
/// Project: Journey to the Holocene (ER-P3)
public class MooseInstruction : MonoBehaviour
{
    
    [SerializeField] private AudioSource eagleAudioSource; // Reference to the eagle's audio source
    private bool hasPlayedLine = false; // Ensure the line plays only once

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayedLine)
        {
            hasPlayedLine = true; // Prevent replaying the line
            Debug.Log("Look to the clearing! There stands the majestic stag-moose, a relative of the modern moose.");
            SoundManager.Instance.PlayVoiceLineAtSource("Line36", eagleAudioSource);
        }
    }
}
