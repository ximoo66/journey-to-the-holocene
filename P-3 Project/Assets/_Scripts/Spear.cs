using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Spear : MonoBehaviour
{
    public static Action PlayerGrabSpear;
    private XRGrabInteractable _grabInteractable;
    private Rigidbody _rb;
    [SerializeField] private GameObject newParent;

    private bool isGrabbed = false;
    // Start is called before the first frame update
    void Awake()
    {
        _grabInteractable = GetComponent<XRGrabInteractable>();
        _rb = GetComponent<Rigidbody>();
        _grabInteractable.selectEntered.AddListener(ResetIsKinematic);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!isGrabbed)
        {
            transform.position = newParent.transform.position;
        }
    }

    public void ResetIsKinematic(SelectEnterEventArgs args)
    {
        
        //Debug.Log("Select entered subscriber called");
        isGrabbed = true;
        //_rb.isKinematic = false;
        Invokee();
        
    }


    public static void Invokee()
    {
        PlayerGrabSpear?.Invoke();
    }
}
