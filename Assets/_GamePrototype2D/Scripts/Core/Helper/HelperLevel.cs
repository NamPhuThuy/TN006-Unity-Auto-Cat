using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperLevel : MonoBehaviour
{
    /*currentAddressable (default: true), nextAddressable (default: true):
     - Indicates whether the current scene (the one the player is in) or the next scene (the one being loaded) uses Addressables for asset management.
        */
    public bool currentAddressable = true, nextAddressable = true;
    public void LoadLevel(string sceneName)
    {
        SceneController.Instance.LoadScene(sceneName, currentAddressable, nextAddressable);
    }
}
