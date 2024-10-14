using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/*
 Purposes:
 - Centralized Data Management:
    + Stores and manages various game-related data using static properties and PlayerPrefs for persistence.

    + User information (ID, cheater status, first-time customer, etc.)
    + Game settings (vibration, sound, music, ads preferences, map selection)
    + Currency amounts (money, tickets)
    + Gameplay progress (rewards, playtime, last day played, achievements)

-  Event Management:

Declares several events (using delegates like REFRESH, MapProcess, DataConfigs) to signal important game events.
Other scripts can subscribe to these events to react to changes or trigger actions.
Facilitates communication and coordination between different game components
 */

public static class Module
{
    #region Statics
    public static string idDevice = "";
    public static string mart_ID  = "1";
    public static string reward_type = string.Empty;
    public static string reward_name = string.Empty;

    public static bool isAdsShowing=false;
    public static bool isAdsMuted=false;
    public static bool isShowOpenapp = true;
    public static bool isTest = false;

    public static bool isYoutuberSeat = false;
    public static bool isFirstDay = true;

    #endregion

    #region Data PlayerPrefs
    public static string idUser
    {
        get {
            string _id = PlayerPrefs.GetString("idUser", "");

            if (string.IsNullOrEmpty(_id))
            {
                _id = Guid.NewGuid().ToString();
                PlayerPrefs.SetString("idUser", _id);
            }

            //Debug.LogError(_id);
            return _id; 
        }
        set { PlayerPrefs.SetString("idUser", value); }
    }

    public static int isCheater
    {
        get { return PlayerPrefs.GetInt("isCheater", 0); }
        set { PlayerPrefs.SetInt("isCheater", value); }
    }

    public static int isFirstCustomer
    {
        get { return PlayerPrefs.GetInt("isFirstCustomer", 0); }
        set { PlayerPrefs.SetInt("isFirstCustomer", value); }
    }

    public static int isRemoveAds
    {
        get { return PlayerPrefs.GetInt("isRemoveAds", 0); }
        set { PlayerPrefs.SetInt("isRemoveAds", value); }
    }

    public static int isVip_subscription
    {
        get { return PlayerPrefs.GetInt("isVip_subscription", 0); }
        set { PlayerPrefs.SetInt("isVip_subscription", value); }
    }

    public static int firstInterAds
    {
        get { return PlayerPrefs.GetInt("firstInterAds", 0); }
        set { PlayerPrefs.SetInt("firstInterAds", value); }
    }

    public static int vibrationFx
    {
        get { return PlayerPrefs.GetInt("vibrationFx", 1); }
        set { PlayerPrefs.SetInt("vibrationFx", value); }
    }

    public static int crMapSelect
    {
        get { return PlayerPrefs.GetInt("crMapSelect", 1); }
        set { PlayerPrefs.SetInt("crMapSelect", value); }
    }

    public static int money_currency
    {
        get { return PlayerPrefs.GetInt("money_currency",0); }
        set {

            if (value <= 0)
                value = 0;

            PlayerPrefs.SetInt("money_currency", value);
            Action_Event_Change_Money();
        }
    }

    public static int ticket_currency
    {
        get { return PlayerPrefs.GetInt("ticket_currency", 0); }
        set
        {
            PlayerPrefs.SetInt("ticket_currency", value);
            Action_Event_Change_Money();
        }
    }


    public static float soundFx
    {
        get { return PlayerPrefs.GetFloat("sound_fx", 1); }
        set { PlayerPrefs.SetFloat("sound_fx", value); }
    }

    public static float musicFx
    {
        get { return PlayerPrefs.GetFloat("music_fx", 1); }
        set { PlayerPrefs.SetFloat("music_fx", value); }
    }

    public static int remove_ads
    {
        get { return PlayerPrefs.GetInt("remove_ads", 0); }
        set { PlayerPrefs.SetInt("remove_ads", value); }
    }

    public static string time_first_open
    {
        get { return PlayerPrefs.GetString("time_first_open", string.Empty); }
        set { PlayerPrefs.SetString("time_first_open", value); }
    }

    public static string time_offline
    {
        get { return PlayerPrefs.GetString("time_offline", string.Empty); }
        set { PlayerPrefs.SetString("time_offline", value); }
    }

    public static string datetime_first_open
    {
        get { return PlayerPrefs.GetString("datetime_first_open", string.Empty); }
        set { PlayerPrefs.SetString("datetime_first_open", value); }
    }

    public static int datetime_lastday
    {
        get { return PlayerPrefs.GetInt("datetime_lastday", -1); }
        set { PlayerPrefs.SetInt("datetime_lastday", value); }
    }

    public static int reward_free_turn
    {
        get { return PlayerPrefs.GetInt("reward_free_turn", 0); }
        set { PlayerPrefs.SetInt("reward_free_turn", value); }
    }

    public static string datetime_reward_free
    {
        get { return PlayerPrefs.GetString("datetime_reward_free", string.Empty); }
        set { PlayerPrefs.SetString("datetime_reward_free", value); }
    }

    #endregion

    #region Events

    public static event REFRESH Event_Change_Money;
    public static void Action_Event_Change_Money()
    {
        if (Event_Change_Money != null)
        {
            Event_Change_Money();
        }
    }

    //Delegate
    public static event Action Event_ChangeMap;
    public static void Action_ChangeMap()
    {
        if (Event_ChangeMap != null)
        {
            Event_ChangeMap();
        }

    }

    public static event Action Event_TutUpgrade;
    public static void Action_TutUpgrade()
    {
        if(Event_TutUpgrade!=null)
        {
            Event_TutUpgrade();
        }
    }

    public static event Action Event_NoticeChange;
    public static void Action_NoticeChange()
    {
        if (Event_NoticeChange != null)
        {
            Event_NoticeChange();
        }

    }

    public static event Action Event_ChangeSound;
    public static void Action_ChangeSound()
    {
        if (Event_ChangeSound != null)
        {
            Event_ChangeSound();
        }

    }

    public static event Action Event_ChangeMusic;
    public static void Action_ChangeMusic()
    {
        if (Event_ChangeMusic != null)
        {
            Event_ChangeMusic();
        }

    }


    public static event DataConfigs Event_RemoteConfigs;
    public static void Action_Event_RemoteConfigs(string _key, string _value)
    {
        if (Event_RemoteConfigs != null)
        {
            Event_RemoteConfigs(_key, _value);
        }
    }

    public static bool isCheatProcess = false;
    public static event MapProcess Event_MapPoint;
    public static void Action_Event_MapPoint(string _idUnit,int _point)
    {
        if (isCheatProcess)
            return;

        if (Event_MapPoint != null)
        {
            Event_MapPoint(_idUnit,_point);
        }
    }

    /*
    public static event ProductAddOn Event_Product_Special;
    public static void Action_Event_Product_Special(EProductType _type, int _value)
    {
        if (Event_Product_Special != null)
        {
            Event_Product_Special(_type,_value);
        }

    }

    public static event SpecialQuest Event_Special_Quest;
    public static void Action_Event_Special_Quest(SpecialData _data)
    {
        if (Event_Special_Quest != null)
        {
            Event_Special_Quest(_data);
        }
    }

    public static int timeCleanBot = 0;
    public static event TimeCountdown Event_CleanBot;
    public static void Action_Event_CleanBot(int _timeLeft)
    {
        if (Event_CleanBot != null)
        {
            timeCleanBot = _timeLeft;
            Event_CleanBot(_timeLeft);
        }
    }

    public static event SkinChange Event_SkinChange;
    public static void Action_Event_SkinChange(ETypeSkin _type)
    {
        if (Event_SkinChange != null)
        {
            Event_SkinChange(_type);
        }
    } 
     */
    

    #endregion

    #region Internet 
    public static bool isNetworking()
    {
        bool result = true;
        if (Application.internetReachability == NetworkReachability.NotReachable)
            result = false;
        return result;
    }

    #endregion

    #region Haptic
#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject vibrator;
#endif

    public static void Vibrate(long milliseconds = 25)
    {
        if (vibrationFx == 0)
            return;

        if (IsAndroid())
        {
            vibrator.Call("vibrate", milliseconds);
            Debug.Log("vibrate andoroid: " + milliseconds);
        }
        else
        {
            // Handheld.Vibrate();
            Debug.Log("vibrate else: " + milliseconds);
        }
    }

    public static void LowVibrate()
    {
        Vibrate(35);
    }

    public static void MediumVibrate()
    {
        Vibrate(50);
    }

    public static void HardVibrate()
    {
        Vibrate(100);
    }

    public static void Cancel()
    {
        if (IsAndroid())
        {
            vibrator.Call("cancel");
        }
    }

    public static bool IsAndroid()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return true;
#else
        return false;
#endif
    }
    #endregion

    #region Random

    private static System.Random mRandom = new System.Random();
    public static int EasyRandom(int range)
    {
        return mRandom.Next(range);
    }

    public static int EasyRandom(int min, int max)//không bao gồm max
    {
        return mRandom.Next(min, max);
    }

    public static float EasyRandom(float min, float max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    #endregion

    #region Convert
    public static string TimestampNow()
    {
        return DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
    }

     // Chuyển số thành text
    public static string NumberCustomToString(float _number)
    {
        string str = "";
        if (_number < 10000)
            str = _number.ToString("00");
        else if (10000 <= _number && _number < 1000000)
            str = (_number / 1000).ToString("0.#") + "K";
        else if (1000000 <= _number && _number < 1000000000)
            str = (_number / 1000000).ToString("0.##") + "M";
        else
            str = (_number / 1000000000).ToString("0.##") + "B";
        return str;
    }

    //Chuyển time s => form
    public static string SecondCustomToTime(int _second)
    {
        string str = "";
        int second = 0, minute = 0, hour = 0;
        
        second = _second % 60;
        if (second > 59) second = 59;
        minute = (int)(Mathf.Floor(_second / 60) % 60);
        hour = (int)(_second / 3600);

        if (hour > 0) str += hour.ToString("00") + "h";
        if (minute >= 0) str += minute.ToString("00") + "m";
        if (_second < 3600) str += second.ToString("00") + "s";

        return str;
    }
    public static string SecondCustomToTime2(int _second)
    {
        string str = "00:00";
        int second = 0;
        int minute = 0;
        //int hour = 0;
        second = _second % 60;
        if (second > 59) second = 59;
        minute = (int)(Mathf.Floor(_second / 60) % 60);
        //hour = (int)(_second / 3600);


        str =/* hour.ToString("00") + ":" +*/ minute.ToString("00") + ":" + second.ToString("00");
        return str;
    }

    #endregion

    #region Scenes Change
    public static void LoadScene(int _index) 
    {
        //Debug.LogError("Loadscene " + _index);

        Action_ChangeMap();
        // timeCleanBot = 0;
        crMapSelect = _index;
        SceneManager.LoadSceneAsync(_index);

    }

    #endregion

    #region Match Format
    public static int money_idle_offline(int _time,int _progress)
    {
        int money = 0;

        return money;
    }

    #endregion
}

public delegate void REFRESH();
public delegate void MapProcess(string _idUnit,int _point);
public delegate void DataConfigs(string _key, string _value);

/*
public delegate void ProductAddOn(EProductType _type,int _value);

public delegate void SpecialQuest(SpecialData data);
public delegate void TimeCountdown(int _timeLeft);
public delegate void SkinChange(ETypeSkin _type);
*/