using System;
using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;

public class TransferPlayMode : MonoBehaviour
{
    public UserMode userMode;
    
    
    private void Start()
    {
        
    }
    
    
}

public enum UserMode
{
    ClientMode,
    HostMode
}
