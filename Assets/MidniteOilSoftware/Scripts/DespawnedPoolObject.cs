using UnityEngine;

namespace MidniteOilSoftware.ObjectPoolManager
{
    /// <summary>
    /// Optional component that can be added to a pooled object. It implements the ReturnedToPool()
    /// method which is a good place to perform any required cleanup when the object is returned
    /// to the object pool. 
    /// </summary>
    public class DespawnedPoolObject : MonoBehaviour, IDespawnedPoolObject
    {
        Rigidbody _rigidBody;
        Rigidbody2D _rigidbody2D;
        
        void Awake()
        {
            _rigidBody = GetComponentInParent<Rigidbody>();
            _rigidbody2D = GetComponentInParent<Rigidbody2D>();
        }

        public void ReturnedToPool()
        {
            if (_rigidBody)
            {
                _rigidBody.velocity = _rigidBody.angularVelocity = Vector3.zero;
            }

            if (_rigidbody2D)
            {
                _rigidbody2D.velocity = Vector2.zero;
                _rigidbody2D.angularVelocity = 0f;
            }
        }
    }
}
