using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace EditorExtend
{
    public class MenuExtend : MonoBehaviour
    {
        const string SWITCH_SCENCE_MENU_NAME = "Tools/Switch Scene";
        private const string PATH_TO_SCENES_FOLDER = "Assets/_Project/Scenes/";

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

        [MenuItem(SWITCH_SCENCE_MENU_NAME + "/Lobby " + ALT + "3")]
        static void Lobby()
        {
            LoadSceneByName("Lobby");
        }

        [MenuItem(SWITCH_SCENCE_MENU_NAME + "/GamePlay " + ALT + "4")]
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


        [MenuItem("Tools/Remove All Data")]
        static void RemoveAllData()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }

        #endregion

#endif
    }
}