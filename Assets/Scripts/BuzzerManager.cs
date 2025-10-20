using UnityEngine;
using UnityEngine.Events;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class BuzzerManager : MonoBehaviour
{
    // --- Network Settings ---
    public int listenPort = 11000; // Must match the port in the ESP32 code

    // --- Events (These don't change) ---
    public UnityEvent onBuzzer1Pressed;
    public UnityEvent onBuzzer2Pressed;
    // ...etc...

    // --- UDP Listener ---
    private Thread receiveThread;
    private UdpClient client;
    private volatile string latestMessage = ""; // A thread-safe way to pass messages

    void Start()
    {
        // Start a background thread to listen for UDP messages
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
        Debug.Log("UDP Listener started on port " + listenPort);
    }

    void Update()
    {
        // Check every frame if a new message has arrived from the background thread
        if (latestMessage != "")
        {
            string messageToProcess = latestMessage;
            latestMessage = ""; // Clear the message so we only process it once
            ProcessMessage(messageToProcess);
        }
    }

    private void ReceiveData()
    {
        client = new UdpClient(listenPort);
        while (true)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref anyIP);
                string text = Encoding.UTF8.GetString(data);
                latestMessage = text; // Store the received message
            }
            catch (Exception err)
            {
                Debug.LogError(err.ToString());
            }
        }
    }

    void ProcessMessage(string message)
    {
        // This part of the logic is exactly the same as before
        Debug.Log(">> Received message: " + message);
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
        // Clean up the thread when the game closes
        if (receiveThread != null && receiveThread.IsAlive)
        {
            receiveThread.Abort();
        }
        if (client != null)
        {
            client.Close();
        }
    }
}