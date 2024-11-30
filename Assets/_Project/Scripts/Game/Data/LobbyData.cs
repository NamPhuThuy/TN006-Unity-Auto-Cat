

using System;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace Game.Data
{
    public class LobbyData
    {
        //
        private string _relayJoinCode;
        

        public string RelayJoinCode
        {
            get => _relayJoinCode; 
            set => _relayJoinCode = value;
        }

        public void Initialize()
        {
            
        }

        public void Initialize(Dictionary<string, DataObject> lobbyData)
        {
            UpdateState(lobbyData);
        }

        public void UpdateState(Dictionary<string, DataObject> lobbyData)
        {
            if (lobbyData.ContainsKey("RelayJoinCode"))
            {
                _relayJoinCode = lobbyData["RelayJoinCode"].Value;
            }
        }

        public Dictionary<string, string> Serialize()
        {
            return new Dictionary<string, string>
            {
                {"RelayJoinCode", _relayJoinCode},
            };
        }
    }
    
    
}