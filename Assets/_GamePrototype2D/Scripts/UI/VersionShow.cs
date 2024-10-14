using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VersionShow : MonoBehaviour
{
    public TextMeshProUGUI txtVesion;
    void Start()
    {
        txtVesion.text = "v" + Application.version;
    }

 
}