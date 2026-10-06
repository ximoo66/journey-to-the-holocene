using UnityEngine;

/// Author: Omid Ameri
/// Project: Journey to the Holocene (ER-P3)
public class TrustInstructionArea : MonoBehaviour
{
    [SerializeField] private AudioSource eagleAudioSource; // Reference to the eagle's AudioSource
    private bool instructionPlayed = false; // Ensure instructions play only once

    private static Coroutine _currentVoiceCoroutine; // Shared among all instances to stop previous coroutines

    private void OnTriggerEnter(Collider other)
    {
        if (!instructionPlayed && other.CompareTag("Player"))
        {
            instructionPlayed = true;

            // Stop any previously running coroutine
            if (_currentVoiceCoroutine != null)
            {
                StopCoroutine(_currentVoiceCoroutine);
            }

            // Start the new coroutine and track it
            _currentVoiceCoroutine = StartCoroutine(PlayInstructions());
        }
    }

    private System.Collections.IEnumerator PlayInstructions()
    {
        // Play Line 22
        Debug.Log("Traveler, to walk among them, you must gain their trust.");
        SoundManager.Instance.PlayVoiceLineAtSource("Line22", eagleAudioSource);
        yield return new WaitForSeconds(8.0f);

        // Play Line 23
        SceneSwapper.Instance.InvokeEnable();
        Debug.Log("Look around for leaves growing nearby.");
        SoundManager.Instance.PlayVoiceLineAtSource("Line23", eagleAudioSource);
        yield return new WaitForSeconds(4.0f);

        // Play Line 24
        Debug.Log("Gather three leaves and throw them into the feeding area. Only then will they accept you.");
        SoundManager.Instance.PlayVoiceLineAtSource("Line24", eagleAudioSource);
        yield return new WaitForSeconds(10.0f);
    }
}
