using System.Collections;
using System.Collections.Generic;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasMainMenu : CanvasBase
{
        [SerializeField] private GameObject _joinScreen;


        [SerializeField] private Button _hostButton;
        [SerializeField] private Button _joinButton;
        [SerializeField] private Button _reJoinButton;
        [SerializeField] private Button _leaveButton;

        [SerializeField] private Button _submitCodeButton;
        [SerializeField] private TextMeshProUGUI _codeText;
    
    
    public override void Show(object data = null)
    {
        base.Show(data);
        
        _hostButton.onClick.AddListener(OnHostClicked);
        _joinButton.onClick.AddListener(OnJoinClicked);
        _reJoinButton.onClick.AddListener(OnRejoinClicked);
        _leaveButton.onClick.AddListener(OnLeaveLobbyClicked);
        _submitCodeButton.onClick.AddListener(OnSubmitCodeClicked);
    }

    public override void Hide()
    {
        base.Hide();
        _hostButton.onClick.RemoveListener(OnHostClicked);
        _joinButton.onClick.RemoveListener(OnJoinClicked);
        _submitCodeButton.onClick.RemoveListener(OnSubmitCodeClicked);
    }
    
    private async void Start()
    {
        _hostButton.gameObject.SetActive(true);
        _joinButton.gameObject.SetActive(false);
        
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
            // SceneManager.LoadSceneAsync("Lobby");
            SceneController.Instance.LoadScene("Lobby", true, true);
        }
    }

    private void OnJoinClicked()
    {
        Debug.Log("Join clicked");
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
            // SceneManager.LoadSceneAsync("Lobby");
            SceneController.Instance.LoadScene("Lobby", true, true);
        }

        Debug.Log($"code = {code}");
    }
    
    private async void OnRejoinClicked()
    {
        bool succeeded = await GameLobbyManager.Instance.RejoinGame();
        if (succeeded)
        {
            // SceneManager.LoadSceneAsync("Lobby");
            SceneController.Instance.LoadScene("Lobby", true, true);
        }
    }
    
    private async void OnLeaveLobbyClicked()
    {
        bool succeeded = await GameLobbyManager.Instance.LeaveAllLobbies();

        if ( succeeded)
        {
            Debug.Log("Left all lobbies");
            SceneController.Instance.LoadScene("MainMenu", true, true);
        }
    }
}
