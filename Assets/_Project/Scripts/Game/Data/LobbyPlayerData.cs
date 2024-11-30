using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using UnityEngine;


namespace Game.Data
{
    public class LobbyPlayerData : MonoBehaviour
    {
        private string _id;
        private string _gamerTag;
        private bool _isReady; //does the player is ready to start the game?
    
        public string Id
        {
            get => _id;
            // set => _id = value;
        }
    
        public string GamerTag
        {
            get => _gamerTag;
            // set => _gamerTag = value;
        }
    
        public bool IsReady
        {
            get => _isReady;
            set => _isReady = value;
        }
    
        public void Initialize(string id, string gamerTag)
        {
            _id = id;
            _gamerTag = gamerTag;
        }
    
        public void Initialize(Dictionary<string, PlayerDataObject> playerData)
        {
            UpdateState(playerData);
        }
    
        public void UpdateState(Dictionary<string, PlayerDataObject> playerData)
        {
            if (playerData.ContainsKey("Id"))
            {
                _id = playerData["Id"].Value;
            }
            
            if (playerData.ContainsKey("GamerTag"))
            {
                _gamerTag = playerData["GamerTag"].Value;
            }
            
            if (playerData.ContainsKey("IsReady"))
            {
                _isReady = playerData["IsReady"].Value == "True";
            }
        }
    
        public Dictionary<string, string> Serialize()
        {
            return new Dictionary<string, string>()
            {
                {"Id", _id },
                {"GamerTag", _gamerTag },
                {"IsReady", _isReady.ToString()} //True or False
            };
        }
        
        
        void Start()
        {
            
        }
    
        // Update is called once per frame
        void Update()
        {
            
        }
    }    
}

