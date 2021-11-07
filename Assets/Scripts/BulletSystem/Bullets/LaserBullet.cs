using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : Bullet
{
    public List<ParticleSystem> effects;
    public override Type BulletType => Type.Laser;
    Vector3 offset;

    public override void InitBullet(Transform t, int damage, int shootFromLayer)
    {
        _weaponTrans = t;
        offset = transform.position;
        Damage = damage;
        gameObject.SetActive(true);
        transform.position = t.position;
        transform.rotation = t.rotation;
        Speed = 0f;
        this.shootFromLayer = shootFromLayer;
        stopEffects();
        GetComponent<PolygonCollider2D>().enabled = false;
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

    private void FixedUpdate()
    {
        transform.position =_weaponTrans.position;
    }

    public  override void setActiveFalse()
    { 

    }

    public void DamageStart()
    {
        GetComponent<PolygonCollider2D>().enabled = true;
    }
}