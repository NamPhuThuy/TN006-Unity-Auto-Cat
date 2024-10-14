using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Vector2 _startPos;
    [SerializeField] private Vector2 _spriteSize;
    [SerializeField] private float _parallaxScale;
    [SerializeField] private Vector2 _prevCameraPosition;
    
    void Start()
    {
        _mainCamera = Camera.main;
        _startPos = transform.position;
        _spriteSize = gameObject.GetComponent<SpriteRenderer>().bounds.size;
        _prevCameraPosition = _mainCamera.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float distX = _mainCamera.transform.position.x * _parallaxScale;
        float temp = _mainCamera.transform.position.x * (1 - _parallaxScale);
            
        //Set layer's position
        transform.position = new Vector2(_startPos.x + distX, _startPos.y);

        if (_prevCameraPosition.x != _mainCamera.transform.position.x)
        {
            //Check for the boundaries
            if (temp > _startPos.x + _spriteSize.x) 
                _startPos = new Vector2(_startPos.x + _spriteSize.x, _startPos.y);
            else if (temp < _startPos.x - _spriteSize.x)
                _startPos = new Vector2(_startPos.x - _spriteSize.x, _startPos.y);  
        }
    }

    public void SetParallaxScale(float a)
    {
        _parallaxScale = a;
    }
}
