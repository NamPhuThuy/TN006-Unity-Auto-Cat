using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WeaponDataListPlayer", menuName = "ScriptableObjects/WeaponDataListPlayer")]
public class WeaponDataListPlayer : ScriptableObject
{
    public List<WeaponDataPlayer> PlayerWeaponDatas;
}

[Serializable]
public class WeaponDataPlayer
{
    public string id;
    public int amount = 0;
    public int level;
}