using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class BulletSpawner : MonoBehaviour
{
    public List<BulletPreset> BulletPresets = new List<BulletPreset>();
    public List<Bullet> Bullets = new List<Bullet>();
    public GameObject BulletPrefab;
    public Transform BulletHolder;

    public int PrewarmBulletCount = 10;

    public static BulletSpawner Instance;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Debug.Log("DUPLICATED BULLET SPAWNER DETECTED: HASTALA VISTA BABY!");
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
        foreach (var bulletType in BulletPresets)
        {
            for (int i = 0; i < PrewarmBulletCount; i++)
            {
                GameObject bulletGo = Instantiate(bulletType.Prefab, BulletHolder);
                Bullet b = bulletGo.GetComponent<Bullet>();
                b.IsActive = false;
                Bullets.Add(b);
            }
        }
    }

    public void SpawnBullet(Transform t, int damage, Bullet.Type type, int layer)
    {
        Bullet b = GetBulletFromPool(type);
        b.InitBullet(t, damage, layer);
    }

    public Bullet GetBulletFromPool(Bullet.Type type)
    {
        Bullet b;
        if (Bullets.Any(each=>!each.IsActive && each.BulletType == type))
        {
            b = Bullets.First(each => !each.IsActive && each.BulletType == type);
            b.IsActive = true;
            return b;
        }

        GameObject bulletGo = Instantiate(BulletPresets.First(each=> each.Type == type).Prefab, BulletHolder);
        b = bulletGo.GetComponent<Bullet>();
        b.IsActive = true;
        Bullets.Add(b);
        return b;
    }
}

[Serializable]
public class BulletPreset
{
    public Bullet.Type Type;
    public GameObject Prefab;
}
