using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Noah Wendt
/// Project: ER-P3 VR-NOMS Journey to the Holocene
/// In this script the logic for the Cave_2 scene is handled. 
/// It copies a lot of code from the potion and shaman class
/// </summary>
public class ReturnToCave : MonoBehaviour
{
    [SerializeField] private GameObject spearPrefab;
    [SerializeField] private Transform spearAttachTransform;
    [SerializeField] private AudioSource audioSource; // Reference to the AudioSource

    private Animator _animator;

    private int _giveSpearHash = Animator.StringToHash("DoGiveSpear");
    private int _doStandHash = Animator.StringToHash("DoStand");

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) // This method is called when the scene is loaded
    {
        Debug.Log("Scene loaded");
        StartShamanAnimation();
    }

    [ContextMenu("Start Shaman Animation")]
    private void StartShamanAnimation()
    {
        _animator = GetComponent<Animator>();
        _animator.SetTrigger(_giveSpearHash);
        Spear.PlayerGrabSpear += OnPlayerGrabbedSpear; // Set up behavior when the player grabs the spear
        
        spearPrefab.SetActive(true);
        // var spear = Instantiate(spearPrefab, spearAttachTransform.position, spearAttachTransform.rotation);
        //spear.transform.Rotate(Vector3.forward, 90f);
        //spear.transform.SetParent(spearAttachTransform.transform);
    }

    private void OnPlayerGrabbedSpear()
    {
        _animator.SetTrigger(_doStandHash);
    
        Debug.Log("Hi");
        // Start playing the sequential voice lines
        StartCoroutine(PlaySequentialVoiceLines());
        Spear.PlayerGrabSpear -= OnPlayerGrabbedSpear;
    }

    private IEnumerator PlaySequentialVoiceLines()
    {
        // Ensure the AudioSource is not null
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource is not assigned in ReturnToCave!");
            yield break;
        }
        
        // Voice line 41
        Debug.Log("Share what you have seen, and honor the mastodons' memory.");
        SoundManager.Instance.PlayVoiceLineAtSource("Line41", audioSource);
        yield return new WaitForSeconds(6f);

        // Voice line 42
        Debug.Log("Their story is now yours to protect.");
        SoundManager.Instance.PlayVoiceLineAtSource("Line42", audioSource);
    }

    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Spear.PlayerGrabSpear -= OnPlayerGrabbedSpear;
    }
}
