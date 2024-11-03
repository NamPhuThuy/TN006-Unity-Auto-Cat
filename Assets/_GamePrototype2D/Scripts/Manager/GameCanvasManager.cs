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

    [Header("Canvas In Game")] 
    [SerializeField] private CanvasStartBattle CanvasStartBattle;
    [SerializeField] private CanvasHUD CanvasHUD;
    [SerializeField] private CanvasGameOver CanvasGameOver;
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
        //--Canvas Main Menu
        CanvasList.Add(DefineValue.CANVAS_EXIT, CanvasExit);
        CanvasList.Add(DefineValue.CANVAS_CREDITS, CanvasCredits);
        CanvasList.Add(DefineValue.CANVAS_SETTINGS, CanvasSettings);
        CanvasList.Add(DefineValue.CANVAS_LEADERBOARD, CanvasLeaderboard);
        
        //--Canvas InGame
        CanvasList.Add(DefineValue.CANVAS_HUD, CanvasHUD);
        CanvasList.Add(DefineValue.CANVAS_GAME_OVER, CanvasGameOver);
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
