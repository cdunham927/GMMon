using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls; // We need this for ButtonControl

public class InputDebugger : MonoBehaviour
{
    void Update()
    {
        // Check if any gamepad is connected
        if (Gamepad.current != null)
        {
            // Read the joystick's current value
            Vector2 stickValue = Gamepad.current.leftStick.ReadValue();

            // If the stick is being moved at all...
            if (stickValue.magnitude > 0.1f)
            {
                Debug.Log("Gamepad Stick is Moving! Value: " + stickValue);
            }

            // NEW METHOD: Loop through all controls on the gamepad
            foreach (var control in Gamepad.current.allControls)
            {
                // Check if the control is a button and if it was pressed this frame
                if (control is ButtonControl button && button.wasPressedThisFrame)
                {
                    // If we find one, print a message and stop checking for this frame
                    Debug.Log("A Gamepad Button Was Pressed! Name: " + control.displayName);
                    break;
                }
            }
        }
    }
}