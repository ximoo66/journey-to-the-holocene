using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
/// Author: Omid Ameri
/// Project: Journey to the Holocene (ER-P3)
public class ShamanIntro : MonoBehaviour
{
    [SerializeField] private Collider storyArea; // The area where the player triggers the story
    [SerializeField] private AudioSource shamanAudioSource; // Reference to the shaman's audio source

    private bool hasStartedStory = false; // To ensure the story starts only once

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the story area and ensure it starts only once
        if (other.CompareTag("Player") && !hasStartedStory)
        {
            hasStartedStory = true; // Mark the story as started
            StartCoroutine(PlayStoryVoiceLines());
        }
    }

    private IEnumerator PlayStoryVoiceLines()
    {
     
        Debug.Log("Welcome, traveler. Come closer to the fire.");
        SoundManager.Instance.PlayVoiceLineAtSource("Line1", shamanAudioSource);
        yield return new WaitForSeconds(6f); // Wait for the duration of the first line 

        Debug.Log("I am the keeper of ancient stories, the bridge to the time of giants.");
        SoundManager.Instance.PlayVoiceLineAtSource("Line2", shamanAudioSource);
        yield return new WaitForSeconds(8f); // Wait for the duration of the second line

        Debug.Log("Before us walked mighty beasts—the mastodons—gentle guardians of a world long lost.");
        SoundManager.Instance.PlayVoiceLineAtSource("Line3", shamanAudioSource);
        yield return new WaitForSeconds(11f); // Wait for the duration of the third line

        Debug.Log("Today, I will guide you to their time, to witness their world as it once was.");
        SoundManager.Instance.PlayVoiceLineAtSource("Line4", shamanAudioSource);
        yield return new WaitForSeconds(8f); // Wait for the duration of the fourth line


        Debug.Log("But the path is not open yet. You must gather the sacred ingredients to prepare the way.");
        SoundManager.Instance.PlayVoiceLineAtSource("Line5", shamanAudioSource);
        yield return new WaitForSeconds(9f); // Wait for the duration of the fifth line

        Debug.Log("Search the cave for three glowing herbs and bring them to the pot.");
        SoundManager.Instance.PlayVoiceLineAtSource("Line6", shamanAudioSource);
    }
}
