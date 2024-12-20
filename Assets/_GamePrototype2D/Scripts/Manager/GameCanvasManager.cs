using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCanvasManager : Pixelplacement.Singleton<GameCanvasManager>, IMessageHandle
{
    [Header("Canvas Main Menu")] 
    [SerializeField] private CanvasExit CanvasExit;
    [SerializeField] private CanvasCredits CanvasCredits;
    [SerializeField] private CanvasLeaderboard CanvasLeaderboard;
    [SerializeField] private CanvasSettings CanvasSettings;
    [SerializeField] private CanvasShop CanvasShop;
    [SerializeField] private CanvasGarage CanvasGarage;
    [SerializeField] private CanvasMainMenu CanvasMainMenu;

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
        CanvasList.Add("CanvasExit", CanvasExit);
        CanvasList.Add("CanvasCredits", CanvasCredits);
        // CanvasList.Add("CanvasGameOver1", CanvasGameOver);
        CanvasList.Add("CanvasSettings", CanvasSettings);
        CanvasList.Add("CanvasLeaderboard", CanvasLeaderboard);
        CanvasList.Add(DefineValue.CANVAS_SHOP, CanvasShop);
        CanvasList.Add(DefineValue.CANVAS_MAINMENU, CanvasMainMenu);
        
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
                CanvasList[DefineValue.CANVAS_MAINMENU].Show();
                
                break;
            case "GamePlay":
                // AudioManager.Instance.StopAll(Audio.Type.Music);
                // AudioManager.Instance.Play(AudioEnum.GameTheme_WigglyAmbition);
                currentScene = CurrentScene.GAME_PLAY;
                
                //Check user-mode
                StartCoroutine(SpawnVehicle());
                
                
                
                break;
        }
    }

    IEnumerator SpawnVehicle()
    {
        yield return Yielders.Get(1.2f);
        
        TransferPlayMode m_transfer = GameObject.Find(DefineValue.STR_NETWORK_MANAGER).GetComponent<TransferPlayMode>();
                
        NetworkManager m_networkManager = GameObject.Find(DefineValue.STR_NETWORK_MANAGER).GetComponent<NetworkManager>();
                
        if (m_transfer.userMode == UserMode.HostMode)
        {
            m_networkManager.StartHost();
        }
        else if (m_transfer.userMode == UserMode.ClientMode)
        {
            m_networkManager.StartClient();
        }
    }

    public void Handle(Message message)
    {
        
    }
}
