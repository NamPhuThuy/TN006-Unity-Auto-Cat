using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerWeaponDataList", menuName = "ScriptableObjects/Player/PlayerWeaponDataList")]
public class PlayerWeaponDataList : ScriptableObject
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