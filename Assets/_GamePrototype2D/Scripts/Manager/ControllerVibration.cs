using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ControllerVibration : MonoBehaviour
{
    public static string VIBRATION_STATUS = "virbration_status";
    [SerializeField] private bool dontDestroyOnLoad = false;
    void Start()
    {
        if (CanVibrate)
            Vibration.Init();
        Application.targetFrameRate = 60;
        if (dontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }
    public static void Vibrate()
    {
        if (CanVibrate)
            Vibration.Vibrate();
    }
    public static void VibratePop()
    {
        if (CanVibrate)
            Vibration.VibratePop();
    }
    public static void VibratePeek()
    {
        if (CanVibrate)
            Vibration.VibratePeek();
    }
    public static void VibrationNope()
    {
        if (CanVibrate)
            Vibration.VibrateNope();
    }
    public static bool CanVibrate
    {
        get
        {
            return PlayerPrefs.GetInt(VIBRATION_STATUS, 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(VIBRATION_STATUS, value ? 1 : 0);
            if (value) Vibration.Init();
        }
    }
}