using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using GameFramwork.Manager;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        private void OnEnable()
        {
            //Make sure the NetworkManager is initialized
            StartCoroutine(AssignEvent());
        }

        IEnumerator AssignEvent()
        {
            yield return new WaitForSeconds(0.5f);

            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;

            /*
         These two lines work together to maximize the amount of logging information provided by the network system. By setting the LogLevel to Developer and enabling network logs, developers gain access to extensive debugging information about network activity, which is crucial for identifying and resolving network-related issues during development. For a released game, these settings would typically be changed to a less verbose level (e.g., Info or Warning) to avoid overwhelming the console with unnecessary data and potentially exposing sensitive information.
         */
            NetworkManager.Singleton.LogLevel = LogLevel.Developer;
            NetworkManager.Singleton.NetworkConfig.EnableNetworkLogs = true;

        }

        private void OnDisable()
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnected;
        }

        void Start()
        {
            //--MAKE BOTH PLAYERS SPAWN IN THE SAME SCENE
            // Disable the connection approval process. Any client that attempts to connect will be immediately accepted, This is generally suitable for simpler games or situations where you don't need to control which clients can join
            NetworkManager.Singleton.NetworkConfig.ConnectionApproval = true;

            //If you are the Host
            if (RelayManager.Instance.IsHost)
            {
                /*
             * .ConnectionApprovalCallback: A property of the Network Manager, It's a delegate (a type that represents a reference to a method) that holds a function to be called whenever a client attempts to connect to the server
             */
                NetworkManager.Singleton.ConnectionApprovalCallback = ConnectionApproval;
                (byte[] allocationId, byte[] key, byte[] connectionData, string ip, int port) =
                    RelayManager.Instance.GetHostConnectionInfo();
                NetworkManager.Singleton.GetComponent<UnityTransport>()
                    .SetHostRelayData(ip, (ushort)port, allocationId, key, connectionData, true);
                NetworkManager.Singleton.StartHost();
            }
            else
            {
                (byte[] allocationId, byte[] key, byte[] connectionData, byte[] hostConnectionData, string ip,
                        int port) =
                    RelayManager.Instance.GetClientConnectionInfo();
                NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(ip, (ushort)port,
                    allocationId, key, connectionData, hostConnectionData, true);
                NetworkManager.Singleton.StartClient();
            }
        }

        private void Update()
        {

            //If we are losing the connection to the server
            if (NetworkManager.Singleton.ShutdownInProgress)
            {
                GameLobbyManager.Instance.GoBackToLobby(true);
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj">networkManager id, example: 0, 1, 2,..</param> 
        /// <exception cref="NotImplementedException"></exception>
        private void OnClientDisconnected(ulong clientId)
        {
            if (NetworkManager.Singleton.LocalClientId == clientId)
            {
                Debug.Log($"I'm not connected anymore");
                NetworkManager.Singleton.Shutdown();
                SceneManager.LoadSceneAsync("MainMenu");
            }
        }

        private void OnClientConnected(ulong obj)
        {
            Debug.Log($"Player connected: {obj}");
        }

        private void ConnectionApproval(NetworkManager.ConnectionApprovalRequest request,
            NetworkManager.ConnectionApprovalResponse response)
        {
            response.Approved = true; //Indicates that server accepts the connection request.
            response.CreatePlayerObject =
                true; //Tells the Network Manager to automatically create a player object (a prefab from your Network Prefab List) for this client on the server. This is typically what you want for a multiplayer game.
            response.Pending =
                false; //Indicates that the connection approval process is complete, and the client's connection status is no longer pending.
        }
    }
}
