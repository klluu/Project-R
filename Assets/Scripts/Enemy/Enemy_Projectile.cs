using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Projectile : MonoBehaviour
{
    public float damage = 10f;

    public void Initialize(float damage)
    {
        this.damage = damage;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player_Stats playerStats = collision.gameObject.GetComponent<Player_Stats>();
            if (playerStats != null)
            {
                playerStats.takeDamage(damage);
            }
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}