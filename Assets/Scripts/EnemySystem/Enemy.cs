using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using StickyGuns.Sound;

public class Enemy : Entity
{
    public float speed = 2f;
    public float distanceFactor = 0.2f;
    public List<Weapon> weapons = new List<Weapon>();

    private Animator animator;

    Tween tween;

    protected new void Start()
    {
        animator = GetComponent<Animator>();

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
        tween?.Kill(false);
        AudioManager.Instance.Play("bigBang");
        animator.SetTrigger("death");
        Weapon[] takeMeOff = weapons.ToArray();
        foreach(Weapon weapon in takeMeOff)
        {
            weapon.TakeDamage(weapon.Health);
        }
    }

    public void DestroyAnimationFinished()
    {
        OnAllowToDie?.Invoke();
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

    private IEnumerator MoveToRandomPos()
    {
        Vector3 targetPos = CreateRandomPos();
        float distance = transform.position.sqrMagnitude - targetPos.sqrMagnitude;
        float duration = (Mathf.Abs(distance) + 1) * distanceFactor / speed;

        tween = transform.DOMove(targetPos, duration);
        yield return tween.WaitForCompletion();

        if (!IsDeath)
        {
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
        Destroy(gameObject);
    }
}
