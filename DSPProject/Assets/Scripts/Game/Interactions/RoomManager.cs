using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;

    public List<Door> doors;

    [SerializeField] private float totalHealthyItemsHealth = 0f;
    [SerializeField] private float heldItemsHealth = 0f;
    [SerializeField] private float requiredHealthForNextDoor = 0f;

    private bool hasBedroomAccess = false;
    private bool hasLivingRoomAccess = false;
    private bool hasKitchenAccess = false;
    private bool hasFoyerAccess = false;

    public PlayerStats playerStats;
    public XRPickUp pickUp;
    public GameManager gameManager;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        hasBedroomAccess = true;

        requiredHealthForNextDoor = 40f;

        doors[0].nextDoor = 1;
        doors[1].nextDoor = 2;
        doors[2].nextDoor = 3;
        doors[3].nextDoor = 4;
    }

    void Update()
    {
        CalculatePotentialHealthGain();

        if (playerStats.currentHealth + totalHealthyItemsHealth < requiredHealthForNextDoor)
        {
            Debug.Log("Game Over - Not enough health to open the next door!");
            gameManager.GameOver();
        }
    }

    public void OpenDoor(int doorNumber)
    {
        if (doorNumber > 0 && doorNumber <= doors.Count)
        {
            Door openedDoor = doors[doorNumber - 1];

            UpdateRoomAccess(openedDoor);

            CheckRequiredHealthForNextDoor(openedDoor);
        }
    }


    private void UpdateRoomAccess(Door door)
    {
        switch (door.nextDoor)
        {
            case 1 when !hasLivingRoomAccess:
                hasLivingRoomAccess = true;
                doors[1].nextDoor = 2;
                break;
            case 2 when !hasKitchenAccess:
                hasKitchenAccess = true;
                doors[2].nextDoor = 3;
                break;
            case 3 when !hasFoyerAccess:
                hasFoyerAccess = true;
                doors[3].nextDoor = 4;
                break;
        }
    }

    private void CalculatePotentialHealthGain()
    {
        totalHealthyItemsHealth = 0f;
        heldItemsHealth = 0f;

        if (pickUp != null && pickUp.currentConsumableItem != null)
        {
            heldItemsHealth = pickUp.currentConsumableItem.health;
        }

        List<ConsumableItem> bedroomItems = GetConsumableItemsForRoom("Bedroom");
        List<ConsumableItem> livingRoomItems = GetConsumableItemsForRoom("LivingRoom");
        List<ConsumableItem> kitchenItems = GetConsumableItemsForRoom("Kitchen");
        List<ConsumableItem> foyerItems = GetConsumableItemsForRoom("Foyer");

        if (hasBedroomAccess)
        {
            totalHealthyItemsHealth += GetConsumableHealth(bedroomItems) + heldItemsHealth;
        }

        if (hasLivingRoomAccess)
        {
            totalHealthyItemsHealth += GetConsumableHealth(livingRoomItems) + heldItemsHealth;
        }

        if (hasKitchenAccess)
        {
            totalHealthyItemsHealth += GetConsumableHealth(kitchenItems) + heldItemsHealth;
        }

        if (hasFoyerAccess)
        {
            totalHealthyItemsHealth += GetConsumableHealth(foyerItems) + heldItemsHealth;
        }
    }

    private float GetConsumableHealth(List<ConsumableItem> items)
    {
        return items.Where(item => item.health > 0 && !item.LiquidDestroyed).Sum(item => item.health);
    }

    private void CheckRequiredHealthForNextDoor(Door currentDoor)
    {
        if (currentDoor.nextDoor > 0 && currentDoor.nextDoor <= doors.Count)
        {
            Door nextDoor = doors[currentDoor.nextDoor - 1];
            requiredHealthForNextDoor = nextDoor.requiredHealth;

            Debug.Log($"Required health for next door: {requiredHealthForNextDoor}");
        }
    }

    private List<ConsumableItem> GetConsumableItemsForRoom(string room)
    {
        List<ConsumableItem> consumableItems = new List<ConsumableItem>();

        ConsumableItem[] itemsInRoom = Object.FindObjectsOfType<ConsumableItem>();

        foreach (ConsumableItem item in itemsInRoom)
        {
            if (IsInRoom(item.gameObject, room) && item.health > 0)
            {
                consumableItems.Add(item);
            }
        }

        return consumableItems;
    }

    private bool IsInRoom(GameObject consumableItem, string room)
    {
        Transform grandparent = consumableItem.transform.parent?.parent;

        if (grandparent != null)
        {
            switch (room)
            {
                case "Bedroom":
                    return grandparent.CompareTag("Bedroom");
                case "LivingRoom":
                    return grandparent.CompareTag("LivingRoom");
                case "Kitchen":
                    return grandparent.CompareTag("Kitchen");
                case "Foyer":
                    return grandparent.CompareTag("Foyer");
                default:
                    Debug.LogError("Unknown room type");
                    return false;
            }
        }
        else
        {
            return false;
        }
    }
}
