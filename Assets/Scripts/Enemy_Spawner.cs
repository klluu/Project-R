//Kevin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups; //list of groups of enemeis to spawn in this wave
        public int waveQuota; //total number of enemies to spawn in this wave
        public float spawnInterval;  //interval at which to spawn enemies
        public float spawnCount; //number of enemies already spawned in this wave
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount; //number of enemies to spawn in this wave
        public int spawnCount; //number of enemies of this type already spawned in this wave
        public GameObject enemyPrefab;
    }

    public List<Wave> waves; //list of all waves
    public int currentWaveCount; //the index of the current wave [list starts at 0]

    [Header("Spawner Attributes")]
    float spawnTimer; //interval between spawning each enemy
    public int enemiesAlive;
    public int maxEnemiesAllowed;
    public bool maxEnemiesReached = false;
    public float waveInterval; //interval between wave

    [Header("Spawn Positions")]
    public List<Transform> relativeSpawnPoints; //list to store all teh relative spawn points of enemies


    Transform player;

    void Start()
    {
        player = FindObjectOfType<Player_Controller>().transform;
        CalculateWaveQuota();
    }

    void Update()
    {
        if(currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0) //check if wave ended and next wave should start
        {
            StartCoroutine(BeginNextWave());
        }


        spawnTimer += Time.deltaTime;

        //check if its time to spawn next enemy
        if(spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }

    IEnumerator BeginNextWave()
    {
        yield return new WaitForSeconds(waveInterval);

        if(currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
        Debug.LogWarning(currentWaveQuota);
    }

    /// <summary>
    /// Will stop spawning enemies if the amount of enemies on the map is maxed
    /// will only spawn enemies in a particular wave until it's time for the next wave's enemies to be spawned
    /// </summary>


    void SpawnEnemies()
    {
        //check if the min number of enemies in the waves have been spawned
        if(waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            //spawn each type of enemy until the quota is filled
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                //check if the min number of enemies of this type have been spawned
                if(enemyGroup.spawnCount < enemyGroup.enemyCount)
                {

                    if(enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }

                    Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);

                    //Vector3 spawnPosition = new Vector3(player.transform.position.x + Random.Range(-10f, 10f), 0.1f, player.transform.position.z + Random.Range(-10f, 10f));
                    //Instantiate(enemyGroup.enemyPrefab, spawnPosition, Quaternion.identity);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                }
            }
        }

        //reset the maxEnemiesReached flag if the number of enmeies alive has dropped below the max
        if(enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }


    public void OnEnemyKilled()
    {
        enemiesAlive--;
    }
}
