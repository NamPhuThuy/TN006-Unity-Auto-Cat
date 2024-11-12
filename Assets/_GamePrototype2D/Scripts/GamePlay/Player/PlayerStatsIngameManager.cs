using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsIngameManager : MonoBehaviour
{
    [SerializeField] private float _health = 0;
    [SerializeField] private float _damage;

    

    public float Health
    {
        get => _health;
        set => _health = value;
    }
    
    public float Damage
    {
        get => _damage;
        set => _damage = value;
    } 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
