using UnityEngine;
/// <summary>
/// Author: Noah Wendt
/// Project: Journey to the Holocene (ER_P3)
/// The base class for the Shamans state Machine.
/// This state Machine is used to control the shaman and progress through the cave scene.
/// At large taken from Professor Gabler's code from T2
/// </summary>
public abstract class ShamanState
{
    // Defining the states the shaman can be in
    public enum STATE
    {
        IDLE,
        HOLDOUTBOWL, 
        BREWPOTION
    };
    // StateMachine stages 
    public enum STAGE
    {
        ENTER, UPDATE, EXIT
    };
    // Internal references
    public STATE Name;
    protected STAGE Stage;
    // The next state after the current one
    protected ShamanState _nextState;
    
    // References we need further down the line of the states
    protected Animator _anim;
    protected Transform _player;
    protected Shaman _shaman;
    
    // Base constructor to pass the references to subsequent states
    public ShamanState(Animator animator, Transform player, Shaman shaman)
    {
        _anim = animator;
        _player = player;
        _shaman = shaman;
    }
    
    // We provide overridable Methods for each stage of the state.
    protected virtual void Enter() // Starting configuration of the state
    {
        Stage = STAGE.UPDATE;
    }

    protected virtual void Update() // update loop of the state. if not otherwise specified will always stay there
    {
        Stage = STAGE.UPDATE;
    }

    protected virtual void Exit() // logic for exiting the state.
    {
        Stage = STAGE.EXIT;
    }

    public ShamanState Process() // the state machine is hooked into the gameplay loop via this function
    {
        if (Stage == STAGE.ENTER)
            Enter();
        if (Stage == STAGE.UPDATE)
            Update();
        if (Stage == STAGE.EXIT)
        {
            Exit();
            return _nextState;
        }
        return this;
    }

    // A function for external classes to set a new state. 
    public void ChangeNextStateAndExit(ShamanState nextState)
    {
        // Que the passed state 
        _nextState = nextState; 
        Stage = STAGE.EXIT;
    }
}