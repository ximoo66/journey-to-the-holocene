using System;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// Author: Noah Wendt
/// Project: Journey to the Holocene (ER-P3)
/// The MonoBehaviour representation of the shaman. Handling the ShamanState -machine, acting as its hook into the game loop.
/// Responsible for instantiating essential objects needed for progression.  
/// </summary>
public class Shaman : MonoBehaviour
{
    private ShamanState _currentState; // The state machine hook in

    public static Action ActivateHighlight;
    public static Action DeactivateHighlight;

    // References needed by the state machine
    private Shaman _shaman;
    private Transform _player;
    private Animator _anim;

    // GameObjects for instancing  
    public GameObject Bowl;
    [SerializeField] private GameObject potionPrefab;
    public GameObject spoon;
    [SerializeField] private GameObject potionParent;

    public bool isPotionSpawned = false; // Flag to make sure the potion brewing loop is only executed once

    void Start()
    {
        // Get References
        _anim = GetComponent<Animator>();
        _player = SceneSwapper.Instance.Player.transform;
        _shaman = this;

        _currentState = new ShamanIdle(_anim, _player, _shaman); // Start in "Idle" state
        //Bowl.SetActive(false); // Set bowl inactive
        DeactivateHighlight += OnDeactivateHighlight;
    }

    private void OnDeactivateHighlight() // this will be called at the beginning of the shaman brew potion state
    {
        spoon.SetActive(true);
    }

    void Update()
    {
        _currentState = _currentState.Process(); // Process the state machine
    }

    private void OnEnable()
    {
        // Subscribe to the PotionRecipe event
        PotionRecipe.OnPotionCompletionEvent += PotionRecipieOnOnPotionCompletionEvent;
    }

    private void OnDisable()
    {
        // Unsubscribe to the PotionRecipe event
        PotionRecipe.OnPotionCompletionEvent -= PotionRecipieOnOnPotionCompletionEvent;
        Potion.OnPlayerGrapPotion -= OnPlayerGrapPotionEvent;
    }

    [ContextMenu("Brew potion")]
    private void PotionRecipieOnOnPotionCompletionEvent()
    {
        // Set the next state so the player can progress
        _currentState.ChangeNextStateAndExit(new ShamanBrewPotion(_anim, _player, _shaman));
    }

    private void OnPlayerGrapPotionEvent()
    {
        // Set the next state so the player can progress
        _anim.SetTrigger("DoStandUp");
       // transform.position += new Vector3(1f, 0, 0);
        _anim.SetTrigger("DoIdle");
    }

    [ContextMenu("Spawn Potion")]
    public void SpawnPotion()
    {
        spoon.SetActive(false);
        Debug.Log("Potion spawned");
        isPotionSpawned = true; // Set the flag so the loop does not execute a second time
        // Instantiate potion & set bowl inactive
        _anim.SetTrigger("DoGivePotion");
        var potion = Instantiate(potionPrefab, potionParent.transform.position, potionParent.transform.rotation);
        Potion.OnPlayerGrapPotion += OnPlayerGrapPotionEvent;
        potion.transform.Rotate(Vector3.forward, 90f);
        potion.transform.SetParent(potionParent.transform);

        // Start playing the sequential voice lines
        StartCoroutine(PlaySequentialVoiceLines());
    }

    private System.Collections.IEnumerator PlaySequentialVoiceLines()
    {
        // Voice line 12
        Debug.Log("The way is open. Step forward, traveler.");
        SoundManager.Instance.PlayVoiceLineAtSource("Line12", _anim.gameObject.GetComponent<AudioSource>());
        yield return new WaitForSeconds(6f); // the length of the voice line

        // Voice line 13
        Debug.Log("Drink the potion and let it carry you across time.");
        SoundManager.Instance.PlayVoiceLineAtSource("Line13", _anim.gameObject.GetComponent<AudioSource>());
        yield return new WaitForSeconds(7f);

        // Voice line 14
        Debug.Log("Trust in the spirits, for they will guide you on this journey.");
        SoundManager.Instance.PlayVoiceLineAtSource("Line14", _anim.gameObject.GetComponent<AudioSource>());
    }

    public void InvokeActivateHighlight()
    {
        ActivateHighlight.Invoke();
    }

    public void InvokeDeactivateHighlight()
    {
        DeactivateHighlight.Invoke();
    }
}