using UnityEngine;

public class RotateTextTowardsPlayer : MonoBehaviour
{
    public Transform player;  

    void Update()
    {
        if (player != null)
        {
            // Rotate the object to always face the player
            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.y = 0; // Keep the rotation in the horizontal plane (optional, so it doesn't tilt up/down)
            transform.rotation = Quaternion.LookRotation(directionToPlayer);
            transform.Rotate(0, 180, 0);
        }
    }
}