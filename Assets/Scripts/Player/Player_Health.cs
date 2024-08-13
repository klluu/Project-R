//Kevin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth;
    [SerializeField] public float currentHealth;
    

    void Start()
    {
        currentHealth = maxHealth;
    }

    //This is only for testing purposes
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            takeDamage(10);
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            takeDamage(25);
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            takeDamage(50);
        }

        if(Input.GetKeyDown(KeyCode.V))
        {
            takeDamage(99);
        }
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
