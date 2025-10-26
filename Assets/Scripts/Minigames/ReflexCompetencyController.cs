using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReflexCompetencyController : MonoBehaviour
{
    public Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    public string[] directionsText = { "Up", "Down", "Left", "Right" };
    public Vector2 currentDirection;

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

        int index = Random.Range(0, directionsText.Length);
        currentDirection = directions[index];

        commandText.text = directionsText[index];
        yurpusCommandText.text = directionsText[index];

        //gameObject.SetActive(false);
    }
}
