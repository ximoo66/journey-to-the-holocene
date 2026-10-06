using UnityEngine;
using TMPro; // Import TextMeshPro namespace
using System.Collections;

/// <summary>
/// Author: Omid Ameri
/// Project: Journey to the Holocene (ER-P3)
/// </summary>
public class HunterInstruction : MonoBehaviour
{
    [SerializeField] private AudioSource eagleAudioSource; // Reference to the eagle's audio source
    [SerializeField] private GameObject instructionText; // Reference to the TextMeshPro object
    private bool hasPlayedLines = false; // Ensure the lines play only once

    private static Coroutine _currentInstructionCoroutine; // Shared coroutine reference

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayedLines)
        {
            hasPlayedLines = true; // Prevent replaying the lines
            instructionText.SetActive(true); // Activate the TextMeshPro object

            // Stop any previously running instruction coroutine
            if (_currentInstructionCoroutine != null)
            {
                StopCoroutine(_currentInstructionCoroutine);
            }

            // Start the new coroutine and track it
            _currentInstructionCoroutine = StartCoroutine(PlayHunterInstructions());

            SoundManager.Instance.PlayInteractionSound("HunterSound");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            instructionText.SetActive(false); // Deactivate the TextMeshPro object
        }
    }

    private IEnumerator PlayHunterInstructions()
    {
        Debug.Log("Hunters approach! Lead the herd to safety.");
        SoundManager.Instance.PlayVoiceLineAtSource("Line32", eagleAudioSource);
        yield return new WaitForSeconds(5f); // duration for Line32

        Debug.Log("Destroy obstacles by pressing [X] with your thumb finger");
        SoundManager.Instance.PlayVoiceLineAtSource("Line33", eagleAudioSource);
        yield return new WaitForSeconds(7f); // duration for Line33
    }
}
