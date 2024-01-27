using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInteractable : MonoBehaviour
{
    private Material objectMaterial;
    private Material highlightMaterial;

    private void Start()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        objectMaterial = new Material(meshRenderer.material);
        highlightMaterial = new Material(objectMaterial.shader);
        highlightMaterial.color = new Color(1f, 1f, 1f, 0.5f);
    }

    public void HoverOver()
    {
        GetComponent<MeshRenderer>().materials = new Material[] { objectMaterial, highlightMaterial };
    }

    public void HoverExited()
    {
        GetComponent<MeshRenderer>().materials = new Material[] { objectMaterial };
    }
}