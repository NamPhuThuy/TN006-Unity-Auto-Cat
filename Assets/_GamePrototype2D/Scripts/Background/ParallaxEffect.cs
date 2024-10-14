using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

//The size of the layers in _layers has to be the same as the size of the FOV
//For example: 
// The screen size is 1920 * 1080
// The sprite's size is 960 * 540, with PPU = 16 -> 60 * 33.75 
// So, the camera's orthographic size should be: 16.875 -> the FOV's height is 16.875 * 2 = 33.75

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private List<Transform> _layersList = new List<Transform>();
    [SerializeField] private List<Vector2> _startPosList = new List<Vector2>();
    [SerializeField] private List<Sprite> _layerSpritesList = new List<Sprite>();
    [SerializeField] private bool _isInfiniteHorizontal = true;
    [SerializeField] private bool _isInfiniteVertical = false;
    [SerializeField] private string _backgroundNameToLoad;
    [SerializeField] private int _backgroundLayerNumber;

    [SerializeField] private AssetReference spriteRef;
    
    public Camera _mainCamera;    // Reference to your main camera
    public float[] parallaxScales; // Parallax scaling factors for each layer
    private Vector2 _spriteSize; // the size of every sprite. All the sprites supposed to be has the same size
    private int _layerSpriteID = 0;
    

    void Start()
    {
        _mainCamera = Camera.main;
        StartCoroutine(LoadBackground());
        
    }

    IEnumerator LoadBackground()
    {
        yield return null;
        yield return null;
        for (int i = 0; i < _backgroundLayerNumber; i++)
        {
            string id = string.Format("{0:00}", i);
            Debug.Log(id);
            
            AsyncOperationHandle<Sprite> asyncOperationHandle = Addressables.LoadAssetAsync<Sprite>($"{_backgroundNameToLoad}_{id}");

            asyncOperationHandle.Completed += OnBackgroundLayerSpriteLoadComplete;
                
            
            Addressables.Release(asyncOperationHandle);
            
        }
        
        foreach (Transform child in transform)
        {
            _layersList.Add(child.transform);
            _startPosList.Add(child.transform.position);
        }

        _spriteSize = _layersList[0].GetComponent<SpriteRenderer>().bounds.size;
        
        
        // Make sure the number of layers matches the number of parallax scales
        if (_layersList.Count != parallaxScales.Length)
        {
            Debug.LogError("Number of layers must match number of parallax scales!");
        }
    }

    IEnumerator LoadLayerSprites()
    {
        yield return null;
    }

    private void OnBackgroundLayerSpriteLoadComplete(AsyncOperationHandle<Sprite> spriteHandle)
    {
        if (spriteHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Check");
            _layerSpritesList.Add(spriteHandle.Result);
            
            AsyncOperationHandle<GameObject> op = Addressables.InstantiateAsync("LayerBackground");
            op.Completed += OnBackgroundLayerPrefabComplete;
            
        }
        else
        {
            Debug.LogError($"Failed to load sprite: {_backgroundNameToLoad}_0");
        }
        
    }

    private void OnBackgroundLayerPrefabComplete(AsyncOperationHandle<GameObject> op)
    {
        if (op.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject instance = op.Result;
            SpriteRenderer spRen = instance.GetComponent<SpriteRenderer>(); 
            spRen.sprite = _layerSpritesList[_layerSpriteID];
            spRen.sortingOrder = _backgroundLayerNumber - _layerSpriteID;
            _layerSpriteID++;
        }
        else
        {
            Debug.LogError("Failed to load addressable: LayerBackground");
        }
    }
    
    

    void LateUpdate()
    {
        for (int i = 0; i < _layersList.Count; i++)
        {
            //Used to set the layer's new position
            Vector2 newPosi = _mainCamera.transform.position * parallaxScales[i];
            
            //Used for boundaries checking
            Vector2 temp = _mainCamera.transform.position * (1 - parallaxScales[i]);
            
            //Set layer's position
            _layersList[i].transform.position = new Vector2(_startPosList[i].x + newPosi.x, _startPosList[i].y + newPosi.y);

            if (_isInfiniteHorizontal)
            {
                //Check for the boundaries
                if (temp.x > _startPosList[i].x + _spriteSize.x) 
                    _startPosList[i] = new Vector2(_startPosList[i].x + _spriteSize.x, _startPosList[i].y);
                else if (temp.x < _startPosList[i].x - _spriteSize.x)
                    _startPosList[i] = new Vector2(_startPosList[i].x - _spriteSize.x, _startPosList[i].y);  
            }
            
            if (_isInfiniteVertical)
            {
                //Check for the boundaries
                if (temp.y > _startPosList[i].y + _spriteSize.y) 
                    _startPosList[i] = new Vector2(_startPosList[i].x, _startPosList[i].y + _spriteSize.y);
                else if (temp.y < _startPosList[i].y - _spriteSize.y)
                    _startPosList[i] = new Vector2(_startPosList[i].x, _startPosList[i].y - _spriteSize.y);
            }
            
        }
    
    }
}