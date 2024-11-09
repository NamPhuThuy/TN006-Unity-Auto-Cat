using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerBodyDataList", menuName = "ScriptableObjects/Player/PlayerBodyDataList")]
public class PlayerBodyDataList : ScriptableObject
{
    public List<PlayerBodyData> PlayerBodyDatas;
}

[Serializable]
public class PlayerBodyData
{
    public string id;
    public int amount = 0;
    public int level;
}