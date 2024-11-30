using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private float randomRange = 4.6f; 
    void Start()
    {
        Vector2 randomPosition = Random.insideUnitCircle * randomRange;
        transform.position = randomPosition;
    }
}
