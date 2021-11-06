using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public enum Type
    {
        Player, 
        Enemy,
        Weapon
    }

    public int MaxHealth = 1;

    public Action OnDeath { get; set; }
    public Action OnAfterDeath { get; set; }

    public bool IsDeath { get { return Health <= 0; } }

    public int Health { get; protected set; }

    protected void Start()
    {
        Health = MaxHealth;
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;

        if(Health <= 0)
        {
            OnDeath?.Invoke();

            //DIE
            StopAllCoroutines();
            gameObject.SetActive(false);

            OnAfterDeath?.Invoke();
        }
    }
}
