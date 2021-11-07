using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int minRespawnDuration;
    public int maxRespawnDuration;

    public Transform enemyHolder;
    public Transform spawnPoint;

    public List<EnemyData> spawnInfo;

    public int totalKilled = 0;

    public static EnemySpawner Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("DUPLICATED ENEMY SPAWNER: HAVE A NICE DAY!");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        foreach(EnemyData enemyData in spawnInfo)
        {
            int amount = (int)Math.Round(enemyData.curve.Evaluate(totalKilled));
            for(int i = 0; i < amount; i++)
            {
                SpawnEnemy(enemyData.enemy);
            }
        }
    }

    private void SpawnEnemy(Enemy prefab)
    {
        Enemy enemy = Instantiate(prefab, spawnPoint.position, Quaternion.identity, enemyHolder);
        enemy.OnAfterDeath += OnEnemyKilled;
    }

    void OnEnemyKilled()
    {
        totalKilled++;
        SpawnEnemies();
    }
}
