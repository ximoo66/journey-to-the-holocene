using UnityEngine;

/// <summary>
/// Author: Noah Wendt
/// Project: Journey to the Holocene (ER-P3)
/// Handles collisions of ingredients with the bowl game object.
/// Communicates with the recipe component located on the parent game object.
/// </summary>
public class Bowl : MonoBehaviour
{
    private PotionRecipe _recipe; // Reference to the recipe 

    void Start()
    {
        _recipe = GetComponent<PotionRecipe>(); // Get the reference 
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out PotionIngredient ingredient)) // Check if the colliding game object has the PotionIngredient component
        {
            Debug.Log("PotionIngredient detected");
            _recipe.AddIngredientToList(ingredient);    // Add the colliding ingredient to the recipe list
            ingredient.gameObject.SetActive(false);     // Deactivate the game object

            // Play mushroom drop sound
            SoundManager.Instance.PlayInteractionSound("MushroomDrop");
        }
    }
}
