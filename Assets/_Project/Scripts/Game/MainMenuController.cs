using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Control the UI flow of the MainMenu
namespace Game
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject _mainScreen;
        [SerializeField] private GameObject _joinScreen;


        [SerializeField] private Button _hostButton;
        [SerializeField] private Button _joinButton;
        [SerializeField] private Button _reJoinButton;
        [SerializeField] private Button _leaveButton;

        [SerializeField] private Button _submitCodeButton;
        [SerializeField] private TextMeshProUGUI _codeText;

        private void OnEnable()
        {
            _hostButton.onClick.AddListener(OnHostClicked);
            _joinButton.onClick.AddListener(OnJoinClicked);
            _reJoinButton.onClick.AddListener(OnRejoinClicked);
            _leaveButton.onClick.AddListener(OnLeaveLobbyClicked);
            _submitCodeButton.onClick.AddListener(OnSubmitCodeClicked);
        }

        private void OnDisable()
        {
            _hostButton.onClick.RemoveListener(OnHostClicked);
            _joinButton.onClick.RemoveListener(OnJoinClicked);
            _submitCodeButton.onClick.RemoveListener(OnSubmitCodeClicked);
        }

        private async void Start()
        {
            //GameLobbyManger gameobject is created after this line
            if (await GameLobbyManager.Instance.HasActiveLobbies())
            {
                _hostButton.gameObject.SetActive(false);
                _joinButton.gameObject.SetActive(false);
                
                _reJoinButton.gameObject.SetActive(true);
                _leaveButton.gameObject.SetActive(true);
            }
        }

        private async void OnHostClicked()
        {
            bool succeeded = await GameLobbyManager.Instance.CreateLobby();

            //If lobby is created successfully => send the player to the lobby-scene
            if (succeeded)
            {
                SceneManager.LoadSceneAsync("Lobby");
            }
        }

        private void OnJoinClicked()
        {
            Debug.Log("Join clicked");
            _mainScreen.SetActive(false);
            _joinScreen.SetActive(true);

            // GameLobbyManager.Instance.JoinLobby();
        }

        private async void OnSubmitCodeClicked()
        {
            string code = _codeText.text;
            code = code.Substring(0, code.Length - 1); //remove the last character (the 'enter' character)

            bool succeeded = await GameLobbyManager.Instance.JoinLobby(code);
            if (succeeded)
            {
                SceneManager.LoadSceneAsync("Lobby");
            }

            Debug.Log($"code = {code}");
        }
        
        private async void OnRejoinClicked()
        {
            bool succeeded = await GameLobbyManager.Instance.RejoinGame();
            if (succeeded)
            {
                SceneManager.LoadSceneAsync("Lobby");
            }
        }
        
        private async void OnLeaveLobbyClicked()
        {
            bool succeeded = await GameLobbyManager.Instance.LeaveAllLobbies();

            if ( succeeded)
            {
                Debug.Log("Left all lobbies");
                SceneManager.LoadSceneAsync("MainMenu");
            }
        }
    }
}
