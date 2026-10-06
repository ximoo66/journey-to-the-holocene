using UnityEngine;
/// Author: Omid Ameri
/// Project: Journey to the Holocene (ER-P3)
public class FireplaceSound : MonoBehaviour
{
    private void Start()
    {
        
        // Play the fireplace sound
        SoundManager.Instance.PlayAmbient("Fireplace");
    }
}
