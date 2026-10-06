using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit; 

public class PlayerRide : MonoBehaviour
{
    [SerializeField] private Transform playerStartPosition;

    [SerializeField] MastodonController currentAnimal; 
    [SerializeField] private BoxCollider mastodonCollider;
    [SerializeField] private GameObject moveProvider;
    [SerializeField] private GameObject teleport;
    [SerializeField] private GameObject snappingPoint; // While climbing, it didn't teleport to the snapping point exact enough, maybe you can find another way to fix it
                                                       //[SerializeField] private GameObject vignette;

    // y positions from the last 5 frames
    /*private float[] yPositions = new float[5];
    private int currentFrame = 0;
    private bool isFirstUpdate = true;*/

    void Start()
    {
        transform.position = playerStartPosition.position; // Force player to start at the correct position
        transform.rotation = playerStartPosition.rotation;
    }


    void Update()
    {
        //SmoothYPosition();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MastodonToRide")) // Detect climb trigger
        {
            snappingPoint = other.GetComponentInChildren<SitPointOn>().gameObject;
            Debug.Log("Player reached the climbing point.");
        }

        if (other.gameObject == snappingPoint && transform.position.y > snappingPoint.transform.position.y - 0.5f) // Only snap if high enough
        {
            Debug.Log("Player reached snap point, mounting now!");
            StartCoroutine(DelayedMount());
        }

        if (other.CompareTag("FinalArea")) // Dismount at final area
        {
            Debug.Log("Final area entered");
            DismountAnimal();
            mastodonCollider.enabled = false;
        }
    }

    IEnumerator DelayedMount()
    {
        yield return new WaitForSeconds(0.5f); // Small delay before snapping
        MountAnimal();
    }



    private void MountAnimal()
    {
        if (currentAnimal == null || snappingPoint == null)
        {
            Debug.Log("MountAnimal: Missing reference. Cannot mount.");
            return;
        }

        Debug.Log($"Snapping to: {snappingPoint.transform.position} | Player Pos: {transform.position}");

        // Parent the player to the snapping point for stable positioning
        transform.SetParent(snappingPoint.transform, worldPositionStays: false);

        // Move the player to the exact snapping position
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        currentAnimal.Mount();

        if (moveProvider != null) moveProvider.SetActive(false);
        if (teleport != null) teleport.SetActive(false);

        Debug.Log("Mounted mastodon");
    }


    IEnumerator FixSnapping()
    {
        yield return new WaitForFixedUpdate(); // Wait for physics update
        transform.position = snappingPoint.transform.position;
        transform.rotation = snappingPoint.transform.rotation;
    }


    private void DismountAnimal()
    {
        if (currentAnimal == null) return;

        currentAnimal.Dismount();
        transform.SetParent(null); 

        if (moveProvider != null)
        {
            moveProvider.SetActive(true);
            Debug.Log("MoveProvider reenabled");
        }

        if (teleport != null)
        {
            teleport.SetActive(true);
            Debug.Log("teleport reenabled");
        }

        Debug.Log("Dismounted mast");
        //vignette.SetActive(false);
    }
   

    /*private void SmoothYPosition()
    {
        // Store the current y position in the array, replacing the oldest value
        yPositions[currentFrame] = transform.position.y;
        currentFrame = (currentFrame + 1) % 5; // Cycle through frames (0 to 4)

        if (currentFrame == 0 && !isFirstUpdate) // Only after 5 frames
        {
            // Get the value from 5 frames ago (index 0 in the array)
            float previousYPosition = yPositions[0];
            
            // Calculate median (average) between current and 5 frames ago
            float smoothedY = (transform.position.y + previousYPosition) / 2f;

            // Apply the smoothed y position
            transform.position = new Vector3(transform.position.x, smoothedY, transform.position.z);
        }

        isFirstUpdate = false;
    }*/
}
