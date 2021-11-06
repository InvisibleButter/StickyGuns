using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class Enemy : Entity
{
    public float speed = 2f;
    public float distanceFactor = 0.2f;
    public List<Weapon> weapons = new List<Weapon>();

    protected new void Start()
    {
        base.Start();
        OnDeath += ShipDestroy;
        OnAfterDeath += RemoveEnemy;

        LinkWeapons();

        StartCoroutine(MoveToRandomPos());
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

    private void ShipDestroy(Entity entity)
    {
        weapons.ForEach((Weapon weapon) => weapon.TakeDamage(1));
    }

    private IEnumerator StartShooting()
    {
        if (!IsDeath)
        {
            yield return new WaitForSeconds(3);
            weapons.ForEach((Weapon weapon) => weapon.Shoot());
            StartCoroutine(StartShooting());
        }
    }

    private IEnumerator MoveToRandomPos()
    {
        if (!IsDeath)
        {
            Vector3 targetPos = CreateRandomPos();
            float distance = transform.position.sqrMagnitude - targetPos.sqrMagnitude;
            float duration = ( Mathf.Abs(distance) + 1 ) * distanceFactor / speed;

            Tween tween = transform.DOMove(targetPos, duration);
            yield return tween.WaitForCompletion();

            StartCoroutine(MoveToRandomPos());
        }
    }


    private Vector3 CreateRandomPos()
    {
        float x = UnityEngine.Random.Range(-5, 5);
        float y = UnityEngine.Random.Range(-4, 4);

        return new Vector3(x, y, transform.position.z);
    }

    private void RemoveEnemy()
    {
        StartCoroutine(WaitAndDo(1, () => Destroy(gameObject)));
    }

    IEnumerator WaitAndDo(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action();
    }
}
