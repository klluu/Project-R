using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Projectile : MonoBehaviour
{
    public float damage = 20f;

    public void Initialize(float damage)
    {
        this.damage = damage;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy_Health enemyHealth = collision.gameObject.GetComponent<Enemy_Health>();
            if (enemyHealth != null)
            {
                enemyHealth.takeDamage(damage);
            }
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}