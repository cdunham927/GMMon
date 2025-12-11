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
    public Vector2[] playerCurAnswer = { };
    public Image[] playerCurQuestion = { };
    public int[] curQuestionIndex;
    public float[] checkedAnswer;
    public float checkTime = 0.2f;
    public Vector2[] joystickInputs;
    public float minigameTime = 30f;
    int players;

    private void Awake()
    {
        players = GameManager.instance.players;

        //Resize arrays
        System.Array.Resize(ref joystickInputs, players);
        System.Array.Resize(ref playerCurAnswer, players);
        System.Array.Resize(ref playerCurQuestion, players);
        System.Array.Resize(ref curQuestionIndex, players);
        System.Array.Resize(ref checkedAnswer, players);

        ShuffleQuestions();
    }

    public bool multiRound;
    public GameObject roundButton;

    private void Start()
    {
        multiRound = true;
    }

    void ShuffleQuestions()
    {
        //Shuffle question and answer arrays once before the game starts
		int count = questions.Length;
        for (int i = count - 1; i > 0; --i)
        {
            int randIndex = Random.Range(0, i);

            Sprite temp = questions[i];
            questions[i] = questions[randIndex];
            questions[randIndex] = temp;

            Vector2 tempD = answers[i];
            answers[i] = answers[randIndex];
            answers[randIndex] = tempD;
        }
    }

    public void StartGame()
    {
        if (!playing)
        {
            roundButton.SetActive(false);
            //Get new command
            playing = true;

            for (int i = 0; i < players; i++)
            {
                playerCurQuestion[i] = FindObjectOfType<PlayerFunctions>().playerScoresText[i].catImpImage;
            }

            GetInstruction();
        }
    }

    private void Update()
    {
        if (playing)
        {
            for (int i = 0; i < players; i++)
            {
                if (joystickInputs[i] != Vector2.zero && checkedAnswer[i] <= 0f)
                {
                    checkedAnswer[i] = checkTime;

                    if (joystickInputs[i] == playerCurAnswer[i])
                    {
                        CorrectAnswer(i);
                    }
                    else
                    {
                        IncorrectAnswer(i);
                    }
                }
            }
        }

        //Check for player inputs in here
        for (int i = 0; i < players; i++)
        {
            joystickInputs[i] = new Vector2(Mathf.RoundToInt(Input.GetAxisRaw("Horizontal" + (i + 1).ToString())), Mathf.RoundToInt(Input.GetAxisRaw("Vertical" + (i + 1).ToString())));
            //Debug.Log(joystickInputs[i]);

            if (checkedAnswer[i] > 0) checkedAnswer[i] -= Time.deltaTime;
        }
    }

    void CorrectAnswer(int p)
    {
        if (curQuestionIndex[p] < questions.Length - 1)
        {
            curQuestionIndex[p]++;

            playerCurAnswer[p] = answers[curQuestionIndex[p]];
            playerCurQuestion[p].sprite = questions[curQuestionIndex[p]];
        }
        else
        {
            //Win game
            playing = false;
        }
    }

    void IncorrectAnswer(int p)
    {
        if (curQuestionIndex[p] > 0)
        {
            curQuestionIndex[p]--;

            playerCurAnswer[p] = answers[curQuestionIndex[p]];
            playerCurQuestion[p].sprite = questions[curQuestionIndex[p]];
        }
    }

    public void GetInstruction()
    {
        for (int i = 0; i < players; i++)
        {
            curQuestionIndex[i] = 0;

            playerCurAnswer[i] = answers[curQuestionIndex[i]];
            playerCurQuestion[i].sprite = questions[curQuestionIndex[i]];
        }
    }
}
