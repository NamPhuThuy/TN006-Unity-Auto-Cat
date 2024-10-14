using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    private bool isCoroutineRunning = false;
    private AsyncOperation asyncLoad;
    private void Start()
    {
        slider.value = 0.05f;
        if (!isCoroutineRunning)
        {
            StartCoroutine(LoadYourAsyncScene());
        }
    }

    IEnumerator LoadYourAsyncScene()
    {
        asyncLoad = SceneManager.LoadSceneAsync(Module.crMapSelect, LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false;
        slider.DOValue(1f, 2).OnUpdate(() =>
        {
            if (slider.value == 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
                
                // FirebaseManager.Instance.LogEvent_LoadingSceneComplete();
            }
        }).OnComplete(() =>
        {
           
            asyncLoad.allowSceneActivation = true;
            
            // FirebaseManager.Instance.LogEvent_LoadingSceneComplete();
        });
        yield return null;
    }
}