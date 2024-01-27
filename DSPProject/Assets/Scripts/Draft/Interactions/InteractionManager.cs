using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private Vector3 interactionRayPoint;
    [SerializeField] private float interactionDistance;
    [SerializeField] private LayerMask interactionLayer = default;

    [SerializeField] private bool canInteract = true;

    private InteractableObject interactableObject;

    public static InteractionManager instance;

    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (canInteract)
        {
            HandleInteractionCheck();
            HandleInteractionInput();
        }
    }

    private void HandleInteractionCheck()
    {
        Vector3 rayOrigin = Camera.main.transform.position;

        Ray ray = Camera.main.ViewportPointToRay(interactionRayPoint);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, interactionLayer))
        {
            InteractableObject newInteractable = hit.collider.GetComponent<InteractableObject>();

            if (newInteractable != interactableObject)
            {
                if (interactableObject != null)
                    interactableObject.OnLoseFocus();

                interactableObject = newInteractable;

                if (interactableObject != null)
                    interactableObject.OnFocus();
            }
        }
        else if (interactableObject != null)
        {
            interactableObject.OnLoseFocus();
            interactableObject = null;
        }
    }

    private void HandleInteractionInput()
    {
        // Example: Left Mouse Button (primary interaction)
        if (Input.GetMouseButton(0))
        {
            // Check if there's a valid interactable object
            if (interactableObject != null)
            {
                interactableObject.OnInteract();
            }
        }

        // Example: Left Mouse Button Up (release interaction)
        if (Input.GetMouseButtonUp(0))
        {
            // Check if there's a valid interactable object
            if (interactableObject != null)
            {
                interactableObject.OnEndInteract();
            }
        }

        // Add more conditions for other keybindings or input methods as needed
    }
}
