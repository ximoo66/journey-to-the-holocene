using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllersScript : MonoBehaviour
{
    public InputActionProperty grip;
    public InputActionProperty pinch;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float value = pinch.action.ReadValue<float>();
        animator.SetFloat("Trigger", value);
        value = grip.action.ReadValue<float>();
        animator.SetFloat("Grip", value);
    }
}
