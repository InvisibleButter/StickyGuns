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

    public Dictionary<string, List<Enemy>> living = new Dictionary<string, List<Enemy>>();

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

    public void SpawnEnemies()
    {
        foreach(EnemyData enemyData in spawnInfo)
        {
            string key = enemyData.enemy.category.ToString();
            if (!living.ContainsKey(key))
            {
                living.Add(key, new List<Enemy>());
            }

            if (living[key].Count > 0)
            {
                continue;
            }

            int amount = (int)Math.Round(enemyData.curve.Evaluate(totalKilled));
            Debug.Log("SPAWNED: " + key + " " + amount);
            for(int i = 0; i < amount; i++)
            {
                Enemy enemy = SpawnEnemy(enemyData.enemy);
                living[key].Add(enemy);
            }
        }
    }

    private Enemy SpawnEnemy(Enemy prefab)
    {
        Enemy enemy = Instantiate(prefab, spawnPoint.position, Quaternion.identity, enemyHolder);
        enemy.OnDeath += OnEnemyKilled;
        return enemy;
    }

    void OnEnemyKilled(Entity enemy)
    {
        string key = (enemy as Enemy).category.ToString();
        totalKilled++;
        living[key].Remove(enemy as Enemy);
        SpawnEnemies();
    }
}
