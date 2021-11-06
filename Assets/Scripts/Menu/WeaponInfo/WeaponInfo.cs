using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfo : MonoBehaviour
{
    public GameObject LevelNumberHolder;
    public GameObject LevelNumberPrefab;

    public Animator CooldownAnimator;
    public Image WeaponIcon;
    public List<WeaponIconConfig> WeaponiconConfigs = new List<WeaponIconConfig>();

    public GameObject LevelBlobsHolder;
    public GameObject LevelBlob;

    public void Setup(Weapon w)
    {
        //i hope the weapon knows his level
    }


    [Serializable]
    public class WeaponIconConfig
    {
        public string Identifier;
        public Sprite Icon;
    }
}
