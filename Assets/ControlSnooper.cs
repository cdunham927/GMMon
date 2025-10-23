using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic; // Needed for the Dictionary

public class ControlSnooper : MonoBehaviour
{
    // A dictionary to store the last known value of each control
    private Dictionary<InputControl, object> lastValues = new Dictionary<InputControl, object>();
    private InputDevice currentDevice = null;

    void Update()
    {
        // First, figure out which device we're using (Gamepad or Joystick)
        if (Gamepad.current != null)
        {
            currentDevice = Gamepad.current;
        }
        else if (Joystick.current != null)
        {
            currentDevice = Joystick.current;
        }
        else
        {
            currentDevice = null;
        }

        if (currentDevice == null) return; // Do nothing if no device is found

        // Loop through every single control on the device
        foreach (var control in currentDevice.allControls)
        {
            // Read the control's current value
            var currentValue = control.ReadValueAsObject();

            // Check if we've seen this control before and if its value has changed
            if (lastValues.TryGetValue(control, out var lastValue))
            {
                if (!currentValue.Equals(lastValue))
                {
                    Debug.Log($"CONTROL CHANGED: Name='{control.name}', Display Name='{control.displayName}', Value={currentValue}");
                }
            }

            // Store the current value for the next frame
            lastValues[control] = currentValue;
        }
    }
}