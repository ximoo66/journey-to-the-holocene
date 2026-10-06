using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Noah Wendt
/// Date: 04.12.24
/// Project: Journey to the Holocene (ER-P3)
/// A singleton which provides the functionality for changing scenes in the build.
/// ATM a bit of a mess. Needs cleanup!!! 
/// </summary>

public class SceneSwapper : MonoBehaviour
{
    public static SceneSwapper Instance { get; private set; } // Make the singleton

    public static Action EnableHighlight;

    public static Action DisaBleHighlight;
    // The async operation
    private AsyncOperation _sceneLoadingOperation = new AsyncOperation(); 
    
    // References 
    private VolumeProfile _volumeProfile; // Used for the visual representation of the scene change
    public GameObject Player; // Reference to the player to be able to put him into the right spot in the new scenes
    
    void Awake()
    {
        // Singleton 
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
        
        // Expensive but well... at least we are only doing it once.
        Player = GameObject.FindWithTag("Player");
        //_volumeProfile = GameObject.FindWithTag("Volume").GetComponent<VolumeProfile>();

        
    }

    public void SwapScene(string sceneName) // Starts the coroutine
    {
        _sceneLoadingOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive); // Start async operation
        StartCoroutine(SceneSwap(sceneName)); // Start coroutine
    }
    
    private IEnumerator SceneSwap(string  sceneName) // Loads the scene without any freeze frames
    {
        while (!_sceneLoadingOperation.isDone) // As long as the scene is not fully loaded we wait
            yield return null;
        
        SceneManager.LoadScene(sceneName); // Then we change the scene
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        // Set the player to the right position in the new scene
        var spawnPos = GameObject.FindWithTag("Anchor").transform.position;

        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player");
        }
        Player.transform.position = spawnPos;
    }

    private void OnEnable() // Subscribe to events
    {
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    private void OnDisable() // Unsubscribe from events
    {
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    public void InvokeEnable()
    {
        EnableHighlight.Invoke();
    }

    public void InvokeDisable()
    {
        DisaBleHighlight.Invoke();
    }
}