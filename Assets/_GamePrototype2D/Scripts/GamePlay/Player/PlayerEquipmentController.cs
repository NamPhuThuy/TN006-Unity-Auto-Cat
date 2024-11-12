using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerEquipmentController : MonoBehaviour
{
    
    [Header("Equipment")]
    [SerializeField] private List<Wheel> _currentWheelList;
    [SerializeField] private List<Weapon> _currentWeaponList;
    [SerializeField] private Body _currentBody;

    [Header("ScriptableObjects")] 
    [SerializeField] private WeaponDataListGame _weaponDataListGame;

    [SerializeField] private BodyDataListGame _bodyDataListGame;
    [SerializeField] private WheelDataListGame _wheelDataListGame;

    [Header("Player's infor")] 
    [SerializeField] private PlayerInfor _playerInfor;

    [SerializeField] private PlayerStatsIngameManager _playerStatsIngame;
    
    void Start()
    {
        _playerStatsIngame = GetComponent<PlayerStatsIngameManager>();

        //--LOAD BODY
        foreach (BodyData bodyData in _bodyDataListGame.BodyDatas)
        {
            if (bodyData.id == _playerInfor._bodyID)
            {
                Debug.Log($"{bodyData.id}");
                _currentBody.GetComponent<SpriteRenderer>().sprite = bodyData.bodyStatsList[0].image;
                
                //Update stats
                _playerStatsIngame.Health += bodyData.bodyStatsList[0].health;
                
                break;
            }
        }
        
        //--LOAD WHEEL
        foreach (WheelData wheelData in _wheelDataListGame.WheelDatas)
        {
            if (wheelData.id == _playerInfor._wheelID)
            {
                Debug.Log($"{wheelData.id}");
                foreach (Wheel w in _currentWheelList)
                {
                    w.GetComponent<SpriteRenderer>().sprite = wheelData.wheelStatsList[0].image;    
                }
                
                //Update stats
                _playerStatsIngame.Health += wheelData.wheelStatsList[0].health;
                
                break;
            }
        }
        
        //--LOAD WEAPON
        foreach (WeaponData weaponData in _weaponDataListGame.WeaponDatas)
        {
            if (weaponData.id == _playerInfor._weaponID[0])
            {
                Debug.Log($"{weaponData.id}");
                foreach (Weapon w in _currentWeaponList)
                {
                    w.GetComponent<SpriteRenderer>().sprite = weaponData.weaponStatsList[0].image;    
                }
                
                //Update stats
                _playerStatsIngame.Damage += weaponData.weaponStatsList[0].damage;
                
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
