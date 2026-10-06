using UnityEngine;

/// <summary>
/// Author: Noah Wendt
/// Date: ??.12.24
/// Project: Journey to the Holocene (ER-P3)
/// Handles the collisions for the scene change. Might be refactored to use events instead...
/// </summary>
/// 
public class SceneSwapCollider : MonoBehaviour
{
    [SerializeField] private string sceneName; // To declare the scene to which the user ill be transported in the inspector
    private void OnTriggerEnter(Collider other)
    {
        // Do the scene change
        SceneSwapper.Instance.SwapScene(sceneName);
    }
}
