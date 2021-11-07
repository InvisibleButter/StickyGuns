using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    private List<Score> freeToUse = new List<Score>();

    public Score prefab;

    public uint Score { get; private set; }

    public static ScoreManager Instance;

    public Action OnChange;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("DUPLICATED SCORE MANAGER DETECTED: 404");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddScore(int amount)
    {
        Score += (uint)amount;
        OnChange?.Invoke();
    }

    private Score GetOrCreateScore(Vector3 position)
    {
        if(freeToUse.Count == 0)
        {
            return Instantiate(prefab, position, Quaternion.identity, transform);
        }
        Score score = freeToUse[0];
        freeToUse.RemoveAt(0);
        return score;
    }

    public void SpawnScore(Vector3 position, int amount)
    {
        Score score = GetOrCreateScore(position);
        score.DisplayNumber(amount);
        AddScore(amount);
    }
}
