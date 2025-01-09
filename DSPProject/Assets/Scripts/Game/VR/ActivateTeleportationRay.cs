using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ActivateTeleportationRay : MonoBehaviour
{
    public GameObject leftTeleportationRay;
    public GameObject rightTeleportationRay;

    public InputActionProperty leftActivate;
    public InputActionProperty rightActivate;

    void Update()
    {
        leftTeleportationRay.SetActive(leftActivate.action.ReadValue<float>() > 0.1f);
        rightTeleportationRay.SetActive(rightActivate.action.ReadValue<float>() > 0.1f);
    }
}
