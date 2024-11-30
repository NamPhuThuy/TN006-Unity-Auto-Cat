using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(menuName = "MapSelectionData", fileName = "ScriptableObjects\\MapSelectionData")]
    public class MapSelectionData : ScriptableObject
    {
        public List<MapInfo> maps;
    }
}


[Serializable]
public struct MapInfo
{
    public Color mapThumbnail;
    public string mapName;
    public Material mapMat;
}