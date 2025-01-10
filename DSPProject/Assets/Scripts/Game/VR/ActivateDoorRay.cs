using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateDoorRay : MonoBehaviour
{
    [SerializeField] private GameObject openUI;
    [SerializeField] private GameObject closeUI; 
    [SerializeField] private Transform playerTransform;
    [SerializeField] private InputActionReference customActionReference;
    [SerializeField] private float rayActivationDistance = 3f;

    public Door currentDoor;

    private LayerMask doorLayerMask;

    void Start()
    {
        doorLayerMask = LayerMask.GetMask("Door");
    }

    void Update()
    {
        Ray ray = new Ray(playerTransform.position, playerTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayActivationDistance, doorLayerMask))
        {
            Door door = hit.collider.GetComponent<Door>();
            if (door != null)
            {
                currentDoor = door;

                if (!door.open)
                {
                    openUI.SetActive(true);
                    closeUI.SetActive(false);
                }
                else
                {
                    openUI.SetActive(false);
                    closeUI.SetActive(true);
                }

                if (customActionReference.action.triggered)
                {
                    Debug.Log(" attempting to interact with door");
                    door.HandleDoorInteraction();
                }
            
            return; 
            }
        }
        else
        {
            openUI.SetActive(false);
            closeUI.SetActive(false);
        }
    }
}