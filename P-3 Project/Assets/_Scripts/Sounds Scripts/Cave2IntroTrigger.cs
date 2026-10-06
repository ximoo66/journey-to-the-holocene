using System.Collections;
using UnityEngine;
/// Author: Omid Ameri
/// Project: Journey to the Holocene (ER-P3)
public class Cave2IntroTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed && other.CompareTag("Player"))
        {
            StartCoroutine(PlaySequentialVoiceLines());
            hasPlayed = true;


            SoundManager.Instance.PlayMusic("Cave Music v1");
        }
    }
    
    private IEnumerator PlaySequentialVoiceLines()
    {
        // Ensure the AudioSource is not null
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource is not assigned in ReturnToCave!");
            yield break;
        }

        // Voice line 39
        Debug.Log("You have returned, carrying their story.");
        SoundManager.Instance.PlayVoiceLineAtSource("Line39", audioSource);
        yield return new WaitForSeconds(6f);

        // Voice line 40
       // Debug.Log("Share what you have seen, and honor the mastodons' memory.");
        SoundManager.Instance.PlayVoiceLineAtSource("Line40", audioSource);
    }
}
