using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Noah Wendt
/// Project: Journey to the Holocene (ER-P3)
/// This class is responsible for keeping track of the potion brewing mechanic.
/// Making use of a static event to inform all interested listeners when the process is finished.
/// And making use of the SoundManager to play the needed sounds at each step from the Shaman's position. 
/// </summary>
public class PotionRecipe : MonoBehaviour
{
    public delegate void PotionEventHandler(); // Define the delegate for the event
    public static event PotionEventHandler OnPotionCompletionEvent; // Define the event 

    private List<PotionIngredient> _neededIngredients; // The list to keep track of the ingredients

    [SerializeField] private AudioSource shamanAudioSource; // Now assignable in the Inspector

    void Start()
    {
        _neededIngredients = new List<PotionIngredient>(); // Initialize the list

        // Check if the AudioSource is assigned, log a warning if missing
        if (shamanAudioSource == null)
        {
            Debug.LogWarning("Shaman AudioSource is not assigned in the Inspector.");
        }
    }

    public void AddIngredientToList(PotionIngredient ingredient)
    {
        // Add the ingredient to the list and do different logic based on the amount in the list
        _neededIngredients.Add(ingredient);

        if (_neededIngredients.Count == 1)
        {
            Debug.Log("Ahh very good. I do need some more though");
            SoundManager.Instance.PlayVoiceLineAtSource("Line7", shamanAudioSource);
        }
        else if (_neededIngredients.Count == 2)
        {
            Debug.Log("Not quite enough child. I do need some more.");
            SoundManager.Instance.PlayVoiceLineAtSource("Line8", shamanAudioSource);
        }
        else if (_neededIngredients.Count == 3)
        {
            Debug.Log("Perfect. That is enough now. Give me a moment would you?");
            SoundManager.Instance.PlayVoiceLineAtSource("Line9 (2)", shamanAudioSource);

            // Inform all listeners that the recipe has been completed
            OnPotionCompletionEvent?.Invoke();
        }
    }
}
