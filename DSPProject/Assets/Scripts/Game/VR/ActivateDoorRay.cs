using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDoorRay : MonoBehaviour
{
    [SerializeField] private GameObject openUI;
    [SerializeField] private GameObject closeUI; 
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float rayActivationDistance = 3f;

    public GameObject leftRay;
    public GameObject rightRay;

    public bool rayIsActive = false;

    public Door currentDoor;

    void Update()
    {
        Ray ray = new Ray(playerTransform.position, playerTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayActivationDistance))
        {
            if (hit.collider != null && hit.collider.CompareTag("Door"))
            {
                leftRay.SetActive(true);
                rightRay.SetActive(true);
                rayIsActive = true;

                currentDoor = hit.collider.GetComponent<Door>();
                if (currentDoor != null)
                {
                    if (!currentDoor.open)
                    {
                        openUI.SetActive(true);
                    }
                    else
                    {
                        closeUI.SetActive(true);
                    }
                }
            }
        }
        else
        {
            leftRay.SetActive(false);
            rightRay.SetActive(false);
            rayIsActive = false;
            openUI.SetActive(false);
            closeUI.SetActive(false);
        }
    }
}