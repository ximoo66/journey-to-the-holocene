using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Author: Noah Wendt
/// Project: Journey to the Holocene (ER-P3)
/// The potion behaviour. Checking if the player drinks and initiates the scene change if done so. 
/// </summary>
public class Potion : MonoBehaviour
{
    private readonly float _pourThreshold = 50.0f; // When does the pour start?
    private float _fillAmount = 5.0f; // How much of the potion is still left. 

    private bool _isHeldByPlayer = false;
    private Rigidbody _rb;
    private XRGrabInteractable _grabInteractable;

    public static Action OnPlayerGrapPotion;
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        //Debug.Log(_rb.name);
        _grabInteractable = GetComponent<XRGrabInteractable>();
        
        _grabInteractable.selectEntered.AddListener(ResetIsKinematic);
    }

    
    public void ResetIsKinematic(SelectEnterEventArgs args)
    {
        Debug.Log("Select entered subscriber called");
        OnPlayerGrapPotion.Invoke();
        _isHeldByPlayer = true;
        _rb.isKinematic = false;
    }

    void Update()
    {
        if (_fillAmount <= 0)
            return;
        if (_isHeldByPlayer)
        {
            // Get the rotation of the transform
            Vector3 rotation = transform.eulerAngles;

            // Normalize the rotation values to the range -180 to 180
            float normalizedX = NormalizeAngle(rotation.x);
            float normalizedZ = NormalizeAngle(rotation.z);

            // Debug.Log("x: " + normalizedX + ", z: " + normalizedZ);
        
            // Check if the x or z rotation is beyond the threshold
            if (Mathf.Abs(normalizedX) >= _pourThreshold || Mathf.Abs(normalizedZ) >= _pourThreshold)
            {
                _fillAmount -= Time.deltaTime; // Take one second to pour all out
                if (_fillAmount <= 0)
                {
                    Debug.Log("Player did drink the entire potion.");
                    // Initiate the scene change
                    SceneSwapper.Instance.SwapScene("MastodonScene");
                }
            }
        }
    }

    
    float NormalizeAngle(float angle) // Normalize an angle to the range -180 to 180
    {
        angle = angle % 360;
        if (angle > 180) angle -= 360;
        return angle;
    }
    
}