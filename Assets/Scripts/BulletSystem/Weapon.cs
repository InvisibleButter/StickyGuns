using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform FirePoint;
    public float CooldownTime = 2f;
    public float TimeBetweenShoot = 0.2f;
    public int MaxBullets = 5;

    public Bullet.Type BulletType;

    private float _remainingShootDelay;
    private float _remainingCooldown;
    private int _currentBullets;

    public bool OnCooldown { get; set; }
    public int Damage { get; set; }

    private void Start()
    {
        _currentBullets = MaxBullets;
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
        BulletSpawner.Instance.SpawnBullet(FirePoint, Damage, BulletType);
        _currentBullets--;
        _remainingShootDelay = TimeBetweenShoot;
    }
}
