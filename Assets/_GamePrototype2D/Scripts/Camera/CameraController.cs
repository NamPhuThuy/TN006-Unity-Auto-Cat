using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    
    private float _horiInput;
    private float _vertiInput;
    private Vector2 _direction;
    private float _speed = 30f;

    [SerializeField] private CinemachineVirtualCamera _virtualCam;
    private CinemachineComponentBase _componentBase;
    private float _camDistance;
    [FormerlySerializedAs("sensitivity")]
    [Range(0f, 10f)]
    [SerializeField] private float _sensitivity = 5f;

    // Update is called once per frame
    void Update()
    {
        _horiInput = Input.GetAxisRaw("Horizontal");
        _vertiInput = Input.GetAxisRaw("Vertical");

        _direction.x = _horiInput;
        _direction.y = _vertiInput;

        HandleZoom();
    }

    private void HandleZoom()
    {
        if (_componentBase == null)
        {
            _componentBase = _virtualCam.GetCinemachineComponent(CinemachineCore.Stage.Body);
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            _camDistance = Input.GetAxis("Mouse ScrollWheel") * _sensitivity;
            if (_componentBase is CinemachineFramingTransposer)
            {
                (_componentBase as CinemachineFramingTransposer).m_CameraDistance -= _camDistance;
            }
        }
    }
    
    private void LateUpdate()
    {
        transform.Translate((Time.deltaTime * _speed) * _direction);
    }
}