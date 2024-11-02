using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameProgressData", menuName = "ScriptableObjects/GameData/GameProgressData")]
public class GameProgressData : ScriptableObject
{
    //-- SINGLE-PLAY
    public int levelPassed;
    
    //-- MULTI-PLAY
    public List<RankMilestone> rankMilestones;

}

[Serializable]
public class RankMilestone
{
    public Ranking rank;
    public int milestones;
}

public enum Ranking
{
    COPPER,
    SILVER,
    GOLD,
    PLATINUM,
    DIAMOND,
    CHALLENGER
}