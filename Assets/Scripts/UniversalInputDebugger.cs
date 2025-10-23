using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class UniversalInputDebugger : MonoBehaviour
{
    void Update()
    {
        // Test for a standard Gamepad
        if (Gamepad.current != null)
        {
            if (Gamepad.current.leftStick.ReadValue().magnitude > 0.1f)
            {
                Debug.Log("SUCCESS: Gamepad Stick is Moving!");
            }

            // CORRECTED METHOD for checking buttons on a Gamepad
            foreach (var control in Gamepad.current.allControls)
            {
                if (control is ButtonControl button && button.wasPressedThisFrame)
                {
                    Debug.Log("SUCCESS: Gamepad Button Was Pressed! Name: " + control.displayName);
                    break;
                }
            }
        }

        // Test for a generic Joystick
        else if (Joystick.current != null)
        {
            foreach (var control in Joystick.current.allControls)
            {
                if (control is StickControl stick && stick.ReadValue().magnitude > 0.1f)
                {
                    Debug.Log("SUCCESS: Generic Joystick Stick is Moving!");
                    break;
                }
                if (control is ButtonControl button && button.wasPressedThisFrame)
                {
                    Debug.Log("SUCCESS: Generic Joystick Button Was Pressed!");
                    break;
                }
            }
        }

        // Test for ANY keyboard key press
        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            Debug.Log("SUCCESS: Keyboard Key Was Pressed! Your controller might be a keyboard encoder.");
        }
    }
}