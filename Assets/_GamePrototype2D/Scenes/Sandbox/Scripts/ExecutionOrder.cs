using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecutionOrder : MonoBehaviour
{
    // public GameObject temp;
    [SerializeField] private int rand = 0;
    private void Awake()
    {
        Debug.Log($"{gameObject.name} Awake");
    }

    private void OnEnable()
    {
        Debug.Log($"{gameObject.name} OnEnable");
    }

    void Start()
    {
        Debug.Log($"{gameObject.name} Start");   
    }

    private void FixedUpdate()
    {
        // Debug.Log($"{gameObject.name} FixedUpdate");
    }

    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{gameObject.name} OnTriggerEnter");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"{gameObject.name} OnTriggerEnter2D");
    }

    void Update()
    {
        
        /*if (Input.GetKeyDown(KeyCode.Space) && temp != null)
        {
            Debug.Log("You press the spacebar 1");
            Instantiate(temp, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            Debug.Log("You press the spacebar 2");
        }*/
        
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        Debug.Log($"{gameObject.name} focusing: {hasFocus}");
    }

    private void OnApplicationQuit()
    {
        Debug.Log($"{gameObject.name} OnApplicationQuit");
    }

    private void Reset()
    {
        Debug.Log($"{gameObject.name} Reset");
    }

    private void OnValidate()
    {
        Debug.Log($"{gameObject.name} OnValidate");
    }

    private void OnDestroy()
    {
        Debug.Log($"{gameObject.name} OnDestroy");
    }

    private void OnDisable()
    {
        Debug.Log($"{gameObject.name} OnDisable");
    }
}
