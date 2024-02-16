using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform playerTransform;
    public float initialSpawnInterval = 2.0f;
    public float minimumSpawnInterval = 0.5f;
    public float maximumSpawnInterval = 1.0f; // Maximum allowed spawn rate
    public float spawnIntervalDecreaseRate = 0.1f;
    public float spawnDistance = 10.0f;

    private List<Vector3> spawnPositions = new List<Vector3>();
    public float currentSpawnInterval;
    private float elapsedTime;
    public float increaseRateInterval = 10.0f; // Increase rate every 10 seconds
    private float nextIncreaseTime;

    private PlayerHealth playerHealth;
    private TimeManager timeManager;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        timeManager = FindObjectOfType<TimeManager>();
        InitializeSpawnPositions();
        currentSpawnInterval = initialSpawnInterval;
        elapsedTime = 0f;
        nextIncreaseTime = increaseRateInterval;

        StartCoroutine(SpawnEnemiesContinuously());
    }

    private void Update()
    {
        
    }

    void InitializeSpawnPositions()
    {
        Vector3 playerPosition = playerTransform.position;
        float numSpawnPoints = 8;
        float angleIncrement = 360f / numSpawnPoints;

        for (int i = 0; i < numSpawnPoints; i++)
        {
            float angle = i * angleIncrement;
            Vector3 spawnPosition = playerPosition + Quaternion.Euler(0f, angle, 0f) * Vector3.forward * spawnDistance;
            spawnPositions.Add(spawnPosition);
        }
    }

    IEnumerator SpawnEnemiesContinuously()
    {
        while (!playerHealth.isDead && !timeManager.isGameOver)
        {
            SpawnEnemy();
            SoundManager.instance.Play(SoundManager.SoundName.EnemySpawn);
            currentSpawnInterval = Mathf.Max(currentSpawnInterval - spawnIntervalDecreaseRate, minimumSpawnInterval);
            currentSpawnInterval = Mathf.Clamp(currentSpawnInterval, minimumSpawnInterval, maximumSpawnInterval);

            elapsedTime += currentSpawnInterval;
            if (elapsedTime >= nextIncreaseTime)
            {
                IncreaseSpawnRate();
            }
            yield return new WaitForSeconds(currentSpawnInterval);
        }
    }

    void SpawnEnemy()
    {
        if (spawnPositions.Count > 0)
        {
            int randomIndex = Random.Range(0, spawnPositions.Count);
            Vector3 spawnPosition = spawnPositions[randomIndex];
            
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
    
    void IncreaseSpawnRate()
    {
        nextIncreaseTime += increaseRateInterval;
    }
}
