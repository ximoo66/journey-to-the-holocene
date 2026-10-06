using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to the UI Text element for the timer
    private float elapsedTime = 0f; // Variable to store elapsed time

    private void Update()
    {
        // Increment the elapsed time by the time since the last frame
        elapsedTime += Time.deltaTime;

        // Convert elapsed time to minutes and seconds
        int minutes = Mathf.FloorToInt(elapsedTime / 60); // Calculate minutes
        int seconds = Mathf.FloorToInt(elapsedTime % 60); // Calculate seconds

        // Update the timerText UI element
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
