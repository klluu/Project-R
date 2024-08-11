using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScriptTest : MonoBehaviour
{
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public float projectileSpeed = 10f; // Speed of the projectile
    public float spreadAngle = 15f; // Angle of the spread in degrees
    public int projectileCount = 1; // Number of projectiles to shoot per shot

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        for (int i = 0; i < projectileCount; i++)
        {
            // Instantiate the projectile at the position and rotation of the current object
            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            
            // Get the Rigidbody component of the projectile
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            
            // Calculate a random direction within the horizontal spread
            float randomYaw = Random.Range(-spreadAngle / 2, spreadAngle / 2);
            Vector3 randomDirection = Quaternion.Euler(0, randomYaw, 0) * transform.up;
            
            // Add force to the projectile to move it in the random direction
            rb.velocity = randomDirection * projectileSpeed;

            // Destroy the projectile after 5 seconds
            Destroy(projectile, 5f);
        }
    }
}