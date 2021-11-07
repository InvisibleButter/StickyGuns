using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    public static WeaponManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("DUPLICATED WEAPON MANAGER GOOD BYE");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public List<Weapon> Weapons = new List<Weapon>();

    public void Register(Weapon w)
    {
        w.Reset();
        Weapons.Add(w);
    }

    public void DeRegister(Weapon w)
    {
        Weapons.Remove(w);
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
