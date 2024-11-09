using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using UnityEngine.UI;
public class HelperHaptic : MonoBehaviour
{
    private void OnEnable()
    {
        if(GetComponent<Button>())
            GetComponent<Button>().onClick.AddListener(PopVibration);
    }
    private void OnDisable()
    {
        if(GetComponent<Button>())
            GetComponent<Button>().onClick.RemoveListener(PopVibration);
    }
    public void PeekVibration()
    {
        ControllerVibration.VibratePeek();
    }
    public void PopVibration()
    {
        ControllerVibration.VibratePop();
    }
}