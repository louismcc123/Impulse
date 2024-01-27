/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumption : MonoBehaviour
{
    public float damage = 20f;
    public float health = 20f;
    public GameObject consumeUI; 

    private PlayerStats playerStats;
    private PickUp pickUpScript;

    void Start()
    {
        FindPlayerStats();
        FindPickUpScript();
    }

    private void FindPlayerStats()
    {
        playerStats = GetComponentInParent<PlayerStats>();

        if (playerStats == null)
        {
            Debug.LogError("PlayerStats not found in the parent. Searching in the scene.");
            playerStats = FindObjectOfType<PlayerStats>();
        }

        if (playerStats != null)
        {

        }
        else
        {
            Debug.LogError("PlayerStats not found in the scene!");
        }
    }

    private void FindPickUpScript()
    {
        pickUpScript = FindObjectOfType<PickUp>();

        if (pickUpScript == null)
        {
            Debug.LogError("PickUp script not found in the scene!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (pickUpScript != null)
            {
                ConsumableItem currentConsumableItem = pickUpScript.GetCurrentConsumableItem();

                if (currentConsumableItem != null)
                {
                    currentConsumableItem.Consume(pickUpScript.IsHoldingObject());

                    if (playerStats != null)
                    {
                        if (currentConsumableItem.CompareTag("Wine"))
                        {
                            playerStats.TakeDamage(damage);
                        }
                        else if (currentConsumableItem.CompareTag("Food") || currentConsumableItem.CompareTag("Water"))
                        {
                            playerStats.AddHealth(health);
                        }
                    }
                }
                else
                {
                    Debug.Log("ConsumableItem not found from PickUp script.");
                }
            }
        }
    }

    public void SetPlayerStats(PlayerStats stats)
    {
        playerStats = stats;
    }

    private void ShowConsumeUI(bool show)
    {
        if (consumeUI != null)
        {
            consumeUI.SetActive(show);
        }
    }
}
*/