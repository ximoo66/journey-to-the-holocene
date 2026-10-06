using System;
using UnityEngine;
/// <summary>
/// Author: Noah Wendt
/// Project: ER-P3 (Journey to the Holocene)
/// This Component is used to handle the highlighting of game objects for interaction.
/// It switches the material array of the mesh renderer. So it should only be placed onto a game object with a mesh renderer component. 
/// </summary>
public class Highlight : MonoBehaviour
{
    private Material _highlightMat;
    private MeshRenderer _renderer;

    private Material[] _originalMaterials;
    private Material[] _highlightedMaterials;
    void Awake()
    {
        // Get the references and catch null reference exceptions
        _renderer = GetComponent<MeshRenderer>();
        if (_renderer == null)
            Debug.LogError("No mesh renderer found on highlighted GameObject!!");
        
        _highlightMat = Resources.Load("Highlight", typeof(Material)) as Material;
        
        if (_highlightMat == null)
            Debug.LogError("HighlightMat not found!!");

        // Set the arrays 
        _originalMaterials = _renderer.materials;
        _highlightedMaterials = new Material[_renderer.materials.Length + 1];

        for (int i = 0; i < _renderer.materials.Length; i++) // Copy the original materials into the array for the highlighted 
        {
            _highlightedMaterials[i] = _renderer.materials[i];
        }

        _highlightedMaterials[^1] = _highlightMat; // set the last index to the highlight material
    }

    // Used for controlling the Mesh renderer during runtime
    public void ActivateHighlight()
    {
        _renderer.materials = _highlightedMaterials;
    }

    public void DeactivateHighlight()
    {
        _renderer.materials = _originalMaterials;
    }

    private void OnEnable()
    {
        Shaman.ActivateHighlight += ActivateHighlight;
        Shaman.DeactivateHighlight += DeactivateHighlight;
        SceneSwapper.EnableHighlight += ActivateHighlight;
        SceneSwapper.DisaBleHighlight += DeactivateHighlight;
    }

    private void OnDisable()
    {
        Shaman.ActivateHighlight -= ActivateHighlight;
        Shaman.DeactivateHighlight -= DeactivateHighlight;
        SceneSwapper.EnableHighlight -= ActivateHighlight;
        SceneSwapper.DisaBleHighlight -= DeactivateHighlight;
    }
    /*
    // just for testing
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            ActivateHighlight();
        if (Input.GetKeyDown(KeyCode.Escape))
            DeactivateHighlight();
    }
    */
}
