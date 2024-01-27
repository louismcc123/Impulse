using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    [SerializeField]
    private List<Renderer> renderers;

    [SerializeField]
    private Color highlightColor = Color.white;
    private Color originalColor;

    private List<Material> materials;

    private void Awake()
    {
        materials = new List<Material>();
        foreach (var renderer in renderers)
        {
            materials.AddRange(new List<Material>(renderer.materials));
        }

        // Store the original color of the material
        originalColor = materials[0].color;
    }

    public void ToggleHighlight(bool val)
    {
        if (val)
        {
            foreach (var material in materials)
            {
                material.EnableKeyword("_EMISSION");
                material.SetColor("_EmissionColor", highlightColor);
            }
        }
        else
        {
            foreach (var material in materials)
            {
                material.DisableKeyword("_EMISSION");
            }
        }
    }

    private void Update()
    {
        // Perform raycasting to check if the player is looking at the object
        RaycastHit hit;
        int layerMask = LayerMask.GetMask("Interactable"); // Replace with your actual layer name

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            Debug.Log("Hit: " + hit.collider.gameObject.name);

            // Check if the hit object is the one with this script
            if (hit.collider.gameObject == gameObject)
            {
                // Player is looking at the object, highlight it
                ToggleHighlight(true);
            }
            else
            {
                // Player is not looking at the object, remove highlight
                ToggleHighlight(false);
            }
        }
        else
        {
            Debug.Log("No Hit");
            // Player is not looking at anything, remove highlight
            ToggleHighlight(false);
        }
    }
}
