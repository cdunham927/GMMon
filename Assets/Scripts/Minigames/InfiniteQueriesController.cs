using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfiniteQueriesController : MonoBehaviour
{
    public string[] objects = { "Rubber Duck", "Paperclip", "'Stop' Sign", "Key", "Birthday Candle", "Toothbrush", "Ice Cube" };
    public string currentObject;
    public TMP_Text commandText;

    public bool multiRound;
    public GameObject roundButton;

    private void Start()
    {
        multiRound = true;
    }

    public void StartRound()
    {
        //Get new command
        GetInstruction();
        roundButton.SetActive(false);
    }

    private void Update()
    {

    }

    public void GetInstruction()
    {
        int index = Random.Range(0, objects.Length);
        currentObject = objects[index];

        commandText.text = currentObject;

        gameObject.SetActive(false);
    }
}
