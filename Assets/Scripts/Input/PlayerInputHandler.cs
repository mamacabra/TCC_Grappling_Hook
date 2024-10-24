using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public enum CurrentControl
    {
        None,
        Keyboard,
        Mouse,
        Gamepad,
    }
    public static CurrentControl currentControl {
        get {
            if (Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame) return CurrentControl.Gamepad;
            else if (Mouse.current != null && Mouse.current.wasUpdatedThisFrame) return CurrentControl.Mouse;
            else if (Keyboard.current != null && Keyboard.current.wasUpdatedThisFrame) return CurrentControl.Keyboard;
            else return CurrentControl.None;
        }
    }
}
