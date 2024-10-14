using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.ResourceManagement.AsyncOperations;

[RequireComponent(typeof(Initialization))]
public class SceneController : Singleton<SceneController>
{
    private bool isLoading = false; // indicating if a scene is currently being loaded.
    private bool isLoadingBarRunning = false;
    private AsyncOperation asyncLoad;
    
    [Header("Some components")]
    [SerializeField] private Slider progressBar; //Reference to the Slider used for the loading bar.
    [SerializeField] private GameObject loadCanvas; //The canvas that contains the loading bar UI.
    
    //SceneInstance provides a wrapper for scene loading operations with Addressables, enabling delayed activation, reference counting, and better control over asynchronous scene management.
    public SceneInstance currentScene;
    public string currentSceneName = "Boostrap";
    
    private float originWidth, originHeight;
    public List<string> sceneLoadingHistory = new List<string>(); //Keeps a record of the scene names that have been loaded.
    
    private void Awake()
    {
        isLoadingBarRunning = true;
        // StartCoroutine(LoadYourAsync("MainMenu"));
        
        Debug.Log("Enter the sceneController");
        LoadScene("MainMenu", false);
        
    }
    private void DoLoadingBar(float loadTime, UnityEngine.Events.UnityAction onLoadCompleted)
    {
        isLoadingBarRunning = true;
        loadCanvas.SetActive(true);
        
        Pixelplacement.Tween.Value(0f, 1f, (float value) =>
        {
            progressBar.value = value;
        }, loadTime, 0, Pixelplacement.Tween.EaseLinear, Pixelplacement.Tween.LoopType.None, null, () =>
        {
            onLoadCompleted?.Invoke();
            isLoadingBarRunning = false;
        }, true);
    }
    
    
    public void LoadScene(string sceneName, bool currentIsAddressable = true, bool nextIsAddressable = true)
    {
        //prevent the function from being called multiple times while a scene is already in the process of loading.
        if (isLoading)
        {
            return;
        }
        
        isLoading = true;
        StopAllCoroutines();

        StartCoroutine(LoadSceneProgress(sceneName, currentIsAddressable, nextIsAddressable));
    }

    private IEnumerator LoadSceneProgress(string sceneName, bool currentIsAddressable = true, bool nextIsAddressable = true)
    {
        float duration = 1f;
        DoLoadingBar(duration, () =>{ isLoadingBarRunning = false; });

        // var loadController = transform.GetChild(0).GetComponent<LoadingSceneController>();
        
        //this will prevent error when trying to reload a certain scene
        var loadLoadingSceneTask = Addressables.LoadSceneAsync("Buffer", LoadSceneMode.Additive);
        yield return loadLoadingSceneTask;

        if (currentIsAddressable)
        {
            AsyncOperationHandle<SceneInstance> unloadCurrentSceneTask = Addressables.UnloadSceneAsync(currentScene);
            yield return unloadCurrentSceneTask;
        }
        else
        {
            // AsyncOperation unloadCurrentSceneTask = SceneManager.UnloadSceneAsync(currentSceneName);
            AsyncOperation unloadCurrentSceneTask = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
            yield return unloadCurrentSceneTask;
        }
        
        yield return new WaitForSeconds(0.05f);

        if (nextIsAddressable)
        {
            AsyncOperationHandle<SceneInstance> asyncNextSceneTask = Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            yield return asyncNextSceneTask;
            
            asyncNextSceneTask.Result.ActivateAsync(); // active the next scene
            currentScene = asyncNextSceneTask.Result; 
        }
        else // dont know how to implement this
        {
            AsyncOperation asyncNextSceneTask = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            yield return asyncNextSceneTask;

            asyncNextSceneTask.allowSceneActivation = true;
            yield return null;
            currentSceneName = SceneManager.GetActiveScene().name;
        }
        
        
        while (isLoadingBarRunning)
            yield return null;
        yield return new WaitForSeconds(0.1f); // wait a bit after the LoadingBar is completed
        // float fadeOutTime = loadController.FadeOut();
        // yield return new WaitForSecondsRealtime(fadeOutTime);
        
        Addressables.UnloadSceneAsync(loadLoadingSceneTask.Result); //release the Buffer scene
        yield return null;
        
        isLoading = false;
        loadCanvas.SetActive(false); 
    }
}