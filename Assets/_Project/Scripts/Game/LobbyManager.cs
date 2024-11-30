using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameFramework.Core;
using GameFramework.Events;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace Game
{
    public class LobbyManager : Singleton<LobbyManager>
    {
        /// <summary>
        /// The current Lobby
        /// </summary>
        private Lobby _lobby;
        private Coroutine _heartBeatCoroutine;
        private Coroutine _refreshLobbyCoroutine;
        private List<string> _joinedLobbiesId;
        
        public async Task<bool> HasActiveLobbies()
        {
            //asynchronously requests a list of lobbies the player has joined from a server - LobbyService
            _joinedLobbiesId = await LobbyService.Instance.GetJoinedLobbiesAsync();

            if (_joinedLobbiesId.Count > 0)
                return true;
            return false;
        }


        #region GETTERS

        public string GetLobbyCode()
        {
            return _lobby?.LobbyCode;
        }

        public string GetHostId()
        {
            return _lobby?.HostId;
        }

        #endregion

        public async Task<bool> CreateLobby(int maxPlayers, bool isPrivate, Dictionary<string, string> data, Dictionary<string, string> lobbyData)
        {
            Dictionary<string, PlayerDataObject> playerData = SerializePlayerData(data);
            
            //Create a new player with the given data
            Player player = new Player(AuthenticationService.Instance.PlayerId, null, playerData);
            
            //Optional parameter class for Lobby creation requests
            CreateLobbyOptions options = new CreateLobbyOptions()
            {
                Data = SerializeLobbyData(lobbyData),
                IsPrivate = isPrivate,
                Player = player
            };

            try
            {
                //Create the lobby
                _lobby = await LobbyService.Instance.CreateLobbyAsync("Lobby", maxPlayers, options);
            }
            catch (Exception)
            {
                return false;
            }

            _heartBeatCoroutine = StartCoroutine(HeartBeatLobbyCoroutine(_lobby.Id, 6f));
            _refreshLobbyCoroutine = StartCoroutine(RefreshLobbyCoroutine(_lobby.Id, 1f));

            Debug.Log($"Lobby created with lobby id: {_lobby.Id}");
            Debug.Log($"Lobby code: {_lobby.LobbyCode}");
            return true;
        }

        private IEnumerator HeartBeatLobbyCoroutine(string lobbyId, float waitTimeSeconds)
        {
            while (true)
            {
                Debug.Log($"HeartBeat: ");
                LobbyService.Instance.SendHeartbeatPingAsync(lobbyId);
                yield return new WaitForSecondsRealtime(waitTimeSeconds);
            }
        }

        private IEnumerator RefreshLobbyCoroutine(string lobbyId, float waitTimeSeconds)
        {
            while (true)
            {
                //Retrieve information about a specific lobby identified by lobbyId
                Task<Lobby> task = LobbyService.Instance.GetLobbyAsync(lobbyId);
                yield return new WaitUntil(() => task.IsCompleted); //wait until the task (refresh Lobby) is complete

                Lobby newLobby = task.Result;
                //Check if the "newLobby" is actually new
                if (newLobby.LastUpdated > _lobby.LastUpdated)
                {
                    _lobby = newLobby;
                    LobbyEvents.OnLobbyUpdated?.Invoke(_lobby);
                }

                yield return new WaitForSecondsRealtime(waitTimeSeconds);
            }
        }

        private Dictionary<string, PlayerDataObject> SerializePlayerData(Dictionary<string, string> data)
        {
            Dictionary<string, PlayerDataObject> playerData = new Dictionary<string, PlayerDataObject>();

            foreach (var (key, value) in data)
            {
                playerData.Add(key, new PlayerDataObject(
                    visibility: PlayerDataObject.VisibilityOptions.Member, //Visible only to member of the lobby
                    value: value));
            }

            return playerData;
        }
        
        private Dictionary<string, DataObject> SerializeLobbyData(Dictionary<string, string> data)
        {
            Dictionary<string, DataObject> lobbyData = new Dictionary<string, DataObject>();

            foreach (var (key, value) in data)
            {
                lobbyData.Add(key, new DataObject(
                    visibility: DataObject.VisibilityOptions.Member, //Visible only to member of the lobby
                    value: value));
            }

            return lobbyData;
        }

        public void OnApplicationQuit()
        {
            //When the host quit application => the lobby is deleted
            if (_lobby != null && _lobby.HostId == AuthenticationService.Instance.PlayerId)
            {
                LobbyService.Instance.DeleteLobbyAsync(_lobby.Id);
            }
        }


        public async Task<bool> JoinLobby(string code, Dictionary<string, string> playerData)
        {
            JoinLobbyByCodeOptions options = new JoinLobbyByCodeOptions();
            Player player = new Player(AuthenticationService.Instance.PlayerId, null, SerializePlayerData(playerData));

            options.Player = player;
            try
            {
                _lobby = await LobbyService.Instance.JoinLobbyByCodeAsync(code, options);
            }
            catch (Exception)
            {
                return false;
            }

            _refreshLobbyCoroutine = StartCoroutine(RefreshLobbyCoroutine(_lobby.Id, 1f));
            return true;
        }

        /// <summary>
        /// Get all player's datas in the lobby
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, PlayerDataObject>> GetPlayerData()
        {
            List<Dictionary<string, PlayerDataObject>> data = new List<Dictionary<string, PlayerDataObject>>();
            foreach (Player player in _lobby.Players)
            {
                data.Add(player.Data);
            }


            return data;
        }

        public async Task<bool> UpdatePlayerData(string playerId, Dictionary<string, string> data, string allocationId = default, string connectionData = default)
        {
            Dictionary<string, PlayerDataObject> playerData = SerializePlayerData(data);
            UpdatePlayerOptions options = new UpdatePlayerOptions()
            {
                Data = playerData
            };

            try
            {
                _lobby = await LobbyService.Instance.UpdatePlayerAsync(_lobby.Id, playerId, options);
            }
            catch (System.Exception)
            {
                return false;
            }

            //Tells everybody that there was a new lobby
            LobbyEvents.OnLobbyUpdated(_lobby);
            return true;
        }

        public async Task<bool> UpdateLobbyData(Dictionary<string, string> data)
        {
            Dictionary<string, DataObject> lobbyData = SerializeLobbyData(data);

            UpdateLobbyOptions options = new UpdateLobbyOptions()
            {
                Data = lobbyData
            };

            try
            {
                _lobby = await LobbyService.Instance.UpdateLobbyAsync(_lobby.Id, options);
            }
            catch (System.Exception)
            {
                return false;
            }

            //LobbyEvent in the Game Framework
            LobbyEvents.OnLobbyUpdated(_lobby); 
            return true;
        }
        

        public async Task<bool> RejoinLobby()
        {
            try
            {
                _lobby = await LobbyService.Instance.ReconnectToLobbyAsync(_joinedLobbiesId[0]);
                
                //Game framework events
                LobbyEvents.OnLobbyUpdated(_lobby);
            }
            catch (System.Exception)
            {
                return false;
            }

            _refreshLobbyCoroutine = StartCoroutine(RefreshLobbyCoroutine(_joinedLobbiesId[0], 1f));
            return true;
        }

        public async Task<bool> LeaveAllLobbies()
        {
            string playerId = AuthenticationService.Instance.PlayerId;
            foreach (string lobbyId in _joinedLobbiesId)
            {
                try
                {
                    await LobbyService.Instance.RemovePlayerAsync(lobbyId, playerId);
                }
                catch (System.Exception)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
