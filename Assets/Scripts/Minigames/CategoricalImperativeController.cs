using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CategoricalImperativeController : MonoBehaviour
{
    public Sprite[] questions = { };
    public Vector2[] answers = { };
    public Vector2[] playerCurQuestion;


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
        int i = Random.Range(0, questions.Length);


    }
}
