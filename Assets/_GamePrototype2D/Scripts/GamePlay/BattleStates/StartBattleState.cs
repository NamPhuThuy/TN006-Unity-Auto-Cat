using System;
using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;

public class StartBattleState : State
{
    private void OnEnable()
    {
        
        StartCoroutine(Setup());
    }

    IEnumerator Setup()
    {
        GameCanvasManager.Instance.OpenCanvas(DefineValue.CANVAS_START_BATTLE);
        //Tải data của 2 player
        GameCanvasManager.Instance.OpenCanvas(DefineValue.CANVAS_HUD);
        
        yield return Yielders.Get(1f);
        Next();
    }
    
}
