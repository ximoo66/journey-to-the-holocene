using System.Collections;
using UnityEngine;
/// Author: Omid Ameri
/// Project: Journey to the Holocene (ER-P3)
public class ClimbingTutorial : MonoBehaviour
{
    [SerializeField] private GameObject caveEntrance; // Reference to the cave entrance object

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player reached the top of the ladder
        {
            Debug.Log("Climbing tutorial completed.");

            // Disable the cave entrance to open the path
            if (caveEntrance != null)
            {
                caveEntrance.SetActive(false);
                //StartCoroutine(OpenSesame());
                Debug.Log("Cave entrance is now open.");
            }
            else
            {
                Debug.LogWarning("Cave entrance object is not assigned in the inspector.");
            }

            

            // Play the final voice line
            SoundManager.Instance.PlayVoiceLine("Finish_Tutorial");
            //Debug.Log("Voice: Well done. You’re ready for your journey. Proceed ahead.");
        }
    }
    
    private IEnumerator OpenSesame() // Function which makes the wall disappear over time
    {
        var startPos = caveEntrance.transform.position;
        var endPos = -9.0f;
        float duration = 3.0f; // Duration of the movement in seconds
        float timer = 0.0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            // Interpolate position based on normalized time
            float normalizedTime = timer / duration;
            var tmp = new Vector3(startPos.x, Mathf.Lerp(startPos.y, endPos, normalizedTime), startPos.z);
            caveEntrance.transform.position = tmp;
            Debug.Log(caveEntrance.transform.position);
            
             // Increment timer
            Debug.Log(timer);
            yield return null;
        }
        
        // Disable this waypoint
        gameObject.SetActive(false);
    }
}
