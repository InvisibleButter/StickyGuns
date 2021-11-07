using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using StickyGuns.Sound;

public class Enemy : ShipEntity
{
    public float speed = 2f;
    public float distanceFactor = 0.2f;
    public List<Weapon> weapons = new List<Weapon>();

    Tween tween;

    public override Type EntityType => Type.Enemy;

    public int gainedScore = 3;

    public Bullet.Type category;

    protected new void Start()
    {
        base.Start();
        OnAfterDeath += RemoveEnemy;

        LinkWeapons();

        StartCoroutine(MoveToRandomPos(false));
        StartCoroutine(StartShooting());
    }

    private void LinkWeapons()
    {
        weapons.AddRange(GetComponentsInChildren<Weapon>());

        weapons.ForEach((Weapon weapon) => weapon.OnDeath += RemoveWeapon);
    }

    private void RemoveWeapon(Entity entity)
    {
        entity.OnDeath -= RemoveWeapon;
        weapons.Remove(entity as Weapon);
    }

    protected override void ShipDestroy(Entity entity)
    {
        base.ShipDestroy(entity);
        tween?.Kill(false);
        Weapon[] takeMeOff = weapons.ToArray();
        foreach(Weapon weapon in takeMeOff)
        {
            weapon.TakeDamage(weapon.Health);
        }
    }

    private IEnumerator StartShooting()
    {
        yield return new WaitForSeconds(3);

        if (!IsDeath)
        {
            weapons.ForEach((Weapon weapon) => weapon.Shoot());
            StartCoroutine(StartShooting());
        }
    }

    private IEnumerator MoveToRandomPos(bool isInView)
    {
        Vector3 targetPos;
        if (isInView)
        {
            targetPos = CreateRandomPos();
        }
        else
        {
            targetPos = new Vector3(UnityEngine.Random.Range(-4, 4), UnityEngine.Random.Range(2, 4), transform.position.z);
        }
        float distance = transform.position.sqrMagnitude - targetPos.sqrMagnitude;
        float duration = (Mathf.Abs(distance) + 1) * distanceFactor / speed;

        tween = transform.DOMove(targetPos, duration);
        yield return tween.WaitForCompletion();

        if (!IsDeath)
        {
            StartCoroutine(MoveToRandomPos(false));
        }
    }


    private Vector3 CreateRandomPos()
    {
        float offsetX = UnityEngine.Random.Range(-1, 1);
        float offsetY = UnityEngine.Random.Range(-1, 1);

        Vector3 newPos = new Vector3(
            Math.Clamp(transform.position.x + offsetX, -5, 5),
            Math.Clamp(transform.position.y + offsetY, -4, 4),
            transform.position.z);

        return newPos;
    }

    private void RemoveEnemy()
    {
        ScoreManager.Instance.SpawnScore(transform.position, gainedScore);
        Destroy(gameObject);
    }

    public override void DestroyAnimationFinished()
    {
        base.DestroyAnimationFinished();
    }
}
