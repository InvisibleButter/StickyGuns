using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{

    public PlayerEntity playerEntity;
    public float stepSize;

    private float fullHeight;

    private void Awake()
    {
        fullHeight = (transform as RectTransform).rect.height;

        SubsbscribeToPlayer(playerEntity);
    }

    private void SubsbscribeToPlayer(PlayerEntity playerEntity)
    {
        if(this.playerEntity != null)
        {
            UnsubscribeFromPlayer();
        }

        this.playerEntity = playerEntity;

        playerEntity.OnTakeDamage += OnHealthChange;
    }

    private void UnsubscribeFromPlayer()
    {
        playerEntity.OnTakeDamage -= OnHealthChange;
    }

    private void OnHealthChange()
    {
        int health = Math.Max(0, playerEntity.Health);
        float amount = stepSize * health;
        RectTransform transform = this.transform as RectTransform;
        transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, amount);
    }
}
