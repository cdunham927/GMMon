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
    public GameObject key;

    public bool[] buzzerInputs;
    int players;
    PlayerFunctions pf;

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
        pf = FindObjectOfType<PlayerFunctions>();
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
            key.SetActive(true);
            roundButton.SetActive(false);
            //Get new command
            playing = true;

            for (int i = 0; i < players; i++)
            {
                playerCurQuestion[i] = pf.playerScoresText[i].catImpImage;
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
                        GameManager.instance.PlayRandomSound(GameManager.instance.categoricalCorrectSounds);
                        CorrectAnswer(i);
                    }
                    else
                    {
                        GameManager.instance.PlayRandomSound(GameManager.instance.categoricalIncorrectSounds);
                        IncorrectAnswer(i);
                    }
                }
            }

            //Check for player inputs in here
            for (int i = 0; i < players; i++)
            {
                buzzerInputs[i] = Input.GetButtonDown("Buzz" + (i + 1).ToString());

                if (buzzerInputs[i])
                {
                    //Play buzz sound
                    GameManager.instance.PlaySound(GameManager.instance.buzzSnd, 0.6f, true);

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
            pf.playerScoresText[p].catImpAnim.Play("CatImpRight");
        }
        else
        {
            //Win game
            GameManager.instance.playerFunctions.playerScoresText[p].Winner();
            GameManager.instance.menuFunctions.playerScores[p].Winner();
            playing = false;
        }
    }

    void IncorrectAnswer(int p)
    {
        if (curQuestionIndex[p] > 0)
        {
            curQuestionIndex[p] = 0;

            playerCurAnswer[p] = answers[curQuestionIndex[p]];
            playerCurQuestion[p].sprite = questions[curQuestionIndex[p]];
            pf.playerScoresText[p].catImpAnim.Play("CatImpWrong");
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
