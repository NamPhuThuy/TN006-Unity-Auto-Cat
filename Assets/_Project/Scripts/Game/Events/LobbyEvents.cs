using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Events
{
    public static class LobbyEvents
    {
        public delegate void LobbyUpdated();

        public static LobbyUpdated OnLobbyUpdated;

        public delegate void LobbyReady();

        public static LobbyReady OnLobbyReady;
        
        
    }
}
