using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CategoricalImperativeController : MonoBehaviour
{
    public string[] directions = { "Up", "Down", "Left", "Right" };
    public Color[] colors = { Color.red, Color.blue, Color.yellow };
    public string[] shapes = { "Triangle", "Circle", "Square", "Star" };

    public TMP_Text commandText;
    public TMP_Text yurpusCommandText;

    public float minigameTime = 30f;
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
        //curTime = minigameTime;

        int dir = Random.Range(0, directions.Length);
        int col = Random.Range(0, colors.Length);
        int sha = Random.Range(0, shapes.Length);
        //currentDirection = directions[dir];

        //commandText.text = directionsText[index];
        //yurpusCommandText.text = directionsText[index];

        //gameObject.SetActive(false);
    }
}
