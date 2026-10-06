using UnityEngine;
using System.Collections;
/// Author: Omid Ameri
/// Project: Journey to the Holocene (ER-P3)
public class ClimaxEndInstruction : MonoBehaviour
{
    [SerializeField] private AudioSource eagleAudioSource; // Reference to the eagle's audio source
    private bool hasPlayedLines = false; // Ensure the lines play only once

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayedLines)
        {
            hasPlayedLines = true; // Prevent replaying the lines
            StartCoroutine(PlayClimaxEndInstructions());
        }
    }

    private IEnumerator PlayClimaxEndInstructions()
    {
        Debug.Log("You have done well, traveler. The herd is safe, and their story lives on.");
        SoundManager.Instance.PlayVoiceLineAtSource("Line37", eagleAudioSource);
        yield return new WaitForSeconds(9f); // Adjust duration for Line37
        
        Debug.Log("But remember, their world is fragile, as is ours. Protect what remains");
        SoundManager.Instance.PlayVoiceLineAtSource("Line38", eagleAudioSource);
        
        
        SceneSwapper.Instance.SwapScene("Cave_2");
    }
}
