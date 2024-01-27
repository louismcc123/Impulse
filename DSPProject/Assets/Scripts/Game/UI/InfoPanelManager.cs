using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject alcoholPanel;
    [SerializeField] private GameObject foodPanel;
    [SerializeField] private GameObject waterPanel;
    [SerializeField] private GameObject cigarettePanel;
    [SerializeField] private GameObject drugsPanel;

    private void Start()
    {
        HideAllPanels();
    }

    private void HideAllPanels()
    {
        alcoholPanel.SetActive(false);
        foodPanel.SetActive(false);
        waterPanel.SetActive(false);
        cigarettePanel.SetActive(false);
        drugsPanel.SetActive(false);
    }

    public void ShowPanelForDuration(string itemType, float duration)
    {
        HideAllPanels();

        switch (itemType)
        {
            case "Alcohol":
                alcoholPanel.SetActive(true);
                break;
            case "Food":
                foodPanel.SetActive(true);
                break;
            case "Water":
                waterPanel.SetActive(true);
                break;
            case "Cigarette":
                cigarettePanel.SetActive(true);
                break;
            case "Drugs":
                drugsPanel.SetActive(true);
                break;

            default:
                Debug.LogWarning($"Unknown item type: {itemType}");
                break;
        }

        StartCoroutine(DeactivatePanelAfterDuration(duration));
    }

    private IEnumerator DeactivatePanelAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        HideAllPanels();
    }
}
