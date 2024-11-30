using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Data;
using GameFramework.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class MapManager : Singleton<MapManager>
    {
        [SerializeField] private MapSelectionData _mapData;
        private int _currentMapId = 0;

        public int CurrentMapId
        {
            get => _currentMapId;
            set => _currentMapId = value;
        }

        [SerializeField] private GameObject _mapTerrain;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "GamePlay")
            {
                _mapTerrain = GameObject.Find("MapTerrain");
                _mapTerrain.GetComponent<MeshRenderer>().material = _mapData.maps[_currentMapId].mapMat;
            }
        }
    }
}
