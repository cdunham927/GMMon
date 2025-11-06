using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ReflexCompetencyController : MonoBehaviour
{
    public Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    public Sprite[] directionsSprites = { };
    public Vector2 currentDirection;

    public Sprite blankSprite;

    public Image commandImage;
    public Image commandImage2;

    public float roundTimeSmall = 2f;
    public float roundTimeBig = 10f;
    float curTime;
    bool countdown = false;
    //public TMP_Text timeRemaining;
    //public TMP_Text timeRemaining2;

    float curReflexTime = 0f;
    public float reflexTime = 1f;

    int players;
    public bool[] checkedInput;
    public int[] playerPoints;
    public Vector2[] joystickInputs;
    public int pointsToEnd = 5;
    bool end = false;

    private void Awake()
    {
        countdown = false;
        players = GameManager.instance.players;

        System.Array.Resize(ref joystickInputs, players);
        System.Array.Resize(ref checkedInput, players);
        System.Array.Resize(ref playerPoints, players);
    }

    public void StartRound()
    {
        countdown = true;
        curTime = Random.Range(roundTimeSmall, roundTimeBig);
    }

    void CheckInput(int p, Vector2 inp)
    {
        if (inp == currentDirection)
        {
            GivePoint(p);
        }
        else
        {
            TakePoint(p);
        }
    }

    void GivePoint(int p)
    {
        GameManager.instance.playerScores[p]++;
        playerPoints[p]++;

        if (playerPoints[p] >= pointsToEnd) end = true;
    }

    void TakePoint(int p)
    {
        GameManager.instance.playerScores[p]--;
        playerPoints[p]--;
    }

    private void Update()
    {
        if (!end)
        {
            //Countdown to new direction
            if (countdown && curTime > 0) curTime -= Time.deltaTime;

            //Get and show new direction
            if (countdown && curTime <= 0)
            {
                countdown = false;

                curReflexTime = reflexTime;

                GetInstruction();
            }

            //During the round
            if (curTime <= 0 && curReflexTime > 0)
            {
                curReflexTime -= Time.deltaTime;

                //Check for player inputs in here
                for (int i = 0; i < players; i++)
                {
                    if (!checkedInput[i] && joystickInputs[i] != Vector2.zero)
                    {
                        checkedInput[i] = true;
                        CheckInput(i, joystickInputs[i]);
                    }
                }
            }

            //Round is over, check for any player that hasnt input yet and take away points
            if (curTime <= 0 && curReflexTime < 0)
            {
                commandImage.sprite = blankSprite;
                commandImage2.sprite = blankSprite;

                //Deduct points from no inputs from players
                for (int i = 0; i < players; i++)
                {
                    if (!checkedInput[i]) TakePoint(i);

                    checkedInput[i] = false;
                }

                //Start new round
                countdown = true;
                curTime = Random.Range(roundTimeSmall, roundTimeBig);
            }

            //Check for player inputs in here
            for (int i = 0; i < players; i++)
            {
                joystickInputs[i] = new Vector2(Mathf.RoundToInt(Input.GetAxisRaw("Horizontal" + (i + 1).ToString())), Mathf.RoundToInt(Input.GetAxisRaw("Vertical" + (i + 1).ToString())));
                //Debug.Log(joystickInputs[i]);
            }
        }
    }

    public void GetInstruction()
    {
        int index = Random.Range(0, directionsSprites.Length);
        currentDirection = directions[index];

        commandImage.sprite = directionsSprites[index];
        commandImage2.sprite = directionsSprites[index];
    }
}
