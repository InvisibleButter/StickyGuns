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

    public Action OnTakeDamage { get; set; }
    public Action<Entity> OnDeath { get; set; }
    public Action OnAfterDeath { get; set; }
    public Action OnAllowToDie { get; private set; }

    public bool IsDeath { get { return Health <= 0; } }

    public int Health { get; protected set; }

    protected void Start()
    {
        Health = MaxHealth;
        OnAllowToDie += Die;
    }

    private void Die()
    {
        //DIE
        StopAllCoroutines();
        gameObject.SetActive(false);

        OnAfterDeath?.Invoke();
    }

    public void TakeDamage(int amount)
    {
        if(Health < 0)
        {
            return;
        }

        Health -= amount;

        OnTakeDamage?.Invoke();

        if(Health <= 0)
        {
            OnDeath?.Invoke(this);
        }
    }
}
