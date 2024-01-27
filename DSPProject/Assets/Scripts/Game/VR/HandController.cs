using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{
    [SerializeField] InputActionReference gripIA;
    [SerializeField] InputActionReference triggerIA;

    Animator handanim;

    private void Awake()
    {
        handanim = GetComponent<Animator>();
    }

private void OnEnable()
    {
        gripIA.action.performed -= Grip;
        triggerIA.action.performed -= Trigger;
    }

    private void Trigger(InputAction.CallbackContext obj)
    {
        handanim.SetFloat("Trigger", obj.ReadValue<float>());
    }

    private void Grip(InputAction.CallbackContext obj)
    {
        handanim.SetFloat("Grip", obj.ReadValue<float>());
    }
}
