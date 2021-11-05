using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform FirePoint;

    public void Shoot()
    {
        BulletSpawner.Instance.SpawnBullet(FirePoint);
    }
}
