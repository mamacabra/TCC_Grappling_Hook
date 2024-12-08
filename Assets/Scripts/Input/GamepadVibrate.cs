using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadVibrate : MonoBehaviour
{
    [HideInInspector]public PlayerInput playerInput;
    [HideInInspector]public Gamepad pad;
    
    public bool rumble = true;
    private void Awake() 
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void RumblePulse(float lowFrequency, float highFrequency, float duration)
    {
        if(!rumble)return;
        pad = playerInput.GetDevice<Gamepad>();
        //pad = Gamepad.current;

        if (pad == null) return;
        
        pad.SetMotorSpeeds(lowFrequency,highFrequency);
      
        StartCoroutine(StopRumble());
        IEnumerator StopRumble()
        {
            yield return new WaitForSecondsRealtime(duration);
            pad.SetMotorSpeeds(0,0);
        }
    }
}
