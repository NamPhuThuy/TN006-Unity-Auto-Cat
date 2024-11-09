using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Pixelplacement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCanvasManager : Singleton<GameCanvasManager>, IMessageHandle
{
    [Header("Canvas Main Menu")] 
    [SerializeField] private CanvasExit CanvasExit;
    [SerializeField] private CanvasCredits CanvasCredits;
    
    [Header("Canvas In Game")] 
    [SerializeField] private CanvasHUD CanvasHUD;
    [SerializeField] private CanvasGameOver CanvasGameOver;
    [SerializeField] private CanvasGameOver1 CanvasGameOver1;
    [SerializeField] private CanvasGamePause CanvasGamePause;
    [SerializeField] private CanvasSettings CanvasSettings;
    [SerializeField] private CanvasLeaderboard CanvasLeaderboard;
    [SerializeField] private CanvasShop CanvasShop;

    public Dictionary<string, CanvasBase> CanvasList = new Dictionary<string, CanvasBase>();
    
    public enum CurrentScene
    {
        MAIN_MENU,
        GAME_PLAY
    }
    
    [Header("Something else")]
    public CurrentScene currentScene;
    public GameObject currentCanvas;
    
    
    
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        
        HideChildren();
        StartCoroutine(AddCanvasToDict());
    }

    private void HideChildren()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);  
        }
    }

    IEnumerator AddCanvasToDict()
    {
        yield return Yielders.Get(0.02f);
        CanvasList.Add("CanvasExit", CanvasExit);
        CanvasList.Add("CanvasCredits", CanvasCredits);
        CanvasList.Add("CanvasHUD", CanvasHUD);
        CanvasList.Add("CanvasGameOver", CanvasGameOver);
        CanvasList.Add("CanvasGameOver1", CanvasGameOver);
        CanvasList.Add("CanvasGamePause", CanvasGamePause);
        CanvasList.Add("CanvasSettings", CanvasSettings);
        CanvasList.Add("CanvasLeaderboard", CanvasLeaderboard);
        CanvasList.Add("CanvasShop", CanvasShop);
        
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HideChildren();
        string sceneName = scene.name;  // Get the name of the loaded scene

        // Choose and play the appropriate music based on sceneName
        switch (sceneName)
        {
            case "MainMenu":
                /*AudioManager.Instance.StopAll(Audio.Type.Music);
                AudioManager.Instance.Play(AudioEnum.MainMenu_SplashOfHope);*/
                currentScene = CurrentScene.MAIN_MENU;
                
                break;
            case "GamePlay":
                /*AudioManager.Instance.StopAll(Audio.Type.Music);
                AudioManager.Instance.Play(AudioEnum.GameTheme_WigglyAmbition);*/
                currentScene = CurrentScene.GAME_PLAY;
                CanvasList["CanvasHUD"].Show();
                break;
        }
    }

    public void Handle(Message message)
    {
        
    }
}
