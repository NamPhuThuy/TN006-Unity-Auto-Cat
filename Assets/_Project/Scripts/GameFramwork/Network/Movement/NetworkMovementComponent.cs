using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;

namespace GameFramework.Network.Movement
{
    /// <summary>
    /// This class is used to implement Client prediction technique
    /// </summary>
    public class NetworkMovementComponent : NetworkBehaviour
    {
        [SerializeField] private CharacterController _cc;
        
        [Header("Some vars")]
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _turnSpeed = 10f;
        [SerializeField] private Transform _camSocket;
        [SerializeField] private GameObject _vcam;


        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private Color _color;
        private Transform _vcamTransform;
        
        /// <summary>
        /// a "tick" refers to a single iteration or step of the game's main loop on the server. It's the fundamental unit of time in which the game's state is updated and simulated. Think of it as a single frame, but specifically on the server side, responsible for managing the game's logic and consistency across all clients.
        /// </summary>
        private int _tick = 0;
        private float _tickRate = 1f / 60f;
        private float _tickDeltaTime = 0f; //how much time elapsed since the last tick

        private const int BUFFER_SIZE = 1024; //size of a buffer used for network communication
        //The maximum number of Input client can keep = BUFFER_SIZE
        private InputState[] _inputStates = new InputState[BUFFER_SIZE];
        private TransformState[] _transformStates = new TransformState[BUFFER_SIZE];

        //The latest value of TransfromState that the Server manage (the latest transform on the Server)
        public NetworkVariable<TransformState> ServerTransformState = new NetworkVariable<TransformState>();
        public TransformState PreviousTransformState = new TransformState();
        
        private void OnEnable()
        {
            //Listen to the variable-change
            ServerTransformState.OnValueChanged += OnServerStateChanged;
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            _vcamTransform = _vcam.transform;
        }

        private void OnServerStateChanged(TransformState previousvalue, TransformState serverState)
        {
            // PreviousTransformState = previousvalue;
            if (!IsLocalPlayer) return;
            if (PreviousTransformState == null)
            {
                PreviousTransformState = serverState;
            }

            //Check for null, before sync with the server 
            if (_transformStates == null) return;
            if (_transformStates.Length < 1) return;
            
            //The last time i check, 'THIS' cause exception
            foreach (TransformState state in _transformStates)
            {
                if (state == null) 
                    return;
            }
            if (serverState == null) return;
            
            //Search for the first 'localState' in _transformStates that match the condition: localState.Tick == serverState.Tick
            TransformState calculateTransform = _transformStates.First(localState => localState.Tick == serverState.Tick);
            if (calculateTransform.Position != serverState.Position)
            {
                //We are out of sync with the server
                /*
                How to fix:
                - Teleport
                - Replay
                */
                
                Debug.Log($"Correcting client position");
                //- Teleport the player to the server's position
                TeleportPlayer(serverState);
                
                // - Replay the inputs that happened after
                IEnumerable<InputState> inputs = _inputStates.Where(input => input.Tick > serverState.Tick);
                inputs = from input in inputs orderby input.Tick select input;
                
                foreach (InputState inputState in inputs)
                {
                    MovePlayer(inputState.MovementInput);
                    RotatePlayer(inputState.LookInput);

                    TransformState newTransformstate = new TransformState()
                    {
                        Tick = inputState.Tick,
                        Position = transform.position,
                        Rotation = transform.rotation,
                        HasStartedMoving = true
                    };

                    for (int i = 0; i < _transformStates.Length; i++)
                    {
                        if (_transformStates[i].Tick == inputState.Tick)
                        {
                            _transformStates[i] = newTransformstate;
                            break;
                        }
                    }
                }
            }
        }

        private void TeleportPlayer(TransformState state)
        {
            //Disable the CharacterController so that we can directly change the player's transform without any bug
            _cc.enabled = false;
            transform.position = state.Position;
            transform.rotation = state.Rotation;
            _cc.enabled = true;

            for (int i = 0; i < _transformStates.Length; i++)
            {
                if (_transformStates[i].Tick == state.Tick)
                {
                    _transformStates[i] = state;
                    break;
                }
            }
        }

        /// <summary>
        ///Purpose: Manages the movement of the local player (the player controlled directly by the client running this code). <br /> <br/>
        /// Functionality: <br/>
        /// - Client-side prediction: moves the player locally<br/>
        /// - Server authority: sends the input to the server via an RPC. The server then authoritatively updates the player's position<br/>
        /// - Client-side buffering: It stores "input" and "resulting transform states" in local buffers (_inputStates, _transformStates), allows for client-side reconciliation in case of lag or packet loss. This is crucial for smooth gameplay. <br/>
        /// - Server-side handling (if host): If the client is also the server (host mode), it directly updates the player's position and server-side state. <br/> 
        /// </summary>
        /// <param name="movementInput"></param>
        /// <param name="lookInput"></param>
        public void ProcessLocalPlayerMovement(Vector2 movementInput, Vector2 lookInput)
        {
            _tickDeltaTime += Time.deltaTime;
            if (_tickDeltaTime > _tickRate)
            {
                //Process the Input
                int bufferIndex = _tick % BUFFER_SIZE;
                
                /*IsClient is true on any client, including the host in a host-client setup.
                 !IsServer (not IsServer) is true on all clients except the host in a host-client setup.
                  
                - In a dedicated server setup, they are functionally equivalent.*/
                if (!IsServer)
                {
                    //Send to the server the information of how we move 
                    MovePlayerServerRpc(_tick, movementInput, lookInput);
                    
                    //Then, we move it immediately
                    MovePlayer(movementInput);
                    RotatePlayer(lookInput);
                }
                else
                {
                    MovePlayer(movementInput);
                    RotatePlayer(lookInput);

                    TransformState state = new TransformState()
                    {
                        Tick = _tick,
                        Position = transform.position,
                        Rotation = transform.rotation,
                        HasStartedMoving = true
                    };
                    
                    /*
                     We have to save the state with SaveState() before the server change the value.
                     If we dont call the SaveState(), the server's value was getting updated before the server actually save it to the local variables (is this talk about host?)
                
                     */
                    SaveState(movementInput, lookInput, bufferIndex);
                    
                    PreviousTransformState = ServerTransformState.Value;
                    ServerTransformState.Value = state;
                }

                InputState inputState = new InputState()
                {
                    Tick = _tick,
                    MovementInput = movementInput,
                    LookInput = lookInput
                };

                TransformState transformState = new TransformState()
                {
                    Tick = _tick,
                    Position = transform.position,
                    Rotation = transform.rotation,
                    HasStartedMoving = true
                };

                //Store in local
                _inputStates[bufferIndex] = inputState;
                _transformStates[bufferIndex] = transformState;

                _tickDeltaTime -= _tickRate;
                _tick++;
            }
        }
        
        
        //Update for all other players in the game that not the local player (player that we controller directly)
        /// <summary>
        /// Purpose: Manages movement of remote players (players controlled by other clients). <br/>
        /// Functionality:
        /// - Server-side authority: It relies entirely on the server's authoritative state (ServerTransformState). It simply interpolates or snaps to the position and rotation provided by the server.
        /// - No client-side prediction or input: This method doesn't predict movement or handle input. It only visually updates the player's position and rotation based on the server's updates.
        /// </summary>
        public void ProcessSimulatedPlayerMovement()
        {
            _tickDeltaTime += Time.deltaTime;
            if (_tickDeltaTime > _tickRate)
            {
                //There some NullReferenceException occurs here when the client enter the game-play scene in the early game (it stop in some frame-about 53 ticks after)
                /*Debug.Log($"TNam - hasMoving: {ServerTransformState.Value.HasStartedMoving}");
                Debug.Log($"TNam - Value: {ServerTransformState.Value}");*/
                if (ServerTransformState.Value.HasStartedMoving)
                {
                    transform.position = ServerTransformState.Value.Position;
                    transform.rotation = ServerTransformState.Value.Rotation;
                }

                _tickDeltaTime -= _tickRate;
                _tick++;
            }
        }

        private void SaveState(Vector2 movementInput, Vector2 lookInput, int bufferIndex)
        {
            InputState inputState = new InputState()
            {
                Tick = _tick,
                MovementInput = movementInput,
                LookInput = lookInput
            };

            TransformState transformState = new TransformState()
            {
                Tick = _tick,
                Position = transform.position,
                Rotation = transform.rotation,
                HasStartedMoving = true
            };

            _inputStates[bufferIndex] = inputState;
            _transformStates[bufferIndex] = transformState;
        }
        
        [ServerRpc]
        private void MovePlayerServerRpc(int tick, Vector2 movementInput, Vector2 lookInput)
        {
            // 
            /*
             Check if the server receive the message from client or not with:
                - if (_tick != PreviousTransformState.Tick + 1) 
                
            There an NullReferenceException occur here if we trying to access the "PreviousTransformState" 
             */
            
            MovePlayer(movementInput);
            RotatePlayer(lookInput);

            //Store the current state as the _prevState, then continue the game
            TransformState state = new TransformState()
            {
                Tick = tick,
                Position = transform.position,
                Rotation = transform.rotation,
                HasStartedMoving = true
            };

            PreviousTransformState = state;
            ServerTransformState.Value = state;
        }

        #region UpdatePlayerLocally
        private void RotatePlayer(Vector2 lookInput)
        {
            _vcamTransform.RotateAround(_vcamTransform.position, _vcamTransform.right, -lookInput.y * _turnSpeed * _tickRate);
            transform.RotateAround(transform.position, transform.up, lookInput.x * _turnSpeed * _tickRate);
        }

        private void MovePlayer(Vector2 movementInput)
        {
            Vector3 movement = movementInput.x * _vcamTransform.right + movementInput.y * _vcamTransform.forward;

            movement.y = 0;
            if (!_cc.isGrounded)
            {
                movement.y = Physics.gravity.y; //= -9.81
            }

            _cc.Move(movement * (_speed * _tickRate));
        }
        

        #endregion

        /*private void OnDrawGizmos()
        {
            Debug.Log($"TNam - OnDrawGizmos");
            if (ServerTransformState.Value != null)
            {
                Debug.Log($"TNam - Draw Shadow");
                Gizmos.color = _color;
                Gizmos.DrawMesh(_meshFilter.mesh, ServerTransformState.Value.Position);
            }
        }*/
    }
}