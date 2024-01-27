/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator openandclose;
    public bool open;
    public GameObject Player;
    public GameObject key;

    public float requiredHealth;
    public AudioObject lockedDoorAudioObject;
    public AudioObject KeysAudioObject;
    public float lockedDoorCooldownTime = 5f;

    public int doorNumber;
    public int nextDoor;

    [SerializeField] private GameManager gameManager;
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
            //gameManager = GetComponent<GameManager>();
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

    void OnMouseOver()
    {
        float dist = Vector3.Distance(Player.transform.position, transform.position);

        if (dist < 15 && Input.GetMouseButtonDown(0))
        {
            if (open == false)
            {
                if (playerStats.currentHealth >= requiredHealth)
                {
                    if (doorNumber == 4 && pickUp.isHoldingKey)
                    {
                        Debug.Log("Opening exit door");
                        StartCoroutine(opening());
                        gameManager.Win();
                    }
                    else if (doorNumber != 4)
                    {
                        Debug.Log("Opening door");
                        StartCoroutine(opening());
                        RoomManager.instance.OpenDoor(doorNumber);
                    }
                    else
                    {
                        vocals.Say(KeysAudioObject);
                        Debug.Log("Need key to open");
                    }
                }
                else
                {
                    vocals.Say(lockedDoorAudioObject);
                }
            }
            else
            {
                StartCoroutine(closing());
            }
        }
    }

    IEnumerator opening()
    {
        openandclose.Play("Opening");
        open = true;
        yield return new WaitForSeconds(.5f);
    }

    IEnumerator closing()
    {
        Debug.Log("You are closing the door");
        openandclose.Play("Closing");
        open = false;
        yield return new WaitForSeconds(.5f);
    }
}*/