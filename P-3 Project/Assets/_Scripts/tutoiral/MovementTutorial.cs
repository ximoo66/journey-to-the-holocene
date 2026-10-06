using System.Collections;
using UnityEngine;
/// Author: Omid Ameri
/// Project: Journey to the Holocene (ER-P3)
public class MovementTutorial : MonoBehaviour
{
    [SerializeField] private GameObject teleportTutorial; // Reference to the teleport tutorial GameObject
    private bool hasPlayedInstruction = false;

    private void Start()
    {
        // Play the welcome voice line at the beginning
        SoundManager.Instance.PlayVoiceLine("Welcome_Tutorial");
        Debug.Log("Voice: Welcome to the training grounds. Follow my guidance to prepare for your journey.");

        // Delay the movement instructions slightly to avoid overlap 
        Invoke(nameof(PlayMovementInstructions), 4f); //  the welcome voice is 4 seconds long
    }

    private void PlayMovementInstructions()
    {
        if (!hasPlayedInstruction)
        {
            SoundManager.Instance.PlayVoiceLine("Movement_Tutorial");
            Debug.Log("Instruction: Move to the green area by using the joystick on your VR controller.");
            hasPlayedInstruction = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player reached the waypoint
        {
            Debug.Log("Movement tutorial completed.");
            teleportTutorial.SetActive(true); // Activate the teleport tutorial
            gameObject.SetActive(false); // Disable this waypoint

            // Play the movement complete audio
            SoundManager.Instance.PlayVoiceLine("Teleport_Tutorial");
            Debug.Log("Voice: Well done! Now teleport to the next green area.");
        }
    }
}
