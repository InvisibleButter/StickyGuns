using System;
using StickyGuns.Sound;
using UnityEngine;

public class Weapon : Entity
{
    public Transform FirePoint;
    public float CooldownTime = 2f;
    public float TimeBetweenShoot = 0.2f;
    public int MaxBullets = 5;

    public Bullet.Type BulletType;

    public Sticky sticky;

    private float _remainingShootDelay;
    private float _remainingCooldown;
    private int _currentBullets;

    public bool OnCooldown { get; set; }
    public int Damage { get; set; }

    public String soundName;

    protected void Start()
    {
        base.Start();
        _currentBullets = MaxBullets;
        OnAfterDeath += SpawnAsSticky;

        Damage = 100;
    }

    private void SpawnAsSticky()
    {
        WeaponManager.Instance.DeRegister(this);

        gameObject.SetActive(true);
        sticky.seperate();
    }

    public virtual void Shoot()
    {
        if(OnCooldown)
        {
            return;
        }

        if(_currentBullets <= 0 && !OnCooldown)
        {
            OnCooldown = true;
            _remainingCooldown = CooldownTime;
            return;
        }

        if(_remainingShootDelay <= 0)
        {
            AudioManager.Instance.Play("pew");
            SpawnBullet();
        }      
    }

    private void Update()
    {
        _remainingShootDelay -= Time.deltaTime;
        if(OnCooldown )
        {
            if(_remainingCooldown > 0)
            {
                _remainingCooldown -= Time.deltaTime;
            }
            else
            {
                OnCooldown = false;
                _currentBullets = MaxBullets;
            }
        }
    }

    protected virtual void SpawnBullet()
    {
        BulletSpawner.Instance.SpawnBullet(FirePoint, Damage, BulletType, gameObject.layer);
        _currentBullets--;
        _remainingShootDelay = TimeBetweenShoot;
    }
}
