using UnityEngine;
/// Author: Omid Ameri
/// Project: Journey to the Holocene (ER-P3)
public class MastodonLandIntro : MonoBehaviour
{
    [SerializeField] private GameObject eagle; // Reference to the eagle (shaman), serialized for Unity Inspector
    private bool introPlayed = false; // Prevent re-triggering
    private AudioSource eagleAudioSource; // AudioSource attached to the eagle

    private void Start()
    {
        // Ensure the eagle has an AudioSource
        if (eagle != null)
        {
            eagleAudioSource = eagle.GetComponent<AudioSource>();
            if (eagleAudioSource == null)
            {
                eagleAudioSource = eagle.AddComponent<AudioSource>();
            }
        }
        else
        {
            Debug.LogError("Eagle object not assigned.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (introPlayed) return;

        if (other.CompareTag("Player"))
        {
            introPlayed = true;
            eagle.GetComponent<Eagle>().StartFlying();
            // Start the coroutine to play lines sequentially
            StartCoroutine(PlayIntroLines());
        }
    }

    private System.Collections.IEnumerator PlayIntroLines()
    {
        // Play Line 15
        Debug.Log("Behold, traveler. This is the land of the mastodons.");
        SoundManager.Instance.PlayVoiceLineAtSource("Line15", eagleAudioSource);
        yield return new WaitForSeconds(7.0f); // Delay for 7 seconds

        // Play Line 16
        Debug.Log("Their steps echo through time, and their hearts are pure.");
        SoundManager.Instance.PlayVoiceLineAtSource("Line16", eagleAudioSource);
        yield return new WaitForSeconds(6.0f); // Delay for 6 seconds

        // Play Line 17
        Debug.Log("Follow their trail to find them.");
        SoundManager.Instance.PlayVoiceLineAtSource("Line17", eagleAudioSource);
        yield return new WaitForSeconds(4.0f); // Delay for 4 seconds

        // Play Line 18
        Debug.Log("Along the way, the land will whisper their stories. Pay attention, for these tales are precious.");
        SoundManager.Instance.PlayVoiceLineAtSource("Line18", eagleAudioSource);
        yield return new WaitForSeconds(11.0f); // Delay for 11 seconds
        
        eagle.GetComponent<Eagle>().StartFlying();
        
    }
}
