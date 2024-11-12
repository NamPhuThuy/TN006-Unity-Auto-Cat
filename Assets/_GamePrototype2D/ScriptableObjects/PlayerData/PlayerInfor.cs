using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfor", menuName = "ScriptableObjects/Player/PlayerInfor")]
public class PlayerInfor : ScriptableObject
{
    //-- PERSONAL
    public string name;
    public Sprite avatar;
    
    
    
    //-- CURRENCY
    public int coin;
    public int gem;
    
    //-- PROGRESS
    public int level;
    public float currentEXP;
    public int trophy;
    public Ranking currentRank;
    
    
    //--CURRENT VEHICLE
    public string _bodyID;
    public List<string> _weaponID;
    public string _wheelID;
}



