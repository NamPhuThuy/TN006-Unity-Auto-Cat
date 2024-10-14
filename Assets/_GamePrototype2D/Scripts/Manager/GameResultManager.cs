using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;

public class GameResultManager : Singleton<GameResultManager>
{
    [Header("Scores")]
    public int highScore = 0;
    public int currentScore = 0;
    
    IEnumerator ResetStats()
    {
        //wait a bit to show the correct currentScore to the 
        yield return new WaitForSeconds(0.5f);
        currentScore = 0;
    }
}
