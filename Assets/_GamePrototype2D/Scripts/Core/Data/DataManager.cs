using System.Collections;
using System.Collections.Generic;
using System.IO;
using Pixelplacement;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private string dataPath = "";
    public GameData gameData;
    protected override void OnRegistration()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "data.dat");
        LoadData();
#if UNITY_IOS
                    Application.targetFrameRate=60;
#endif
    }
    public DataResource GetResourceByID(string id)
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
    {
        foreach (var _resource in gameData.dataResourcesInGame)
            if (_resource.id == id) return _resource;
        return null;
    }
    
    private void ResetData()
    {
        gameData = new GameData();
        gameData.dataResourcesInGame = JsonUtility.FromJson<DataResources>(Resources.Load<TextAsset>("Data/data_resources").text).dataResources;
        gameData.dataBuildingsInGame = JsonUtility.FromJson<DataBuildings>(Resources.Load<TextAsset>("Data/data_buildings").text).dataBuildings;
        
        SaveData();
    }
    public void SaveData(bool postData = true)
    {
        string origin = JsonUtility.ToJson(gameData);
        string encrypted = Utils.XOROperator(origin, "NamPhuThuy");
        File.WriteAllText(dataPath, encrypted);
    }
    public void LoadData()
    {
        if (File.Exists(dataPath))
        {
            try
            {
                string data = File.ReadAllText(dataPath);
                string decrypted = Utils.XOROperator(data, "NamPhuThuy");
                gameData = JsonUtility.FromJson<GameData>(decrypted);
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
                ResetData();
            }
        }
        else
            ResetData();
    }
}
