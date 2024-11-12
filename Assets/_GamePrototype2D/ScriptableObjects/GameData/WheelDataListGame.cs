using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "WheelDataListGame", menuName = "ScriptableObjects/GameData/WheelDataListGame")]

public class WheelDataListGame : ScriptableObject
{
	public List<WheelData> WheelDatas;
}

[Serializable]
public class WheelData
{
	public string name;
	public string id;
	public string description;
	public EquipmentRarity wheelRarity;
	public List<WheelStats> wheelStatsList;
}

[Serializable]
public class WheelStats
{
	public int health;
	public Sprite image;
}