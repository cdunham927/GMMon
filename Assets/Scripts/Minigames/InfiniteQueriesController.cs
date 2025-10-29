using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfiniteQueriesController : MonoBehaviour
{
    public string[] objects = { "Rubber Duck", "Paperclip", "'Stop' Sign", "Key", "Birthday Candle", "Toothbrush", "Ice Cube" };
    public string currentObject;

    public TMP_Text commandText;
    public TMP_Text yurpusCommandText;

    public float roundTimeSmall = 2f;
    public float roundTimeBig = 10f;
    float curTime;
    public TMP_Text timeRemaining;
    public TMP_Text timeRemaining2;

    public void StartRound()
    {
        //Get new command
        GetInstruction();
    }

    private void Update()
    {
        //if (curTime > 0) curTime -= Time.deltaTime;

        //Show time remaining
        //int seconds = ((int)curTime % 60);
        //int minutes = ((int)curTime / 60);
        //timeRemaining.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        //timeRemaining2.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void GetInstruction()
    {
        int index = Random.Range(0, objects.Length);
        currentObject = objects[index];

        commandText.text = currentObject;
        yurpusCommandText.text = currentObject;

        gameObject.SetActive(false);
    }
}
