using UnityEngine;
/// <summary>
/// Author: Noah Wendt
/// Project: Journey to the Holocene (ER_P3)
/// A ShamanState inheritor. The first state the Shaman is in. Then going over to the potion brewing or the explanation for the trip. 
/// </summary>
public class ShamanIdle : ShamanState
{ 
    // Constructor. Pass the references
    public ShamanIdle(Animator animator, Transform player, Shaman shaman)
        : base(animator, player, shaman)
    {
        Name = STATE.IDLE; // Set current State
    }

    protected override void Enter()
    {
        Debug.Log("Shaman entered Idle.");
        base.Enter();
    }

    protected override void Update()
    {
        // When the player gets close enough & the potion has not been made yet
        if (Vector3.Distance(_shaman.transform.position, _player.position) < 2.5f && !_shaman.isPotionSpawned)
        {
            // Go to the HoldOutBowl state 
            Debug.Log("Close enough to the shaman");
            _nextState = new ShamanHoldOutBowl(_anim, _player, _shaman);
            Stage = STAGE.EXIT;
        }
    }
}