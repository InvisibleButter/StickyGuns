using System.Collections.Generic;
using UnityEngine;

public class RandomShooterHelper : MonoBehaviour
{
    public List<Weapon> Ws = new List<Weapon>();

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Ws[Random.Range(0, Ws.Count)].Shoot();
        }
    }
}
