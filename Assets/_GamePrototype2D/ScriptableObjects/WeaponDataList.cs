using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDataListGame", menuName = "ScriptableObjects/WeaponDataListGame")]

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
    public Rarity rarity;
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

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}


public enum AttackType
{
    Melee,
    Ranged
}