using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Enemy : Entity
{
    public float speed = 2f;
    public float distanceFactor = 0.2f;

    protected new void Start()
    {
        base.Start();
        OnAfterDeath += RemoveEnemy;
        StartCoroutine(MoveToRandomPos());
    }

    private IEnumerator MoveToRandomPos()
    {
        if (!IsDeath)
        {
            Vector3 targetPos = CreateRandomPos();
            float distance = transform.position.sqrMagnitude - targetPos.sqrMagnitude;
            float duration = ( Mathf.Abs(distance) + 1 ) * distanceFactor * speed;

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
