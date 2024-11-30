//Represent the button to open DoorButton door-type

using Unity.Netcode;
using UnityEngine;

namespace Game.Enviroment
{
    public class ButtonDoor : NetworkBehaviour
    {
        public delegate void ButtonPressed(ButtonDoor buttonDoor);

        public event ButtonPressed OnButtonPressed;

        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Material _activeMat;
        [SerializeField] private Material _unActiveMat;

        public void Activate()
        {
            OnButtonPressed?.Invoke(this);
            _meshRenderer.material = _activeMat;
        }
        
    }
}