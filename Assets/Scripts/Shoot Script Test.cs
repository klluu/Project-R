using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScriptTest : MonoBehaviour
{
	public GameObject projectilePrefab; // Reference to the projectile prefab
	public float projectileSpeed = 10f; // Speed of the projectile
	public float coneAngle = 30f; // Angle of the cone in degrees

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
		// Instantiate the projectile at the position and rotation of the current object
		GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
		
		// Get the Rigidbody component of the projectile
		Rigidbody rb = projectile.GetComponent<Rigidbody>();
		
		// Calculate a random direction within the cone
		Vector3 randomDirection = Quaternion.Euler(
			Random.Range(-coneAngle / 2, coneAngle / 2),
			Random.Range(-coneAngle / 2, coneAngle / 2),
			0
		) * transform.up;
		
		// Add force to the projectile to move it in the random direction
		rb.velocity = randomDirection * projectileSpeed;

		// Destroy the projectile after 5 seconds
		Destroy(projectile, 5f);
	}
}