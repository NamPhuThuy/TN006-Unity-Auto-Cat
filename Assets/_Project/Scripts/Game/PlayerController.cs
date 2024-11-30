using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Game;
using Game.Enviroment;
using GameFramework.Network.Movement;
using Unity.Netcode;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : NetworkBehaviour
    {
        /*[SerializeField] private float _speed;
        [SerializeField] private float _turnSpeed;*/
        [SerializeField] private Vector2 _minMaxRotationX = new Vector2(90f, -90f);
        [SerializeField] private Transform _camTransform;
        [SerializeField] private NetworkMovementComponent _playerMovement;

        [Header("Networked Raycast")]
        [SerializeField] private float _interactDistance;
        [SerializeField] private LayerMask _interactLayers;

        private CharacterController _cc;
        private PlayerControl _playerControl;
        private float _cameraAngle;

        public override void OnNetworkSpawn()
        {
            CinemachineVirtualCamera cvm = _camTransform.gameObject.GetComponent<CinemachineVirtualCamera>();

            /*
             - Ensures that the Cinemachine Virtual Camera associated with _camTransform has a high priority (1) only if the current player is its owner. If the player is not the owner, the camera's priority is set to 0
              
            - If this player die (the camera is destroy), they will see what other team-mate see. Because, other camera in the scene is set Priority to '0' (lower than the owner camera)
             */
            if (IsOwner) 
            {
                cvm.Priority = 1;
            }
            else
            {
                cvm.Priority = 0;
            }
            
        }

        void Start()
        {
            _cc = GetComponent<CharacterController>();
            _playerControl = new PlayerControl();
            _playerControl.Enable();

            // Hides mouse cursor and confines its movement to the game window, used in FPS-games or flight simulators,..
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        void Update()
        {
            //--Read input
            Vector2 movementInput = _playerControl.Player.Move.ReadValue<Vector2>();
            Vector2 lookInput = _playerControl.Player.Look.ReadValue<Vector2>();
            
            /*
             (IsServer && IsLocalPlayer) is true if:
            - The game use host-client model (not a dedicated server).
            - The script is running on the host client (client act as a server)
            - The script is attached to the game object representing the host player.
            */

            /*
             (IsClient && IsLocalPlayer) is true if:
            - The script is on your locally controlled player in a client build.. 
            
            It's false in all other scenarios, such as:
            - On the server: IsClient would be false.
            - On a client, but not on the local player: IsLocalPlayer would be false (e.g., if you're running a client and viewing another player's character).
            - In a standalone build (no networking): IsClient and IsLocalPlayer would likely be false or undefined, depending on the networking library's implementation.
            */
            if (IsClient && IsLocalPlayer)
            {
                _playerMovement.ProcessLocalPlayerMovement(movementInput, lookInput);
            }
            else
            {
                _playerMovement.ProcessSimulatedPlayerMovement();
            }

            //If the LocalPlayer click the mouse
            if (IsLocalPlayer && _playerControl.Player.Interact.inProgress)
            {
                if (Physics.Raycast(_camTransform.position, _camTransform.forward, out RaycastHit hit, _interactDistance,
                        _interactLayers))
                {
                    if (hit.collider.TryGetComponent<ButtonDoor>(out ButtonDoor buttonDoor))
                    {
                        //If we hit a button with Raycast successfully, do the same thing on Server
                        UseButtonServerRpc();
                    }
                }
            }
        }

        [ServerRpc]
        private void UseButtonServerRpc()
        {
            if (Physics.Raycast(_camTransform.position, _camTransform.forward, out RaycastHit hit, _interactDistance,
                    _interactLayers))
            {
                if (hit.collider.TryGetComponent<ButtonDoor>(out ButtonDoor buttonDoor))
                {
                    buttonDoor.Activate();
                }
            }
        }
    }
}
