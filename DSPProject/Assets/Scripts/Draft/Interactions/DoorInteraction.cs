/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorInteraction : MonoBehaviour
{
    public float interactionDistance = 3f;
    public LayerMask interactableLayer;
    public InputHelpers.Button activateButton = InputHelpers.Button.PrimaryButton;

    private XRController xrController;
    private bool isPressingButton = false;

    void Start()
    {
        xrController = GetComponent<XRController>();
    }

    void Update()
    {
        CheckForDoorInteraction();
    }

    void CheckForDoorInteraction()
    {
        if (xrController.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out isPressingButton) && isPressingButton)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance, interactableLayer))
            {
                Door door = hit.collider.GetComponent<Door>();

                if (door != null)
                {
                    door.AttemptDoorInteraction();
                }
            }
        }
    }
}
*/