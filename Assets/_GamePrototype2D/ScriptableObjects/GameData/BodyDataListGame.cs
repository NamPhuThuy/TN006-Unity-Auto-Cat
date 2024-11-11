using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "BodyDataListGame", menuName = "ScriptableObjects/GameData/BodyDataListGame")]

public class BodyDataListGame : ScriptableObject

{
    public List<BodyData> BodyDatas;
}

[Serializable]
public class BodyData
{
    public string name;
    public string id;
    public string description;
    public EquipmentRarity equipmentRarity;
    
    public List<Vector2> weaponMounts;
    public List<BodyStats> weaponStatsList;
}

[Serializable]
public class BodyStats
{
    public float health;
    public Sprite image;
}