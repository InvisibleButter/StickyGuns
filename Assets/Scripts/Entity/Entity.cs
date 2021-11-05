using UnityEngine;

public class Entity : MonoBehaviour, IEntity
{
    public int Health { get; set; }

    public int MaxHealth = 1;

    void Start()
    {
        Health = MaxHealth;
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;

        if(Health <= 0)
        {
            //DIE
            gameObject.SetActive(false);
        }
    }
}
