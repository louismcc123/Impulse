/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerControllerManager : MonoBehaviour
{
    private XRPickUp xrPickUpScript;
    private ActivateTeleportationRay teleportationRayScript;
    private DoorInteraction doorInteractionScript;

    void Start()
    {
        xrPickUpScript = GetComponent<XRPickUp>();
        teleportationRayScript = GetComponent<ActivateTeleportationRay>();
        doorInteractionScript = GetComponent<DoorInteraction>();
    }

    void Update()
    {
        // Check the context and call the appropriate interaction
        if (CheckForDoorInteraction())
        {
            // Door interaction takes precedence
            return;
        }

        if (teleportationRayScript.enabled)
        {
            teleportationRayScript.UpdateInteraction();
        }
        else
        {
            xrPickUpScript.UpdateInteraction();
        }
    }

    bool CheckForDoorInteraction()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, doorInteractionScript.interactionDistance, doorInteractionScript.interactableLayer))
        {
            // If a door is detected, enable DoorInteraction and return true
            doorInteractionScript.enabled = true;
            return true;
        }
        else
        {
            // If no door is detected, disable DoorInteraction and return false
            doorInteractionScript.enabled = false;
            return false;
        }
    }
}
*/