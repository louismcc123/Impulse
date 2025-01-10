using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject doorLockedUI;
    [SerializeField] private GameManager gameManager;
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

            if (playerStats == null) Debug.LogError("PlayerStats component is missing from the Player object.");
            if (vocals == null) Debug.LogError("Vocals component is missing from the Player object.");
            if (pickUp == null) Debug.LogError("XRPickUp component is missing from the Player object.");
        }
        else
        {
            Debug.LogError("Player reference is not set in the Door script.");
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

    public void HandleDoorInteraction()
    {
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats is null in HandleDoorInteraction.");
            return;
        }

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
                Debug.Log("Player doesn't have enough health.");

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
