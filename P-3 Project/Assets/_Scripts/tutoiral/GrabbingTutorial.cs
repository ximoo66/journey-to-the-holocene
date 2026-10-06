using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
/// Author: Omid Ameri
/// Project: Journey to the Holocene (ER-P3)
public class GrabbingTutorial : MonoBehaviour
{
    [SerializeField] private GameObject climbingTutorial; // Reference to the climbing tutorial GameObject
    [SerializeField] private GameObject greenCircle; // Reference to the green circle GameObject
    private bool itemGrabbed = false;

    private void Update()
    {
        if (itemGrabbed) return;

        // Check if the item is grabbed
        if (GetComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>().isSelected)
        {
            Debug.Log("Grabbing tutorial completed.");
            itemGrabbed = true;

            climbingTutorial.SetActive(true); // Activate the climbing tutorial

            if (greenCircle != null)
            {
                greenCircle.SetActive(false); // Deactivate the green circle
            }
            else
            {
                Debug.LogWarning("Green circle reference is missing!");
            }

            // Play climbing instruction
            SoundManager.Instance.PlayVoiceLine("Climb_Tutorial");
            Debug.Log("Voice: Well done. Now climb the glowing ladder using the [Grip Button] with your middle finger.");
        }
    }
}
