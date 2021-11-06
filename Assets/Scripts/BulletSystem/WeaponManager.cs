using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<Weapon> Weapons = new List<Weapon>();

    public void Register(Weapon w)
    {
        Weapons.Add(w);
    }

    public void DeRegister(Weapon w)
    {
        Weapons.Remove(w);
        //todo maybe a pooling could be cool
        Destroy(w.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var weapon in Weapons)
            {
                weapon.Shoot();
            }
        }
    }
}
