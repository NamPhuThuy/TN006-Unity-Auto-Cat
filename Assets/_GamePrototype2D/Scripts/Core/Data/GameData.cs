using System.Collections.Generic;
using System.Drawing;
using Pixelplacement;
using UnityEngine.Serialization;

[System.Serializable]
public class GameData
{
    public int villagerLimit = 0;
    public int currentVillagers = 0;
    public List<DataResource> dataResourcesInGame;
    public List<DataBuilding> dataBuildingsInGame;
    
    public GameData(){}
    // public GameData(List<DataResource> dataResources)
    // {
    //     this.dataResources = dataResources;
    // }
}

[System.Serializable]
public class DataResource
{
    public string id;
    public string name;
    public int value;
}

[System.Serializable]
public class DataResources
{
    //the variable-name of the list has to be the same as the list name in .json file ("dataResources")
    public List<DataResource> dataResources;
}

[System.Serializable]
public class DataBuilding
{
    public string id;
    public string name;
    public int size;
    public int capacity;
    public int workLoadlv1;
    public List<DataResource> resourcesRequiredlv1;
    public List<DataResource> resourcesRequiredlv2;
    public List<DataResource> resourcesRequiredlv3;
}

[System.Serializable]
public class DataBuildings
{
    public List<DataBuilding> dataBuildings;
}


