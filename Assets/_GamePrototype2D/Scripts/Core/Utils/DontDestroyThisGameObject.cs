using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;

public class DontDestroyThisGameObject : Singleton<DontDestroyThisGameObject>
{
    protected void Awake()
    {
        /*
         * if (transform.childCount > 0)
            DontDestroyOnLoad(this.gameObject);
        
        else
            Destroy(this.gameObject);
         */
        
        DontDestroyOnLoad(this.gameObject);
        
    }
    
    void Start()
    {
        Application.targetFrameRate = 120;
    }
}