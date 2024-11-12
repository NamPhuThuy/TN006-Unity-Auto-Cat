using System;
using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;

public class Player1AttackState : State
{
    private void OnEnable()
    {
        //Thực hiện tấn công
        //Nếu đối thủ chết -> chuyển sang GameOverState
        //Nếu đối thủ còn sống -> chuyển sang Player2AttackState
    }
    
    
}
