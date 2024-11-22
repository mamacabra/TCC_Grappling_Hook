using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.InputSystem.Users;

namespace LocalMultiplayer
{
    public class PlayersUIInputs : MonoBehaviour
    {
        private Menus_Input actions;// Input

        private void OnEnable() {
            if (actions == null) actions = new Menus_Input();
            actions.Navigation.Join.performed += OnJoin;
            actions.Navigation.Cancel.performed += OnCancel;
            actions.Navigation.Reset.performed += OnReset;
            EnableInputActions();
        }

        private void OnReset(InputAction.CallbackContext obj)
        {
            InterfaceManager.Instance.ResetOptions();
        }

        private void OnDisable() {
            actions.Navigation.Join.performed -= OnJoin;
            actions.Navigation.Cancel.performed -= OnCancel;
        }

        private void Awake() {
            PlayersManager.Instance.playersUIInputs = this;
        }

        private void OnJoin(InputAction.CallbackContext context) {// Join Input Pressed
            if (InterfaceManager.Instance.GetCurrentScreenIndex != (int)ScreensName.CharacterChoice_Screen) return;
            // Check if the action was triggered by a control
            if (context.control != null && context.action.WasPerformedThisFrame()) {
                // Get the input device
                InputDevice device = context.control.device;

                // Get the control scheme
                int bindingIndex = context.action.GetBindingIndexForControl(context.control);
                string controlScheme = context.action.bindings[bindingIndex].groups;
                var acitonMap = context.action.actionMap;

                // Get free id
                int id = PlayersManager.Instance.GetFreeId();
                if (id == -1) return; // not has free id return

                PlayerInput player;
                // player.gameObject.SetActive(true);

                if (PlayersManager.Instance.isDebug) { // For add ilimitted players pressing J
                    player = PlayersManager.Instance.characterChoice.ReturnPlayerInput(true,false);
                    // player = PlayersManager.Instance.characterChoice.ReturnPlayerInput(id);
                    player.SwitchCurrentControlScheme(controlScheme: controlScheme, device);
                    return;
                }

                // Join a new player
                if (controlScheme == "Keyboard&Mouse") { // Keyboard P1
                    if (PlayersManager.Instance.keyboardP1) return;
                    PlayersManager.Instance.keyboardP1 = true;
                    // player = PlayersManager.Instance.characterChoice.ReturnPlayerInput(id);
                    player = PlayersManager.Instance.characterChoice.ReturnPlayerInput(false, true);
                } else if (controlScheme == "KeyboardP2") { // Keyboard P2
                    if (PlayersManager.Instance.keyboardP2) return;
                    PlayersManager.Instance.keyboardP2 = true;
                    // player = PlayersManager.Instance.characterChoice.ReturnPlayerInput(id);
                    player = PlayersManager.Instance.characterChoice.ReturnPlayerInput(false,false);
                } else { // Gamepad
                    if (PlayerInput.FindFirstPairedToDevice(device) != null) return;
                    // player = PlayersManager.Instance.characterChoice.ReturnPlayerInput(id);
                    player = PlayersManager.Instance.characterChoice.ReturnPlayerInput(true, false);
                }
                player.SwitchCurrentControlScheme(controlScheme: controlScheme, device);
                PlayersManager.Instance.OnPlayerJoinedEvent(player);
            }
        }
        private void OnCancel(InputAction.CallbackContext context) {
            if (context.control == null) return;

            if (context.action.WasPerformedThisFrame()) {
                InterfaceManager.Instance.ReturnCurrentScreen();
            }
        }
        

        #region ActiveInputActions
        public void EnableInputActions() {
            actions.Navigation.Enable();
        }
        public void DisableInputActions() {
            actions.Navigation.Disable();
        }
        #endregion
    }
}

