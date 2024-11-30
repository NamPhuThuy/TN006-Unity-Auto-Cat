
//Represent the door that need active buttons to open

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enviroment
{
    public class DoorButton : Door
    {
        [SerializeField] private List<ButtonDoor> _buttons;
        private Dictionary<ButtonDoor, bool> _activeButtons = new Dictionary<ButtonDoor, bool>();

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            Debug.Log($"Number of buttons: {_buttons.Count}");
            
            foreach (ButtonDoor buttonDoor in _buttons)
            {
                buttonDoor.OnButtonPressed += OnButtonPressed;
                _activeButtons.Add(buttonDoor, false);
            }
        }

        private void OnButtonPressed(ButtonDoor buttonDoor)
        {
            if (_buttons.Contains(buttonDoor))
            {
                _activeButtons[buttonDoor] = true;
                int numberOfButtonPressed = CountActivatedButtons();
              
                Debug.Log($"Clicked the button: {buttonDoor.name}");
                
                if (numberOfButtonPressed == _buttons.Count)
                {
                    _animCtrl.SetTrigger("OpenDoor");
                }
            }
        }

        private int CountActivatedButtons()
        {
            int numberOfButtonPressed = 0;
            foreach (KeyValuePair<ButtonDoor, bool> button in _activeButtons)
            {
                numberOfButtonPressed = button.Value ? numberOfButtonPressed + 1 : numberOfButtonPressed;
            }

            return numberOfButtonPressed;
        }

        private void ResetButtons()
        {
            foreach (ButtonDoor button in _buttons)
            {
                _activeButtons[button] = false;
            }
        }
    }
}