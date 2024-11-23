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

        private void OnDisable() {
            actions.Navigation.Join.performed -= OnJoin;
            actions.Navigation.Cancel.performed -= OnCancel;
            actions.Navigation.Reset.performed -= OnReset;
        }

        private void Awake() {
            PlayersManager.Instance.playersUIInputs = this;
        }

        private void OnJoin(InputAction.CallbackContext context) {// Join Input Pressed
            if (context.control == null) return;

            // Check if the action was triggered by a control
            if (context.action.WasPerformedThisFrame()) {

                //Enable a new player on character selection
                if (InterfaceManager.Instance.GetCurrentScreenIndex == (int)ScreensName.CharacterChoice_Screen) {
                    // Get the input device
                    InputDevice device = context.control.device;

                    // Get the control scheme
                    int bindingIndex = context.action.GetBindingIndexForControl(context.control);
                    string controlScheme = context.action.bindings[bindingIndex].groups;

                    PlayersManager.Instance.JoinPlayer(device, controlScheme);
                }
            }
        }
        private void OnCancel(InputAction.CallbackContext context) {
            if (context.control == null) return;

            if (context.action.WasPerformedThisFrame()) {
                InterfaceManager.Instance.ReturnCurrentScreen();
            }
        }
        
        private void OnReset(InputAction.CallbackContext context) {
            if (context.control == null) return;

            if (context.action.WasPerformedThisFrame()) {
                InterfaceManager.Instance.ResetOptions();
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

