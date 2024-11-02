using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "WeaponDataListGame", menuName = "ScriptableObjects/GameData/WeaponDataListGame")]

public class WeaponDataListGame : ScriptableObject

{
    public List<WeaponData> WeaponDatas;
}

[Serializable]
public class WeaponData
{
    public string name;
    public string id;
    public string description;
    public WeaponRarity weaponRarity;
    public AttackType attackType;
    public List<WeaponStats> weaponStatsList;
}

[Serializable]
public class WeaponStats
{
    public float attackSpeed;
    public float power; // Damage multiplier
    public int damage;
    public Sprite image;
}

public enum WeaponRarity
{
    Common,
    Uncommon,
    Rare,
    Legendary
}


public enum AttackType
{
    Melee,
    Ranged
}