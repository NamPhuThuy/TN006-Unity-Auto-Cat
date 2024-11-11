using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    public float speed = 10f;
    public GameObject bulletPrefab;

    private void Start()
    {
        
    }

    private void Reset()
    {
        speed = 10f;
    }

    private void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector2 movementDirection = new Vector2(horizontalMovement, verticalMovement).normalized;
       transform.Translate(speed * Time.deltaTime * movementDirection);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Shoot");
            // Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);  

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = transform.up * 10f; // Adjust bullet speed as needed
    }
}
