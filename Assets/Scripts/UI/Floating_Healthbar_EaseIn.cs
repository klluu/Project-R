using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Floating_Healthbar_EaseIn : MonoBehaviour
{
    public Slider healthbarSlider;
    public Slider easeHealthbarSlider;
    private float maxHealth;
    private float health;
    public float lerpSpeed = 0.01f;
    private Player_Health playerHealth;
    private float previousHealth;

    void Start()
    {
        // Get the Player_Health component from the parent object
        playerHealth = GetComponentInParent<Player_Health>();
        // Set maxHealth to the maxHealth value from the Player_Health script
        if (playerHealth != null)
        {
            maxHealth = playerHealth.maxHealth;
            health = playerHealth.currentHealth;
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
    // Exponential interpolation function with super slow start and quick acceleration
    float ExponentialEaseIn(float start, float end, float t)
    {
        return start + (end - start) * Mathf.Pow(t, 3);
    }
    void Update()
    {
        if (health != playerHealth.currentHealth)
        {
            // Update previousHealth to the current health
            previousHealth = health;
            health = playerHealth.currentHealth;
        }

        if (maxHealth != playerHealth.maxHealth)
        {
            maxHealth = playerHealth.maxHealth;
        }

        // Calculate the difference in health
        float healthChange = Mathf.Abs(health - previousHealth);

        // Adjust lerpSpeed based on the magnitude of the health change
        lerpSpeed = Mathf.Clamp(0.2f / (healthChange + 1), 0.01f, 0.2f);

        if (healthbarSlider != null && healthbarSlider.value != health)
        {
            healthbarSlider.value = health;
        }

        if (healthbarSlider != null && easeHealthbarSlider != null && healthbarSlider.value != easeHealthbarSlider.value)
        {
            easeHealthbarSlider.value = ExponentialEaseIn(easeHealthbarSlider.value, health, lerpSpeed);
        }
    }
}