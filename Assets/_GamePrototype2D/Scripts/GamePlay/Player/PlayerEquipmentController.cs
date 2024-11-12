using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentController : MonoBehaviour
{

    [Header("Equipment")]
    [SerializeField] private Wheel _currentWheel;
    [SerializeField] private List<Weapon> _weaponList;
    [SerializeField] private Body _currentBody;

    [Header("ScriptableObjects")] 
    [SerializeField] private WeaponDataListGame _weaponDataListGame;

    [SerializeField] private BodyDataListGame _bodyDataListGame;
    [SerializeField] private WheelDataListGame _wheelDataListGame;

    [Header("Player's infor")] 
    [SerializeField] private PlayerInfor _playerInfor;
    
    void Start()
    {
        //Get the infor of equipment from scriptableObjects

        // Find the BodyData with the matching ID
        foreach (BodyData bodyData in _bodyDataListGame.BodyDatas)
        {
            if (bodyData.id == _playerInfor._bodyID)
            {
                Debug.Log($"{bodyData.id}");
                _currentBody.GetComponent<SpriteRenderer>().sprite = bodyData.bodyStatsList[0].image;
                // ...
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
