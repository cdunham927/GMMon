using UnityEngine;
using System.IO.Ports;
using UnityEngine.Events;

public class BuzzerManager : MonoBehaviour
{
    public string portName = "COM3";
    private SerialPort serialPort;
    private string incomingMessage = "";

    public UnityEvent onBuzzer1Pressed;
    public UnityEvent onBuzzer2Pressed;
    public UnityEvent onBuzzer3Pressed;
    public UnityEvent onBuzzer4Pressed;
    public UnityEvent onBuzzer5Pressed;
    public UnityEvent onBuzzer6Pressed;

    void Start()
    {
        try
        {
            serialPort = new SerialPort(portName, 115200);
            serialPort.ReadTimeout = 100;
            serialPort.Open();
            Debug.Log("Successfully opened port: " + portName);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Could not open serial port: " + e.Message);
        }
    }
    void Update()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try // Try to perform the risky operation
            {
                incomingMessage += serialPort.ReadExisting();

                int newlineIndex = incomingMessage.IndexOf('\n');
                if (newlineIndex >= 0)
                {
                    string message = incomingMessage.Substring(0, newlineIndex).Trim();
                    incomingMessage = incomingMessage.Substring(newlineIndex + 1);

                    ProcessMessage(message);
                }
            }
            catch (System.IO.IOException) // Catch the exact error you're getting
            {
                // This will catch the error without crashing.
                // It just prints a warning so you know it happened.
                Debug.LogWarning("IOException: The port was likely disconnected. This is now handled.");
            }
            catch (System.TimeoutException)
            {
                /* This is normal, just means no data was available */
            }
        }
    }

    void ProcessMessage(string message)
    {
        string[] parts = message.Split(':');
        if (parts.Length == 2)
        {
            string buzzerID = parts[0];
            string action = parts[1];
            if (action == "BUZZ")
            {
                if (buzzerID == "1") { onBuzzer1Pressed.Invoke(); }
                if (buzzerID == "2") { onBuzzer2Pressed.Invoke(); }
            }
        }
    }

    void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}