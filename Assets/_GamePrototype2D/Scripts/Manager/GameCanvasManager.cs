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
    [SerializeField] private CanvasLeaderboard CanvasLeaderboard;
    [SerializeField] private CanvasSettings CanvasSettings;
    [SerializeField] private CanvasShop CanvasShop;
    [SerializeField] private CanvasGarage CanvasGarage;

    [Header("Canvas In Game")] 
    [SerializeField] private CanvasStartBattle CanvasStartBattle;
    [SerializeField] private CanvasHUD CanvasHUD;
    [SerializeField] private CanvasGameWin CanvasGameWin;
    [SerializeField] private CanvasGameLose CanvasGameLose;
    [SerializeField] private CanvasGamePause CanvasGamePause;
    
    [Header("SubCanvas")]
    [SerializeField] private CanvasClickToContinue CanvasClickToContinue;

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

    public void OpenCanvas(string a)
    {
        if (CanvasList.ContainsKey(a))
        {
            Instance.CanvasList[a].Show();
        }
        else
        {
            Debug.LogError("UIError: Canvas not found: " + a);
        }
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
        CanvasList.Add("CanvasGameWin", CanvasGameWin);
        CanvasList.Add("CanvasGameLose", CanvasGameLose);
        CanvasList.Add("CanvasGamePause", CanvasGamePause);
        CanvasList.Add("CanvasSettings", CanvasSettings);
        CanvasList.Add("CanvasLeaderboard", CanvasLeaderboard);
        CanvasList.Add("CanvasShop", CanvasShop);
        
        //--Canvas InGame
        CanvasList.Add(DefineValue.CANVAS_HUD, CanvasHUD);
        CanvasList.Add(DefineValue.CANVAS_GAME_WIN, CanvasGameWin);
        CanvasList.Add(DefineValue.CANVAS_GAME_LOSE, CanvasGameLose);
        CanvasList.Add(DefineValue.CANVAS_GAME_PAUSE, CanvasGamePause);
        CanvasList.Add(DefineValue.CANVAS_START_BATTLE, CanvasStartBattle);
        
        //--Sub Canvas
        CanvasList.Add(DefineValue.CANVAS_CLICK_TO_CONTINUE, CanvasClickToContinue);
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HideChildren();
        string sceneName = scene.name;  // Get the name of the loaded scene

        // Choose and play the appropriate music based on sceneName
        switch (sceneName)
        {
            case "MainMenu":
                // AudioManager.Instance.StopAll(Audio.Type.Music);
                // AudioManager.Instance.Play(AudioEnum.MainMenu_SplashOfHope);
                currentScene = CurrentScene.MAIN_MENU;
                
                break;
            case "GamePlay":
                // AudioManager.Instance.StopAll(Audio.Type.Music);
                // AudioManager.Instance.Play(AudioEnum.GameTheme_WigglyAmbition);
                currentScene = CurrentScene.GAME_PLAY;
                
                break;
        }
    }

    public void Handle(Message message)
    {
        
    }
}
