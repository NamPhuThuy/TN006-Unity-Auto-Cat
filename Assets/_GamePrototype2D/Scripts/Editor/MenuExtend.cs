
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor.PackageManager.UI;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
#endif

public class MenuExtend : MonoBehaviour
{
    public const string BUILD_VERSIONNAME = "0.0.1";
    public const int BUILD_VERSIONCODE = 001;

    const string BUILD_PASSWORD = "";
    public static readonly string[] DEFINE_BUILD = { /*"UNITY_IAP", "USE_DOTWEEN",
        "ACTIVE_FIREBASE", "ACTIVE_FIREBASE_REMOTE","ACTIVE_FIREBASE_CRASHLYTICS","ACTIVE_FIREBASE_ANALYTICS",
        "ACTIVE_FACEBOOK","ACTIVE_IRONSOURCE" ,"ODIN_INSPECTOR","TUTORIAL","SPINE_SKIP"*/};
    const string DEFINE_UNLOCKALL = "DEVELOPMENT";

    const string BUILD_NAME_DEV = "zName_{0}_dev";
    const string BUILD_NAME_FINAL = "zName_{0}_product_signed";

    const string SWITCH_SCENCE_MENU_NAME = "Tools/Switch Scene";
    private const string PATH_TO_SCENES_FOLDER = "Assets/_GamePrototype2D/Scenes/";

    private const string ALT = "&";
    private const string SHIFT = "#";
    private const string CTRL = "%";


    
#if UNITY_EDITOR
    #region Setup Editor
    [MenuItem(SWITCH_SCENCE_MENU_NAME + "/Bootstrap " + ALT + "1")]
    // [MenuItem(SWITCH_SCENCE_MENU_NAME + "/Intro &1")]
    
    static void Boots()
    {
        LoadSceneByName("Bootstrap");
    }


    [MenuItem(SWITCH_SCENCE_MENU_NAME + "/MainMenu " + ALT + "2")]
    static void MainMenu()
    {
        LoadSceneByName("MainMenu");

    }
    
    [MenuItem(SWITCH_SCENCE_MENU_NAME + "/GamePlay " + ALT + "3")]
    static void Gameplay()
    {
        LoadSceneByName("GamePlay");

    }

    static void LoadSceneByName(string _nameScene)
    {
        // EditorApplication.SaveCurrentSceneIfUserWantsTo();
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene($"{PATH_TO_SCENES_FOLDER}{_nameScene}.unity");
    }


    //=====================
    [MenuItem(SWITCH_SCENCE_MENU_NAME + "/Config")]
    static void BuildCongfig()
    {
        PlayerSettings.keystorePass = BUILD_PASSWORD;
        PlayerSettings.keyaliasPass = BUILD_PASSWORD;
        //PlayerSettings.bundleVersion = BUILD_VERSIONNAME;
        //PlayerSettings.Android.bundleVersionCode = BUILD_VERSIONCODE;
        //PlayerSettings.iOS.buildNumber = BUILD_VERSIONCODE.ToString();
        Debug.Log("Config"
           + "\nPassword=  " + PlayerSettings.keystorePass
            + "\nVersion Name=  " + PlayerSettings.bundleVersion
            + "\nVersion Code=  " + PlayerSettings.Android.bundleVersionCode);
        //  SetupBuild(false, false);
        //EditorUserBuildSettings.activeScriptCompilationDefines=_script.ToArray();
    }

    //[MenuItem(MENU_NAME + "/Build/Config/ConfigDev")]
    static void BuildCongfigDev()
    {
        PlayerSettings.keystorePass = BUILD_PASSWORD;
        PlayerSettings.keyaliasPass = BUILD_PASSWORD;
        PlayerSettings.bundleVersion = BUILD_VERSIONNAME;
        PlayerSettings.Android.bundleVersionCode = BUILD_VERSIONCODE;

        Debug.Log("Config"
           + "\nPassword=  " + PlayerSettings.keystorePass
            + "\nVersion Name=  " + PlayerSettings.bundleVersion
            + "\nVersion Code=  " + PlayerSettings.Android.bundleVersionCode);
        ActiveDevOption(true);
    }
    
    //[MenuItem(MENU_NAME + "/Build/Config/ConfigNonDev")]
    static void BuildCongfigNonDev()
    {
        PlayerSettings.keystorePass = BUILD_PASSWORD;
        PlayerSettings.keyaliasPass = BUILD_PASSWORD;
        PlayerSettings.bundleVersion = BUILD_VERSIONNAME;
        PlayerSettings.Android.bundleVersionCode = BUILD_VERSIONCODE;
        Debug.Log("Config"
           + "\nPassword=  " + PlayerSettings.keystorePass
            + "\nVersion Name=  " + PlayerSettings.bundleVersion
            + "\nVersion Code=  " + PlayerSettings.Android.bundleVersionCode);
        ActiveDevOption(false);
    }

    //[MenuItem(MENU_NAME + "/Build/Build/Build Apk-Dev " + BUILD_VERSIONNAME)]
    public static void AutoBuildAPKDEV()
    {
        Debug.Log("Building APK-DEV...");
        SetupBuild(false, true);
    }
    //[MenuItem(MENU_NAME + "/Build/Build/Build Final-apk " + BUILD_VERSIONNAME)]
    public static void AutoBuildApk()
    {
        Debug.Log("Building apk-Final...");
        SetupBuild(false, false);
    }
    //[MenuItem(MENU_NAME + "/Build/Build/Build Final-aab " + BUILD_VERSIONNAME)]
    public static void AutoBuildaab()
    {
        Debug.Log("Building aab-Final...");
        SetupBuild(true, false);
    }
    [MenuItem(SWITCH_SCENCE_MENU_NAME + "/Build/Build/Build ALL " + BUILD_VERSIONNAME)]
    public static void AutoBuilAll()
    {
        Debug.Log("Building ALL...");
        Debug.Log("Building APK-DEV...");
        SetupBuild(false, true, () =>
        {
            Debug.Log("Building apk-Final...");
            SetupBuild(false, false, () =>
            {
                Debug.Log("Building aab-Final...");
                SetupBuild(true, false);
            });
        });
    }
    static void ActiveDevOption(bool buildDev)
    {
        //string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
        //List<string> _script = definesString.Split(';').ToList();

        List<string> _script = new List<string>(DEFINE_BUILD);
        if (buildDev)
        {
            if (!_script.Contains(DEFINE_UNLOCKALL))
            {
                _script.Add(DEFINE_UNLOCKALL);
                Debug.Log("Add Build " + DEFINE_UNLOCKALL);
            }
#if UNITY_2020_1_11
			string definesString = "";
			foreach (var item in _script)
			{
				definesString += item + ";";
			}
			PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, definesString);
			PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, definesString);
#else
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, _script.ToArray());
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, _script.ToArray());
#endif

        }
        else
        {
            if (_script.Contains(DEFINE_UNLOCKALL))
            {
                _script.Remove(DEFINE_UNLOCKALL);
                Debug.Log("Remove Build " + DEFINE_UNLOCKALL);
            }
#if UNITY_2020_1_11
			string definesString = "";
			foreach (var item in _script)
			{
				definesString += item + ";";
			}
			PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, definesString);
			PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, definesString);
#else
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, _script.ToArray());
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, _script.ToArray());
#endif
        }
        Debug.Log("Build Define: " + _script.ToString());

    }
    
    static void SetupBuild(bool AppBundle = true, bool buildDev = true, System.Action OnBuildDone = null)
    {
        BuildCongfig();
        var scenes = EditorBuildSettings.scenes.Where(s => s.enabled).Select(s => s.path).ToArray();
        //EditorBuildSettings.
        BuildOptions bo = BuildOptions.None;
        //if (buildDev)
        //    bo = BuildOptions.Development;

        AndroidArchitecture aac = AndroidArchitecture.ARM64 | AndroidArchitecture.ARMv7;

        // EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
        // PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);

        PlayerSettings.Android.targetArchitectures = aac;
        EditorUserBuildSettings.buildAppBundle = AppBundle;


        ActiveDevOption(buildDev);

        string BUILD_PATH = "E:\\GameAPK";

        string fileName = buildDev ? BUILD_NAME_DEV : BUILD_NAME_FINAL;
        fileName = string.Format(fileName, BUILD_VERSIONNAME);
        string path = BUILD_PATH + fileName;
        path += AppBundle ? ".aab" : ".apk";
        BuildReport report = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, path, BuildTarget.Android, bo);

        BuildSummary summary = report.summary;
        Debug.Log("Building...");
        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded:\nFile Name:" + summary.outputPath
                + "\nVersion Name" + PlayerSettings.bundleVersion
                 + "\nVersion Code" + PlayerSettings.Android.bundleVersionCode
                + "\nFile Size:" + summary.totalSize / 1024 / 1024 + " MB\nGood Luck");
            if (OnBuildDone != null)
                OnBuildDone();
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
        if (summary.result == BuildResult.Succeeded)
            OpenFolderInWin(BUILD_PATH);

    }

    static string[] GetScenePaths()
    {
        string[] scenes = new string[EditorBuildSettings.scenes.Length];
        for (int i = 0; i < scenes.Length; i++)
        {
            scenes[i] = EditorBuildSettings.scenes[i].path;
        }
        return scenes;
    }


    [MenuItem("Tools/Remove All Data")]
    static void RemoveAllData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
    #endregion

    #region Extension
    public static void OpenFolderInWin(string path)
    {
        bool openInsidesOfFolder = false;

        // try windows
        string winPath = path.Replace("/", "\\"); // windows explorer doesn't like forward slashes

        if (System.IO.Directory.Exists(winPath)) // if path requested is a folder, automatically open insides of that folder
        {
            openInsidesOfFolder = true;
        }

        try
        {
            System.Diagnostics.Process.Start("explorer.exe", (openInsidesOfFolder ? "/root," : "/select,") + winPath);
        }
        catch (System.ComponentModel.Win32Exception e)
        {
            // tried to open win explorer in mac
            // just silently skip error
            // we currently have no platform define for the current OS we are in, so we resort to this
            e.HelpLink = ""; // do anything with this variable to silence warning about not using it
        }
    }
    #endregion

#endif
}