using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Helpers
{
    public class HelperButton : MonoBehaviour
    {
        public void LoadScene(int id)
        {
            SceneManager.LoadScene(id);
        }

        public void QuitApp()
        {
            Application.Quit();
        }
    }
}
