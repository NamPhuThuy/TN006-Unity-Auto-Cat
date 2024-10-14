using MidniteOilSoftware.ObjectPoolManager;
using UnityEngine;

/// <summary>
/// Attach to a GameObject to have it automatically be despawned and added to the
/// object pool after a specified delay.
/// </summary>
public class DespawnAfterDelay : MonoBehaviour
{
    [SerializeField] [Range(5f, 30f)] float _despawnDelay = 10f;

    private void OnEnable()
    {
        Invoke(nameof(Despawn), _despawnDelay);
    }

    private void Despawn()
    {
        ObjectPoolManager.DespawnGameObject(gameObject);
    }
    
}
