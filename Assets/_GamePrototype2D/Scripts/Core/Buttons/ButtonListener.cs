using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using Application = UnityEngine.Device.Application;

public class ButtonListener : MonoBehaviour
{
    public void ExitTheGame()
    {
        Application.Quit();
    }
    
    IEnumerator InstantiateObject(string address)
    {
        AsyncOperationHandle<GameObject> op = Addressables.InstantiateAsync(address);
        yield return op;

        if (op.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject instance = op.Result;
            // Do something with the instantiated GameObject
        }
        else
        {
            Debug.LogError("Failed to load addressable: " + address);
        }
    }

    public void DestroyThisGameObject(GameObject a)
    {
        Destroy(a);
    }
    
    public void OpenTheLink(string a)
    {
        Application.OpenURL(a);
    }

    public void SwitchActiveState(GameObject a)
    {
        if (a.activeSelf == true)
        {
            a.SetActive(false);
            return;
        }
        a.SetActive(true);
    }

    public void OpenCanvas(string a)
    {
        GameCanvasManager.Instance.OpenCanvas(a);
    }

    public void StartAsHost()
    {
        TransferPlayMode m_transfer = GameObject.Find(DefineValue.STR_NETWORK_MANAGER).GetComponent<TransferPlayMode>();
        m_transfer.userMode = UserMode.HostMode;
    }

    public void StartAsClient()
    {
        TransferPlayMode m_transfer = GameObject.Find(DefineValue.STR_NETWORK_MANAGER).GetComponent<TransferPlayMode>();
        m_transfer.userMode = UserMode.ClientMode;
    }
    
}
