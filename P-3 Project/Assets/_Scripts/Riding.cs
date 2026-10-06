using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class Riding : MonoBehaviour
{
    public GameObject User;
    public InputActionProperty TurnAction;
    public GameObject mouveProvider;
    private bool _isRiding = false;
    private Rigidbody _rb;

    private float _speed = 5.0f;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isRiding)
        {
            Vector2 turnInput = TurnAction.action.ReadValue<Vector2>();
            //Debug.Log(turnInput);
            transform.Rotate(Vector3.up, turnInput.x);
            transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
        }
    }

    public void StartRiding()
    {
        _isRiding = true;
        mouveProvider.SetActive(false);
        User = GameObject.FindWithTag("Player");
        User.transform.parent = transform;
    }
    
    void OnEnable()
    {
        if (TurnAction != null)
        {
            TurnAction.action.Enable();
            Debug.Log("Turn Action enabled");
        }
    }
}
