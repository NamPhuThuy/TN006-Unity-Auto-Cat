using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;

public class DefineValue : PropertyAttribute
{
    //Set the variable as const so that they will be created earlier?
    //SCENE NAME 
    public const string CANVAS_EXIT = "CanvasExit";
    public const string CANVAS_CREDITS = "CanvasCredits";
    public const string CANVAS_LEADERBOARD = "CanvasLeaderboard";
    public const string CANVAS_SETTINGS = "CanvasSettings";
    public const string CANVAS_SHOP = "CanvasShop";
    public const string CANVAS_GARAGE = "CanvasGarage";
    public const string CANVAS_MAINMENU = "CanvasMainMenu";
    
    
    
    public const string CANVAS_START_BATTLE = "CanvasStartBattle";
    public const string CANVAS_HUD = "CanvasHUD";
    public const string CANVAS_GAME_OVER = "CanvasGameOver";
    public const string CANVAS_GAME_PAUSE = "CanvasGamePause";

    public const string CANVAS_CLICK_TO_CONTINUE = "CanvasClickToContinue";
    
    
    //GAMEOBJECT NAME
    public const string STR_NETWORK_MANAGER = "NetworkManager";

}
