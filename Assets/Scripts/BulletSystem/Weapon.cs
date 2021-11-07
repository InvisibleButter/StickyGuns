using System;
using StickyGuns.Sound;
using UnityEngine;

public class Weapon : Entity
{
    public Transform FirePoint;
    public float CooldownTime = 2f;
    public float TimeBetweenShoot = 0.2f;
    public int MaxBullets = 5;
    public int MaxLevel = 5;
    public float ScaleModifier = 0.2f;
    public int StartDamage = 1;

    public Bullet.Type BulletType;
    public Transform Visual;

    public Sticky sticky;

    private float _remainingShootDelay;
    private float _remainingCooldown;
    private int _currentBullets;

    private int _currentCollectedGuns;
    private bool _onDestroy;

    public bool OnCooldown { get; set; }
    public int Damage { get => StartDamage + (1 + CurrentLevel); }
    public override Type EntityType => Type.Weapon;

    public int CurrentLevel { get; set; }

    public String soundName;

    public WeaponType Weapontype;

    public enum WeaponType
    {
        BlueChristal,
        StrawberryWeapon,
        Pheeeeew
    }

    public void Reset()
    {
        _currentBullets = MaxBullets;
        _currentCollectedGuns = 0;
        CurrentLevel = 0;
    }

    protected void Start()
    {
        base.Start();
        _currentBullets = MaxBullets;
        OnDeath += OnWeaponDeath;
    }

    private void OnWeaponDeath(Entity obj)
    {
        if(!sticky.isReciever && !sticky.isSticky)
        {
            sticky.seperate();
            return;
        }
        WeaponManager.Instance.DeRegister(this);
        OnAllowToDie?.Invoke();
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
        if (_onDestroy)
            return;

        BulletSpawner.Instance.SpawnBullet(FirePoint, Damage, BulletType, gameObject.layer);
        _currentBullets--;
        _remainingShootDelay = TimeBetweenShoot;
    }

    public bool MayLvlUp()
    {
        int nextLevel = 1 + CurrentLevel * 2;
        if(nextLevel <= _currentCollectedGuns + 1)
        {
            if(CurrentLevel + 1 <= MaxLevel)
            {
                CurrentLevel++;
                _currentCollectedGuns = 0;

                float modifier = 1 + (ScaleModifier * (CurrentLevel + 1));
                Visual.localScale = new Vector3(modifier, modifier, modifier);
                return true;
            } 
        }
        else
        {
            _currentCollectedGuns++;
            return true;
        }

        return false;
    }

    public void DestroyMe()
    {
        _onDestroy = true;
        Destroy(transform.parent.gameObject);
    }

    public override void TakeDamage(int amount)
    {
        _currentCollectedGuns -= amount;

        if (_currentCollectedGuns <= 0)
        {
            CurrentLevel--;
            if (CurrentLevel <= 0)
            {
                base.TakeDamage(amount);
            }
            else
            {
                float modifier = 1 + (ScaleModifier * (CurrentLevel + 1));
                Visual.localScale = new Vector3(modifier, modifier, modifier);
            }
        }
    }
}
