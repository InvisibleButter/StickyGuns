using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Score : MonoBehaviour
{

    public TMPro.TMP_Text text;

    public void DisplayNumber(int amount)
    {
        gameObject.SetActive(true);
        text.text = amount.ToString();
        StartCoroutine(Hide());
    }

    private IEnumerator Hide()
    {
        Tween tween = transform.DOMove(transform.position + new Vector3(0, 10, 0), 10);
        yield return tween.WaitForCompletion();
        ScoreManager.Instance.Release(this);
        gameObject.SetActive(false);
    }
}
