using Unity.Netcode;
using UnityEngine;

namespace GameFramework.Network.Movement
{
    //INetworkSerializable interface provides a standardized and efficient way to serialize and deserialize data for network transmission.
    public class TransformState : INetworkSerializable
    {
        [Header("Transfer Information")]
        public int Tick;
        public Vector3 Position;
        public Quaternion Rotation;
        public bool HasStartedMoving;
        
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            //If this is the Reader
            if (serializer.IsReader)
            {
                //the order of things we read must be the same as the order when we write. That how network-message work
                
                var reader = serializer.GetFastBufferReader();
                reader.ReadValueSafe(out Tick);
                reader.ReadValueSafe(out Position);
                reader.ReadValueSafe(out Rotation);
                reader.ReadValueSafe(out HasStartedMoving);
            }
            //If this is the Writer
            else
            {
                var writer = serializer.GetFastBufferWriter();
                writer.WriteValueSafe(Tick);
                writer.WriteValueSafe(Position);
                writer.WriteValueSafe(Rotation);
                writer.WriteValueSafe(HasStartedMoving);
            }
        }
    }
}