using UnityEngine;

public class GameManager : MonoBehaviour
{
    public StartMenu Menu;
    public EnemySpawner Spawner;

    public static GameManager Instance;

    public GameMode Mode { get; set; }

    public enum GameMode
    {
        Grow,
        WeaponMess
    }

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

    public void StartGame(GameMode mode)
    {
        Mode = mode;
        Spawner.SpawnEnemies();
        ScoreManager.Instance.AddScore(0); //REFRESH
    }

    public void LooseGame()
    {
        Menu.ShowLoose();
    }
}


