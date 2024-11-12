using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovementController : NetworkBehaviour
{
    [SerializeField] private float _horiInput, _vertInput;
    private Vector2 _direction;
    [SerializeField] private float _speed = 12f;
    
    // NetworkVariable to sync the player's position across the network
    // private NetworkVariable<Vector2> networkedPosition = new NetworkVariable<Vector2>(Vector2.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
   
    private void Start()
    {
        // Only the owner should control the movement
        if (!IsOwner) return;

        // Register to handle changes in position
        // networkedPosition.OnValueChanged += OnPositionChanged;
    }

    // This method is called when the networked position changes (syncs position for other clients)
    private void OnPositionChanged(Vector2 previousValue, Vector2 newValue)
    {
        if (!IsOwner)
        {
            // Only non-owners should update their position (sync other clients to the server position)
            transform.position = newValue;
        }
    }
    
    void Update()
    {
        if (!IsOwner) return;
        //Move with 4 keys
        MovementHandle();
        
    }

    public void MovementHandle()
    {
        _horiInput = Input.GetAxis("Horizontal");
        _vertInput = Input.GetAxis("Vertical");
        _direction = new Vector2(_horiInput, _vertInput).normalized;
        
        transform.Translate((_speed * Time.deltaTime) * _direction);
        
        // Vector2 newPosi = (Vector2)transform.position + _direction * (_speed * Time.deltaTime);
        // SendMovementInforRpc(newPosi);
    }

    // This method runs on the server to update the position based on the client request
    [Rpc(SendTo.Server)]
    private void SendMovementInforRpc(Vector2 newPosi, RpcParams rpcParams = default)
    {
        Debug.Log($"Server Received the movement request on NetworkObject #{NetworkObjectId}");
        // networkedPosition.Value = newPosi;
    }
    
    //--MOVE STEP BY STEP
    public void Move()
    {
        SubmitPositionRequestRpc();
    }
    
    [Rpc(SendTo.Server)]
    void SubmitPositionRequestRpc(RpcParams rpcParams = default)
    {
        Debug.Log($"Server Received the movement request on NetworkObject #{NetworkObjectId}");
        var randomPosition = Random.insideUnitCircle * 4.6f;
        transform.position = randomPosition;
        // networkedPosition.Value = randomPosition;
    }
    
    private void OnDestroy()
    {
        if (IsOwner)
        {
            // networkedPosition.OnValueChanged -= OnPositionChanged;
        }
    }
}
