using System.Collections;
using UnityEngine;

/// <summary>
/// Author: Noah Wendt
/// Project: Journey to the Holocene (ER_P3)
/// A ShamanState inheritor. Used to progress the user's journey, handling the brewing of the potion. 
/// </summary>
public class ShamanBrewPotion : ShamanState
{
    private float _timer;

    // Constructor. Get references and set the State
    public ShamanBrewPotion(Animator animator, Transform player, Shaman shaman)
        : base(animator, player, shaman)
    {
        Name = STATE.BREWPOTION;
        _timer = 0.0f;
    }

    protected override void Enter()
    {
        Debug.Log("Shaman started brewing potion");
        _shaman.InvokeDeactivateHighlight();
        _anim.SetTrigger("DoStandUp");
        _shaman.spoon.SetActive(true);

        // Start playing the sequential voice lines
        _shaman.StartCoroutine(PlaySequentialVoiceLines());

        base.Enter();
    }

    protected override void Update()
    {
        // Wait a bit before progressing into the next state
        _timer += Time.deltaTime;

        if (_timer > 7.0f)
        {
            Debug.Log("Timer: " + _timer);
            _shaman.SpawnPotion(); // Create the potion 

            // Progress to the next state
            _nextState = new ShamanIdle(_anim, _player, _shaman);
            Stage = STAGE.EXIT;
        }
    }

    private IEnumerator PlaySequentialVoiceLines()
    {
        AudioSource audioSource = _shaman.GetComponent<AudioSource>();

        // Voice line 10
        Debug.Log("The spirits are awakening. This potion will open the path.");
        SoundManager.Instance.PlayVoiceLineAtSource("line10", audioSource);
        yield return new WaitForSeconds(7f); //   the length of the voice line

        // Voice line 11
        Debug.Log("Stand close and breathe deeply. Let the smoke carry you to the world of the mastodons.");
        SoundManager.Instance.PlayVoiceLineAtSource("line11", audioSource);
    }
}
