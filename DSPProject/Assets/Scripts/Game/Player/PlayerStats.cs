using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public HealthBar healthBar;
    public GameManager gameManager;

    private bool isDead;

    void Start()
    {
        maxHealth = 100f;
        currentHealth = 40f;
        healthBar.SetSliderMax(maxHealth);
        healthBar.SetSlider(currentHealth);
    }

    public void TakeDamage(float amount)
    {
        Debug.Log($"Taking damage: {amount}");
        UpdateHealth(-amount);
    }

    public void AddHealth(float amount)
    {
        Debug.Log($"Adding health: {amount}");
        UpdateHealth(amount);
    }

    private void UpdateHealth(float amount)
    {
        currentHealth += amount;

        if (currentHealth <= 0 && !isDead)
        {
            Debug.Log("Player died.");
            isDead = true;
            gameObject.SetActive(false);
            gameManager.GameOver();
        }
        else if (currentHealth >= 100)
        {
            currentHealth = 100;
            Debug.Log("Player health reached maximum.");
        }

        Debug.Log("Player health adjusted. Current health: " + currentHealth);
        healthBar.SetSlider(currentHealth);
    }
}
