using UnityEngine;

/// Author: Omid Ameri
/// Project: Journey to the Holocene (ER-P3)
public class MastodonEducation : MonoBehaviour
{
    [SerializeField] private GameObject eagle; // Reference to the eagle (shaman)
    [SerializeField] private Collider educationalTrigger; // Single area that triggers educational lines
    private AudioSource eagleAudioSource; // AudioSource attached to the eagle

    private static Coroutine _currentVoiceCoroutine; // Shared among all instances to stop previous coroutines

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
        if (other.CompareTag("Player"))
        {
            // Stop any previously running coroutine
            if (_currentVoiceCoroutine != null)
            {
                StopCoroutine(_currentVoiceCoroutine);
            }

            // Start the new coroutine and track it
            _currentVoiceCoroutine = StartCoroutine(PlayEducationalLines());
        }
    }

    private System.Collections.IEnumerator PlayEducationalLines()
    {
        // Play Line 19
        Debug.Log("Mastodons roamed these lands in herds, much like elephants today.");
        SoundManager.Instance.PlayVoiceLineAtSource("Line19", eagleAudioSource);
        yield return new WaitForSeconds(10.0f);

        // Play Line 20
        Debug.Log("Their tusks grew over a lifetime, holding the secrets of their struggles and triumphs.");
        SoundManager.Instance.PlayVoiceLineAtSource("Line20", eagleAudioSource);
        yield return new WaitForSeconds(10.0f);

        // Play Line 21
        Debug.Log("The changing climate and human hunters led to their extinction, ending their story far too soon.");
        SoundManager.Instance.PlayVoiceLineAtSource("Line21", eagleAudioSource);
        yield return new WaitForSeconds(11.0f);
    }
}
