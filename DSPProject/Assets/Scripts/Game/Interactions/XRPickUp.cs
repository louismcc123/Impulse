using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class XRPickUp : MonoBehaviour
{
    [SerializeField] private InputActionReference customActionReference;
    [SerializeField] private GameObject pickUpUI;
    [SerializeField] private GameObject consumeUI;

    public AudioObject KeysFoundAudioObject;

    public bool isHoldingKey = false;
    private bool isHoldingObject = false;

    public ConsumableItem currentConsumableItem;
    private Vocals vocals;

    private void Start()
    {
        vocals = GetComponent<Vocals>();

        var directInteractors = GetComponentsInChildren<XRDirectInteractor>();
        foreach (var interactor in directInteractors)
        {
            interactor.selectEntered.AddListener(OnSelectEnter);
            interactor.selectExited.AddListener(OnSelectExit);
        }
    }

    private void OnDestroy()
    {
        var directInteractors = GetComponentsInChildren<XRDirectInteractor>();
        foreach (var interactor in directInteractors)
        {
            interactor.selectEntered.RemoveListener(OnSelectEnter);
            interactor.selectExited.RemoveListener(OnSelectExit);
        }
    }

    private void Update()
    {
        if (isHoldingObject)
        {
            if (currentConsumableItem != null)
            {
                if (customActionReference.action.triggered)
                {
                    ConsumeObject();
                }
            }
        }
    }

    private void OnSelectEnter(SelectEnterEventArgs args)
    {
        isHoldingObject = true;

        if (args.interactable.CompareTag("Keys"))
        {
            isHoldingKey = true;
            if (KeysFoundAudioObject != null)
            {
                vocals.Say(KeysFoundAudioObject);
            }
        }
        else
        {
            currentConsumableItem = args.interactable.GetComponent<ConsumableItem>();

            if (currentConsumableItem != null)
            {
                consumeUI.SetActive(true);
            }
        }
    }

    private void OnSelectExit(SelectExitEventArgs args)
    {
        isHoldingObject = false;
        isHoldingKey = false;
        pickUpUI.SetActive(false);
        consumeUI.SetActive(false);
    }

    public void ConsumeObject()
    {
        currentConsumableItem.Consume();
        currentConsumableItem = null;
        consumeUI.SetActive(false);
    }
}