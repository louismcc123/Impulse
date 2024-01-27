using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private LayerMask Interactable;
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private GameObject pickUpUI;
    [SerializeField] private GameObject consumeUI;
    [SerializeField] private float ThrowingForce;
    [SerializeField] private float PickupRange;
    [SerializeField] private Transform Hand;

    public RaycastHit hit;
    private Rigidbody CurrentObjectRigidbody;
    private Collider CurrentObjectCollider;
    private bool isHoldingObject = false;
    private bool isHoldingKey = false;
    public AudioObject KeysFoundAudioObject;

    private ConsumableItem currentConsumableItem;
    private PlayerStats playerStats;
    private Vocals vocals;

    void Start()
    {
        vocals = GetComponent<Vocals>();
    }

    void Update()
    {
        if (hit.collider != null)
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
            pickUpUI.SetActive(false);
        }

        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, PickupRange, Interactable))
        {
            if (!isHoldingObject)
            {
                hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
                pickUpUI.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    PickUpObject();
                }
            }
        }

        if (isHoldingObject)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ThrowObject();
            }

            if (currentConsumableItem != null)
            {
                consumeUI.SetActive(true);

                if (Input.GetKeyDown(KeyCode.F))
                {
                    ConsumeObject();
                }
            }
            else
            {
                consumeUI.SetActive(false);
            }
        }

        if (CurrentObjectRigidbody)
        {
            CurrentObjectRigidbody.position = Hand.position;
            CurrentObjectRigidbody.rotation = Hand.rotation;
        }
    }

    public bool IsHoldingObject
    {
        get { return isHoldingObject; }
    }

    public bool IsHoldingKey
    {
        get { return isHoldingKey; }
    }

    public void ConsumeObject()
    {
        if (currentConsumableItem != null)
        {
            currentConsumableItem.Consume();

            if (currentConsumableItem.ConsumableItemDestroyed)
            {
                isHoldingObject = false;
                CurrentObjectRigidbody = null;
                CurrentObjectCollider = null;
            }
            else
            {
                CurrentObjectRigidbody.isKinematic = true;
                CurrentObjectRigidbody.useGravity = false;
                CurrentObjectCollider.enabled = false;
            }

            currentConsumableItem = null;

        }

        consumeUI.SetActive(false);
    }

    void PickUpObject()
    {
        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out RaycastHit hitInfo, PickupRange, Interactable))
        {
            CurrentObjectRigidbody = hitInfo.rigidbody;
            CurrentObjectCollider = hitInfo.collider;

            if (CurrentObjectRigidbody)
            {
                CurrentObjectRigidbody.isKinematic = true;
                CurrentObjectRigidbody.useGravity = false;
                CurrentObjectCollider.enabled = false;

                isHoldingObject = true;
                pickUpUI.SetActive(false);

                if (CurrentObjectCollider.CompareTag("Keys"))
                {
                    isHoldingKey = true;
                    vocals.Say(KeysFoundAudioObject);
                    Debug.Log("Holding key");
                }

                ConsumableItem consumableItem = CurrentObjectRigidbody.GetComponent<ConsumableItem>();

                if (consumableItem != null)
                {
                    currentConsumableItem = consumableItem;
                    consumeUI.SetActive(true);
                }
            }
        }
    }


    public void ThrowObject()
    {
        if (CurrentObjectRigidbody != null)
        {
            CurrentObjectRigidbody.isKinematic = false;
            CurrentObjectRigidbody.useGravity = true;
            CurrentObjectCollider.enabled = true;

            CurrentObjectRigidbody.AddForce(PlayerCamera.transform.forward * ThrowingForce, ForceMode.Impulse);

            CurrentObjectRigidbody = null;
            CurrentObjectCollider = null;
        }

        isHoldingObject = false;
        currentConsumableItem = null;
    }
}