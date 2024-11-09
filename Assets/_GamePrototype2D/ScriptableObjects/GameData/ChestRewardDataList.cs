using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ChestRewardDataList", menuName = "ScriptableObjects/GameData/ChestRewardDataList")]
public class ChestRewardDataList : ScriptableObject
{
    public List<ChestRewardData> ChestRewardDatas;
}

[Serializable]
public class ChestRewardData 
{
    public ChestRank chestRank;
    public List<RarityRatio> rarityRatioList;
}

[Serializable]
public class RarityRatio
{
    [FormerlySerializedAs("weaponRarity")] public EquipmentRarity equipmentRarity;
    public float ratio;
}


public enum ChestRank
{
    COMMON,
    UNCOMMON,
    RARE,
    LEGENDARY
}
