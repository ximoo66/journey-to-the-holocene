using UnityEngine;
using TMPro;
using System.Collections;

/// Author: Omid Ameri
/// Project: Journey to the Holocene (ER-P3)
public class ClimbingInstructions : MonoBehaviour
{
    [SerializeField] private GameObject climbingText; // Reference to the TextMeshPro object
    [SerializeField] private AudioSource eagleAudioSource; // Reference to the eagle's audio source
    private bool hasPlayedLines = false; // Ensure the lines play only once

    private static Coroutine _currentVoiceCoroutine; // Shared among all instances to stop previous coroutines

    private void Update()
    {
        // Check if the TextMeshPro object becomes active and the lines haven't already been played
        if (climbingText.gameObject.activeInHierarchy && !hasPlayedLines)
        {
            hasPlayedLines = true; // Prevent replaying the lines

            // Stop any previously running coroutine
            if (_currentVoiceCoroutine != null)
            {
                StopCoroutine(_currentVoiceCoroutine);
            }

            // Start the new coroutine and track it
            _currentVoiceCoroutine = StartCoroutine(PlayClimbingInstructions());
        }
    }

    private IEnumerator PlayClimbingInstructions()
    {
        // Play Line 28
        Debug.Log("Now, they will allow you to ride with them.");
        SoundManager.Instance.PlayVoiceLineAtSource("Line28", eagleAudioSource);
        yield return new WaitForSeconds(5f);

        // Play Line 29
        Debug.Log("Approach the lead mastodon and climb onto its back.");
        SoundManager.Instance.PlayVoiceLineAtSource("Line29", eagleAudioSource);
        yield return new WaitForSeconds(6f);

        // Play Line 30
        Debug.Log("Use your hands to grab and pull yourself up.");
        SoundManager.Instance.PlayVoiceLineAtSource("Line30", eagleAudioSource);
        yield return new WaitForSeconds(5f);

        // Play Line 31
        Debug.Log("Once mounted, you will guide the herd.");
        SoundManager.Instance.PlayVoiceLineAtSource("Line31", eagleAudioSource);
    }
}
