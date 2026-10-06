using UnityEngine;
/// Author: Omid Ameri
/// Project: Journey to the Holocene (ER-P3)
public class TeleportTutorial : MonoBehaviour
{
    [SerializeField] private GameObject grabbingTutorial; // Reference to the grabbing tutorial GameObject

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player teleported to the waypoint
        {
            Debug.Log("Teleport tutorial completed.");
            grabbingTutorial.SetActive(true); // Activate the grabbing tutorial
            gameObject.SetActive(false); // Disable this waypoint

            // Play grabbing instruction
            SoundManager.Instance.PlayVoiceLine("Grab_Tutorial");
            Debug.Log("Voice: Well done. Now grab the glowing herb using the [Grip Button] with your middle finger.");
        }
    }
}
