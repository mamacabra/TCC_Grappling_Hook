using Character.Utils;
using LocalMultiplayer;
using LocalMultiplayer.Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class CharacterInput : ACharacterMonoBehaviour
    {
        public Vector2 Axes { get; private set; } = Vector2.zero;
        public Vector3 MoveDirection => new (Axes.x, 0, Axes.y);
        public Vector3 LookDirection { get; set;} = new (0,0,-1);

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.started)
                CharacterEntity.CharacterState.SetDashState();
        }

        public void OnMelee(InputAction.CallbackContext context)
        {
            if (context.started)
                CharacterEntity.CharacterState.SetAttackMeleeState();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Axes = context.ReadValue<Vector2>();
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (context.started)
                CharacterEntity.CharacterState.SetPrepareHookState();
            else if (context.canceled)
                CharacterEntity.CharacterState.SetDispatchHookState();
        }

        public void OnDeviceLost(PlayerInput playerInput) {
            var interfaceManager = InterfaceManager.Instance;
            var playersManager = PlayersManager.Instance;
            if (!interfaceManager || !playersManager) return;
            bool canPause = (!interfaceManager.isOnCount && !interfaceManager.isOnFeedback && !playersManager.GameOver);
            if (!interfaceManager.pause && canPause) {
                interfaceManager.ShowSpecificScreen(ScreensName.Pause_InGame_Screen);
                interfaceManager.pause = true;
            }
            var playerConfig = playersManager.GetPlayerConfig(CharacterEntity.Character.Id);
            interfaceManager.notificationManager.PlayNotification($"Dispositivo {playerConfig.inputDevicesNames[0]} desconectado!");
        }

        public void OnDeviceRegained(PlayerInput playerInput) {
            var playerConfig = PlayersManager.Instance.GetPlayerConfig(CharacterEntity.Character.Id);
            InterfaceManager.Instance.notificationManager.PlayNotification($"Dispositivo {playerConfig.inputDevicesNames[0]} reconectado!");
        }
    }
}
