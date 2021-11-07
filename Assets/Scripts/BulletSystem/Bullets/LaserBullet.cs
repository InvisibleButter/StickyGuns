using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : Bullet
{
    public List<ParticleSystem> effects;
    public override Type BulletType => Type.Laser;



    public override void InitBullet(Transform t, int damage, int shootFromLayer)
    {
        _weaponTrans = t;
        Damage = damage;
        gameObject.SetActive(true);
        transform.position = t.position;
        transform.rotation = t.rotation;
        Speed = 0f;
        this.shootFromLayer = shootFromLayer;
        stopEffects();
    }

    public void DestroyMe()
    {
        stopEffects();
        IsActive = false;
    }

    public void startEffect(int number)
    {
        for (int i = 0; i < effects.Count; i++)
        {
            if (i == number)
            {
                effects[i].Play();
            }
            else
            {
                effects[i].Stop();
            }
        }
    }

    private void stopEffects()
    {
        for (int i = 0; i < effects.Count; i++)
        {
            effects[i].Stop();
        }
    }
}