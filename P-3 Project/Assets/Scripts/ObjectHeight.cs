using UnityEngine;

public class ObjectHeight : MonoBehaviour
{
    void Start()
    {
        // Get the object's mesh bounds
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            float heightInMeters = renderer.bounds.size.y;
            Debug.Log("Object Height: " + heightInMeters + " meters");
        }
        else
        {
            Debug.LogError("Renderer not found!");
        }
    }
}
