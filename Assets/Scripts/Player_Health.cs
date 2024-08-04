//Kevin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth;
    [SerializeField] private float currentHealth;
    

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void takeDamage(float amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
