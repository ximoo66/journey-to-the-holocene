using UnityEngine;
using System.Collections; // Required for coroutines
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

/// Author: Omid Ameri
/// Project: Journey to the Holocene (ER-P3)
public class MastodonTrustMechanic : MonoBehaviour
{
    private int leafCount = 0;
    private bool trustGained = false;

    [SerializeField] private Collider feedingArea; // Trigger area where leaves are thrown
    [SerializeField] private GameObject mastodon; // Reference to the mastodon object
    [SerializeField] private Animator mastodonAnimator; // Animator for mastodon eating animation
    [SerializeField] private GameObject climbText; // Reference to the TextMeshPro component for "Climb Here"
    [SerializeField] private Transform leafAttachmentPoint; // Transform for leaf attachment (e.g., on the mastodon's horn)
    [SerializeField] private AudioSource eagleAudioSource; // Reference to the eagle's audio source
    [SerializeField] private GameObject circleArea1; // First circular area
    [SerializeField] private GameObject circleArea2; // Second circular area

    private ClimbInteractable climbInteractable;
    private static Coroutine _currentVoiceCoroutine; // Shared among all instances to stop previous coroutines

    private void Start()
    {
        // Ensure climb text and circle areas are initially set correctly
        if (climbText != null)
        {
            climbText.gameObject.SetActive(false);
        }

        if (mastodon != null)
        {
            climbInteractable = mastodon.GetComponent<ClimbInteractable>();
            if (climbInteractable == null)
            {
                Debug.LogError("Mastodon does not have a ClimbInteractable component.");
            }
        }
        else
        {
            Debug.LogError("Mastodon object not assigned.");
        }

        if (circleArea1 != null) circleArea1.SetActive(true);  // Ensure the first area is active
        if (circleArea2 != null) circleArea2.SetActive(false); // Ensure the second area is inactive
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the feeding area is a leaf
        if (other.CompareTag("Leaf"))
        {
            HandleLeafFed(other.gameObject);
        }
    }

    private void HandleLeafFed(GameObject leaf)
    {
        leafCount++;
        Debug.Log($"Leaf count: {leafCount}/3");

        // Attach the leaf to the mastodon
        AttachLeafToMastodon(leaf);

        // Play the eating animation
        if (mastodonAnimator != null)
        {
            mastodonAnimator.SetTrigger("Eat");
        }

        // Play eating sound (one-shot)
        SoundManager.Instance.PlayInteractionSound("EatingSound");

        // Stop any previous voice coroutine
        if (_currentVoiceCoroutine != null)
        {
            StopCoroutine(_currentVoiceCoroutine);
        }

        // Start the new coroutine and track it
        _currentVoiceCoroutine = StartCoroutine(PlayVoiceLines());
    }

    private IEnumerator PlayVoiceLines()
    {
        if (leafCount == 1)
        {
            Debug.Log("Good start, but they will need more. Two leaves remain.");
            SoundManager.Instance.PlayVoiceLineAtSource("Line25", eagleAudioSource);
            yield return new WaitForSeconds(6.0f);
        }
        else if (leafCount == 2)
        {
            Debug.Log("You're earning their trust. One more leaf should do it.");
            SoundManager.Instance.PlayVoiceLineAtSource("Line26", eagleAudioSource);
            yield return new WaitForSeconds(6.0f);
        }
        else if (leafCount == 3)
        {
            Debug.Log("Well done, traveler. You have gained their trust.");
            SoundManager.Instance.PlayVoiceLineAtSource("Line27", eagleAudioSource);
            yield return new WaitForSeconds(6.0f);

            SceneSwapper.Instance.InvokeDisable();

            // Deactivate circleArea1 and activate circleArea2
            if (circleArea1 != null) circleArea1.SetActive(false);
            if (circleArea2 != null) circleArea2.SetActive(true);

            // Start Line28 after Line27 finishes
            _currentVoiceCoroutine = StartCoroutine(PlayLine28AfterDelay());
        }
    }

    private IEnumerator PlayLine28AfterDelay()
    {
        // Play Line28
        SoundManager.Instance.PlayVoiceLineAtSource("Line28", eagleAudioSource);
        yield return new WaitForSeconds(6.0f);

        // Enable climbing on the mastodon
        EnableClimbing();
    }

    private void AttachLeafToMastodon(GameObject leaf)
    {
        // Disable physics on the leaf
        Rigidbody leafRigidbody = leaf.GetComponent<Rigidbody>();
        if (leafRigidbody != null)
        {
            leafRigidbody.isKinematic = true; // Stop the leaf from moving
            leafRigidbody.detectCollisions = false; // Disable collisions
        }

        // Attach the leaf to the mastodon's horn
        leaf.transform.SetParent(leafAttachmentPoint);
        leaf.transform.localPosition = Vector3.zero; // Position it at the attachment point
        leaf.transform.localRotation = Quaternion.identity;

        // Destroy the leaf after the eating animation ends
        StartCoroutine(DestroyLeafAfterEating(leaf));
    }

    private IEnumerator DestroyLeafAfterEating(GameObject leaf)
    {
        // Wait for the eating animation to finish
        yield return new WaitForSeconds(3.0f); // Adjust this duration to match the animation

        // Destroy the leaf
        Destroy(leaf);
    }

    private void EnableClimbing()
    {
        if (!trustGained)
        {
            trustGained = true;

            Debug.Log("Now, they will allow you to ride with them.");

            // Show the "Climb Here" text
            if (climbText != null)
            {
                climbText.gameObject.SetActive(true);
            }
        }
        if (climbInteractable != null)
        {
            climbInteractable.enabled = true; // Enable the ClimbInteractable component, making the mastodon climbable
            Debug.Log("Climbing mechanic activated on the mastodon.");
        }
    }
}
