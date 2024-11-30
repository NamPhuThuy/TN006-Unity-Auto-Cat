using Game.Data;
using Game.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LobbyUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _lobbyCodeText;
        
        [Header("Ready Indicator")]
        [SerializeField] private Button _readyButton;
        [SerializeField] private Button _startButton;
        
        [Header("Map Selection")]
        [SerializeField] private Image _mapImage;
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;
        [SerializeField] private TextMeshProUGUI _mapName;
        [SerializeField] private MapSelectionData _mapSelectionData;
        private int _currentMapIndex = 0;


        private void OnEnable()
        {
            if (GameLobbyManager.Instance.IsHost)
            {
                _leftButton.onClick.AddListener(OnLeftButtonClick);
                _rightButton.onClick.AddListener(OnRightButtonClick);
                _startButton.onClick.AddListener(OnStartButtonClick);
                Events.LobbyEvents.OnLobbyReady += OnLobbyReady;
            }
            
            _readyButton.onClick.AddListener(OnReadyPressed);

            LobbyEvents.OnLobbyUpdated += OnLobbyUpdated;

            //Make sure when we go back to the lobby, the cursor will appear again 
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        private void OnDisable()
        {
            _rightButton.onClick.RemoveAllListeners();
            _leftButton.onClick.RemoveAllListeners();
            _startButton.onClick.RemoveAllListeners();
            _readyButton.onClick.RemoveAllListeners();
            
            LobbyEvents.OnLobbyUpdated -= OnLobbyUpdated;
            Events.LobbyEvents.OnLobbyReady -= OnLobbyReady;
        }

        void Start()
        {
            _lobbyCodeText.text = $"Lobby code: {GameLobbyManager.Instance.GetLobbyCode()}";
            if (!GameLobbyManager.Instance.IsHost)
            {
                _leftButton.gameObject.SetActive(false);
                _rightButton.gameObject.SetActive(false);
            }
            else
            {
                GameLobbyManager.Instance.SetSelectedMap(_currentMapIndex);
            }
        }

        private async void OnLeftButtonClick()
        {
            if (_currentMapIndex - 1 > 0)
            {
                _currentMapIndex--;
            }
            else
            {
                _currentMapIndex = _mapSelectionData.maps.Count - 1;
            }

            UpdateMapThumbnail();
            GameLobbyManager.Instance.SetSelectedMap(_currentMapIndex);
        }

        private async void OnRightButtonClick()
        {
            if (_currentMapIndex + 1 < _mapSelectionData.maps.Count - 1)
            {
                _currentMapIndex++;
            }
            else
            {
                _currentMapIndex = 0;
            }

            UpdateMapThumbnail();
            GameLobbyManager.Instance.SetSelectedMap(_currentMapIndex);
        }
        
        private async void OnStartButtonClick()
        {
            await GameLobbyManager.Instance.StartGame();
        }

        private async void OnReadyPressed()
        {
            bool succeeded = await GameLobbyManager.Instance.SetPlayerReady();
            if (succeeded)
            {
                _readyButton.gameObject.SetActive(false);
            }
        }
        
        private void UpdateMapThumbnail()
        {
            _mapImage.color = _mapSelectionData.maps[_currentMapIndex].mapThumbnail;
            _mapName.text = _mapSelectionData.maps[_currentMapIndex].mapName;
        }
        
        private void OnLobbyUpdated()
        {
            _currentMapIndex = GameLobbyManager.Instance.GetMapIndex();
            UpdateMapThumbnail();
        }
        
        private void OnLobbyReady()
        {
            _startButton.gameObject.SetActive(true);
        }

        
        
    }
}
