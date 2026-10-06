using UnityEngine;
/// <summary>
/// Author: Noah Wendt
/// Project: Journey to the Holocene (ER_P3)
/// A ShamanState inheritor. Used to progress the users journey.
/// </summary>
public class ShamanHoldOutBowl : ShamanState
{
    // Constructor 
    public ShamanHoldOutBowl(Animator animator, Transform player, Shaman shaman) 
        : base(animator, player, shaman)
    {
        Name = STATE.HOLDOUTBOWL; // Set the current State
    }

    protected override void Enter()
    {
        // Activate the bowl, play sounds etc. 
        Debug.Log("Shaman entered HoldOutBowl");
        _shaman.Bowl.SetActive(true);
        _shaman.InvokeActivateHighlight();
        base.Enter();
    }
}