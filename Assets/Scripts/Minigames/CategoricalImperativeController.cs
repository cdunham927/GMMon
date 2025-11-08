using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CategoricalImperativeController : MonoBehaviour
{
    bool playing = false;
    public Sprite[] questions = { };
    public Vector2[] answers = { };
    public Vector2[] playerCurQuestion;
    public int[] curQuestionIndex;

    public Vector2[] joystickInputs;


    public TMP_Text commandText;
    public TMP_Text yurpusCommandText;

    public float minigameTime = 30f;
    float curTime;
    public TMP_Text timeRemaining;
    public TMP_Text timeRemaining2;

    int players;

    private void Awake()
    {
        players = GameManager.instance.players;

        //Resize arrays
        System.Array.Resize(ref joystickInputs, players);
        System.Array.Resize(ref playerCurQuestion, players);
        System.Array.Resize(ref curQuestionIndex, players);
    }

    void ShuffleQuestions()
    {
        //Shuffle question and answer arrays once before the game starts
    }

    public void StartGame()
    {
        //Get new command
        playing = true;
        GetInstruction();
    }

    private void Update()
    {
        if (playing)
        {

        }



        //Check for player inputs in here
        for (int i = 0; i < players; i++)
        {
            joystickInputs[i] = new Vector2(Mathf.RoundToInt(Input.GetAxisRaw("Horizontal" + (i + 1).ToString())), Mathf.RoundToInt(Input.GetAxisRaw("Vertical" + (i + 1).ToString())));
            //Debug.Log(joystickInputs[i]);
        }
    }

    void CorrectAnswer(int p)
    {
        curQuestionIndex[p]++;
    }

    void IncorrectAnswer(int p)
    {
        curQuestionIndex[p]--;
    }

    public void GetInstruction()
    {
        for (int i = 0; i < players; i++)
        {
            curQuestionIndex[i] = 0;

            playerCurQuestion[curQuestionIndex[i]] = answers[i];
        }
    }
}
