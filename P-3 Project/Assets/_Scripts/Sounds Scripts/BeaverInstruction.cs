using UnityEngine;
/// Author: Omid Ameri
/// Project: Journey to the Holocene (ER-P3)
public class BeaverInstruction : MonoBehaviour
{
   
    [SerializeField] private AudioSource eagleAudioSource; // Reference to the eagle's audio source
    private bool hasPlayedLine = false; // Ensure the line plays only once

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayedLine)
        {
            hasPlayedLine = true; // Prevent replaying the line
            Debug.Log("Look there! By the water, a beaver. Beavers were among the creatures that thrived in this era");
            SoundManager.Instance.PlayVoiceLineAtSource("Line35", eagleAudioSource);
        }
    }
}
