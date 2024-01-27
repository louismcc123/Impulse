using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : MonoBehaviour
{
    public string itemType;
    public float damage = 20f;
    public float health = 20f;

    public AudioObject audioObject;

    private PlayerStats playerStats;
    private XRPickUp pickUpScript;
    private InfoPanelManager infoPanelManager;
    private Vocals vocals;

    private bool consumableItemDestroyed = false;
    private bool liquidDestroyed = false;

    private void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        infoPanelManager = FindObjectOfType<InfoPanelManager>();
        vocals = FindObjectOfType<Vocals>();
        if (vocals == null)
        {
            Debug.LogError("Vocals component not found.");
        }
    }

    public void Consume()
    {
        if (playerStats != null)
        {
            if (gameObject.CompareTag("Wine"))
            {
                playerStats.TakeDamage(damage);
            }
            else if (gameObject.CompareTag("Food") || gameObject.CompareTag("Water"))
            {
                playerStats.AddHealth(health);
            }

            if (infoPanelManager != null)
            {
                infoPanelManager.ShowPanelForDuration(itemType, 8f);
            }

            PlayConsumeAudio();

            DestroyObject();
        }
        else
        {
            Debug.LogError("PlayerStats not found.");
        }
    }

    public void DestroyObject()
    {      
        Transform wine = transform.Find("Wine");
        Transform water = transform.Find("Water");;

        if (wine != null)
        {
            Destroy(wine.gameObject);
            Debug.Log("Destroying wine.");
        }
        else if (water != null)
        {
            Destroy(water.gameObject);
            Debug.Log("Destroying water.");
        }
        else
        {
            consumableItemDestroyed = true;
            gameObject.SetActive(false);
            Debug.Log("Destroying entire object.");
        }

        liquidDestroyed = true;
    }

    private void PlayConsumeAudio()
    {
        if (audioObject != null)
        {
            vocals.Say(audioObject);
        }
    }

    public void SetPickUpScript(XRPickUp pickUp)
    {
        pickUpScript = pickUp;
    }

    public bool ConsumableItemDestroyed
    {
        get { return consumableItemDestroyed; }
    }

    public bool LiquidDestroyed
    {
        get { return liquidDestroyed; }
    }
}