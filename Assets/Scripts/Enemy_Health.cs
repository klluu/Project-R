//Kevin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth;
    [SerializeField] private float currentHealth;

    public float despawnDistance = 20f;
    Transform player;
    

    void Start()
    {
        currentHealth = maxHealth;
        player = FindObjectOfType<Player_Controller>().transform;
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
    }

    public void takeDamage(float amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Enemy_Spawner es = FindObjectOfType<Enemy_Spawner>();
        es.OnEnemyKilled();
    }

    void ReturnEnemy()
    {
        Enemy_Spawner es = FindObjectOfType<Enemy_Spawner>();
        transform.position = player.position + es.relativeSpawnPoints[Random.Range(0, es.relativeSpawnPoints.Count)].position;
    }
}
