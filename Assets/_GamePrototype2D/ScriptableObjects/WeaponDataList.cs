using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDataList", menuName = "ScriptableObjects/WeaponDataList")]

public class WeaponDataList : ScriptableObject

{
    public List<WeaponData> WeaponDatas;
}

public class PlayerWeaponData : ScriptableObject
{
    public int amount = 0;
    public string id;
}


public class WeaponData : ScriptableObject
{
    public string name;
    public string id;
    public string description;
    public int level;
    public Rarity rarity;
    public AttackType attackType;
    public float attackSpeed;
    public float power; // Damage multiplier
    public int damage;
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