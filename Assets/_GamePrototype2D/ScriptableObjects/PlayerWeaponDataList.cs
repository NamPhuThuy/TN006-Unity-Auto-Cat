using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerWeaponDataList", menuName = "ScriptableObjects/PlayerWeaponDataList")]
public class PlayerWeaponDataList : ScriptableObject
{
    public List<PlayerWeaponData> PlayerWeaponDatas;
}
