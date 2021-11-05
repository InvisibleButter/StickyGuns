using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BulletSpawner : MonoBehaviour
{
    public List<Bullet> Bullets = new List<Bullet>();
    public GameObject BulletPrefab;
    public Transform BulletHolder;

    public int PrewarmBulletCount = 20;

    public static BulletSpawner Instance;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        PrewarmBullets();
    }

    private void PrewarmBullets()
    {
        for (int i = 0; i < PrewarmBulletCount; i++)
        {
            GameObject bulletGo = Instantiate(BulletPrefab, BulletHolder);
            Bullet b = bulletGo.GetComponent<Bullet>();
            b.IsActive = false;
            Bullets.Add(b);
        }
    }

    public void SpawnBullet(Transform t)
    {
        Bullet b = GetBulletFromPool();
        b.InitBullet(t);
    }

    public Bullet GetBulletFromPool()
    {
        Bullet b;
        if (Bullets.Any(each=>!each.IsActive))
        {
            b = Bullets.First(each => !each.IsActive);
            b.IsActive = true;
            return b;
        }

        GameObject bulletGo = Instantiate(BulletPrefab, BulletHolder);
        b = bulletGo.GetComponent<Bullet>();
        b.IsActive = true;
        Bullets.Add(b);
        return b;
    }
}
