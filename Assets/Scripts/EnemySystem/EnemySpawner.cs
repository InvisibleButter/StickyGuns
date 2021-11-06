using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int minRespawnDuration;
    public int maxRespawnDuration;

    public int targetEnemyAmount;
    public Transform enemyHolder;
    public Enemy enemyPrefab;
    public Transform spawnPoint;

    public int livingEntitys;

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
            livingEntitys = 0;
        }
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SchedulRespawn());
    }

    IEnumerator SchedulRespawn()
    {
        int waitForRespawn = Random.Range(minRespawnDuration, maxRespawnDuration);
        yield return new WaitForSeconds(waitForRespawn);

        if(livingEntitys < targetEnemyAmount)
        {
            Enemy enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity, enemyHolder);
            livingEntitys++;
            enemy.OnAfterDeath += OnEnemyKilled;
            StartCoroutine(SchedulRespawn());
        }
    }

    void OnEnemyKilled()
    {
        livingEntitys--;
        StartCoroutine(SchedulRespawn());
    }
}
