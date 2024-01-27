using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject doorLockedUI;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private InputActionReference customActionReference;
    [SerializeField] private ActivateDoorRay activateDoorRay;

    public GameObject Player;
    public GameObject key;
    public AudioObject lockedDoorAudioObject;
    public AudioObject KeysAudioObject;

    public float lockedDoorCooldownTime = 5f;
    public float requiredHealth;
    public int doorNumber;
    public int nextDoor;
    public bool open;

    public Animator openAndClose;

    private PlayerStats playerStats;
    private Vocals vocals;
    private XRPickUp pickUp;

    void Start()
    {
        open = false;

        if (Player != null)
        {
            playerStats = Player.GetComponent<PlayerStats>();
            vocals = Player.GetComponent<Vocals>();
            pickUp = Player.GetComponent<XRPickUp>();
        }

        switch (doorNumber)
        {
            case 1:
                nextDoor = 2;
                break;
            case 2:
                nextDoor = 3;
                break;
            case 3:
                nextDoor = 4;
                break;
        }
    }

    void Update()
    {
        if (activateDoorRay != null && activateDoorRay.currentDoor == this)
        {
            if (customActionReference.action.triggered)
            {
                HandleDoorInteraction();
            }
        }
    }

    void HandleDoorInteraction()
    {
        if (!open)
        {
            if (playerStats.currentHealth >= requiredHealth)
            {
                if (doorNumber == 4 && pickUp.isHoldingKey)
                {
                    StartCoroutine(opening());
                    gameManager.Win();
                }
                else if (doorNumber != 4)
                {
                    StartCoroutine(opening());
                    RoomManager.instance.OpenDoor(doorNumber);
                }
                else
                {
                    vocals.Say(KeysAudioObject);
                }
            }
            else
            {
                vocals.Say(lockedDoorAudioObject);
                doorLockedUI.SetActive(true);
                StartCoroutine(ClearAfterSeconds(3f));
            }
        }
        else
        {
            StartCoroutine(closing());
        }
    }

    IEnumerator opening()
    {
        openAndClose.Play("Opening");
        open = true;
        yield return new WaitForSeconds(.5f);
    }

    IEnumerator closing()
    {
        openAndClose.Play("Closing");
        open = false;
        yield return new WaitForSeconds(.5f);
    }

    private IEnumerator ClearAfterSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
        doorLockedUI.SetActive(false);
    }
}
