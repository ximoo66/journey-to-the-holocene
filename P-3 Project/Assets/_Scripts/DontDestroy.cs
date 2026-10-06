using UnityEngine;
/// <summary>
/// Author: Noah Wendt
/// Date: ??.12.24
/// Project: Journey to the Holocene (ER-P3)
/// A little component which can distribute the dont destroy on load attribute for
/// GameObjects that should not be destroyed on load. 
/// </summary>
public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // This is a comment. 
    }

}
