using UnityEngine;

public class GameManager : MonoBehaviour
{
    public StartMenu Menu;
    public EnemySpawner Spawner;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("DUPLICATED Game MANAGER SEE YOU SOON BABOON");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void StartGame()
    {
        Spawner.SpawnEnemies();
    }

    public void LooseGame()
    {
        Menu.ShowLoose();
    }
}


