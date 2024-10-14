using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    
    /*public enum TempEnum
    {
        OPTIONA,
        OPTIONB,
        OPTIONC
    }
    */

    public void OpenCanvas(string a)
    {
        if (GameCanvasManager.Instance.CanvasList.ContainsKey(a))
        {
            GameCanvasManager.Instance.CanvasList[a].Show();
        }
        else
        {
            Debug.LogError("UIError: Canvas not found: " + a);
        }
    }
}
