using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Floating_Healthbar : MonoBehaviour
{
    public Slider healthbarSlider;
    public Slider easeHealthbarSlider;
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    public float lerpSpeed = 0.01f;
    private Player_Stats playerStats;
    private float previousHealth;

    void Start()
    {
        // Get the Player_Health component from the parent object
        playerStats = GetComponentInParent<Player_Stats>();
        // Set maxHealth to the maxHealth value from the Player_Health script
        if (playerStats != null)
        {
            maxHealth = playerStats.maxHealth;
            health = playerStats.currentHealth;
            previousHealth = health; // Initialize previousHealth
        }
        else
        {
            Debug.LogError("Player_Health component not found in parent object.");
        }

        // Check if healthbarSlider and easeHealthbarSlider are assigned
        if (healthbarSlider == null)
        {
            Debug.LogError("HealthbarSlider is not assigned.");
        }
        if (easeHealthbarSlider == null)
        {
            Debug.LogError("EaseHealthbarSlider is not assigned.");
        }
    }

    void Update()
    {
        if (health != playerStats.currentHealth)
        {
            // Update previousHealth to the current health
            previousHealth = health;
            health = playerStats.currentHealth;
        }

        if (maxHealth != playerStats.maxHealth)
        {
            maxHealth = playerStats.maxHealth;
        }

        // Calculate the difference in health
        float healthChange = Mathf.Abs(health - previousHealth);

        // Adjust lerpSpeed based on the magnitude of the health change
        lerpSpeed = Mathf.Clamp(0.1f / (healthChange + 1), 0.001f, 0.1f);

        if (healthbarSlider != null && healthbarSlider.value != health)
        {
            healthbarSlider.value = health;
        }

        if (healthbarSlider != null && easeHealthbarSlider != null && healthbarSlider.value != easeHealthbarSlider.value)
        {
            easeHealthbarSlider.value = Mathf.Lerp(easeHealthbarSlider.value, health, lerpSpeed);
        }

    }
}