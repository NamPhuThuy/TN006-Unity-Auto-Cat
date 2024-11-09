using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerWheelDataList", menuName = "ScriptableObjects/Player/PlayerWheelDataList")]
public class PlayerWheelDataList : ScriptableObject
{
    public List<PlayerWheelData> PlayerWheelDatas;
}

[Serializable]
public class PlayerWheelData
{
    public string id;
    public int amount = 0;
    public int level;
}