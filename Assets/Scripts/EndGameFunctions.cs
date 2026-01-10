using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameFunctions : MonoBehaviour
{
    public PlayerScoreButton[] playerScores;

    public int curKeycard = 0;
    public EndGameImageController[] player1Keycards;
    public EndGameImageController[] player2Keycards;
    public EndGameImageController[] player3Keycards;
    public EndGameImageController[] player4Keycards;
    public EndGameImageController[] player5Keycards;
    public EndGameImageController[] player6Keycards;
    public EndGameImageController[] curKeycards;
    public int keycardSlot = 0;
    public bool finished = true;

    public bool[] buzzerInputs;
    public Vector2[] joystickInputs;
    int players;
    float selectCools;
    public float timeBetweenSelects = 0.2f;

    void Start()
    {
        players = GameManager.instance.players;
        //GameManager.instance.ShowEndgame();
        GameManager.instance.endgame = this;
        curKeycards = player1Keycards;
        curKeycards[keycardSlot].highlightImage.color = curKeycards[keycardSlot].highlightColor;
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                ConfirmGuess();
            }
        }

        //Check for player inputs in here
        for (int i = 0; i < players; i++)
        {
            joystickInputs[i] = new Vector2(Mathf.RoundToInt(Input.GetAxisRaw("Horizontal" + (i + 1).ToString())), Mathf.RoundToInt(Input.GetAxisRaw("Vertical" + (i + 1).ToString())));
            //Debug.Log(joystickInputs[i]);
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

        if (selectCools > 0) selectCools -= Time.deltaTime;

        //If the current player pushes up or down, change the image
        if (joystickInputs[curKeycard].y != 0 && selectCools <= 0)
        {
            NextImage();
            selectCools = timeBetweenSelects;
        }

        //Move left or right to select different keycards
        if (joystickInputs[curKeycard].x > 0 && selectCools <= 0)
        {
            NextKeycard();
            selectCools = timeBetweenSelects;
            
        }
        if (joystickInputs[curKeycard].x < 0 && selectCools <= 0)
        {
            PreviousKeycard();
            selectCools = timeBetweenSelects;
        }

        if (buzzerInputs[curKeycard])
        {
            ConfirmGuess();
        }
    }

    public void NextPlayer()
    {
        if (!finished)
        {
            if (curKeycards == player1Keycards)
            {
                curKeycards[keycardSlot].highlightImage.color = curKeycards[keycardSlot].unhighlightColor;
                keycardSlot = 0;
                curKeycards = player2Keycards;
                curKeycard = 1;
            }
            else if (curKeycards == player2Keycards)
            {
                curKeycards[keycardSlot].highlightImage.color = curKeycards[keycardSlot].unhighlightColor;
                keycardSlot = 0;
                curKeycards = player3Keycards;
                curKeycard = 2;
            }
            else if (curKeycards == player3Keycards)
            {
                curKeycards[keycardSlot].highlightImage.color = curKeycards[keycardSlot].unhighlightColor;
                keycardSlot = 0;
                curKeycards = player4Keycards;
                curKeycard = 3;
            }
            else if (curKeycards == player4Keycards)
            {
                curKeycards[keycardSlot].highlightImage.color = curKeycards[keycardSlot].unhighlightColor;
                keycardSlot = 0;
                curKeycards = player5Keycards;
                curKeycard = 4;
            }
            else if (curKeycards == player5Keycards)
            {
                curKeycards[keycardSlot].highlightImage.color = curKeycards[keycardSlot].unhighlightColor;
                keycardSlot = 0;
                curKeycards = player6Keycards;
                curKeycard = 5;
            }
            else
            {
                curKeycards[keycardSlot].highlightImage.color = curKeycards[keycardSlot].unhighlightColor;
                keycardSlot = 0;
                curKeycards = player1Keycards;
                curKeycard = 0;
                finished = true;
            }
        }
    }

    public void NextKeycard()
    {
        if (keycardSlot < curKeycards.Length - 1)
        {
            curKeycards[keycardSlot].highlightImage.color = curKeycards[keycardSlot].unhighlightColor;
            keycardSlot++;
            curKeycards[keycardSlot].highlightImage.color = curKeycards[keycardSlot].highlightColor;
        }
        else
        {
            curKeycards[keycardSlot].highlightImage.color = curKeycards[keycardSlot].unhighlightColor;
            keycardSlot = 0;
            curKeycards[keycardSlot].highlightImage.color = curKeycards[keycardSlot].highlightColor;
        }
    }

    public void PreviousKeycard()
    {
        if (keycardSlot > 0)
        {
            curKeycards[keycardSlot].highlightImage.color = curKeycards[keycardSlot].unhighlightColor;
            keycardSlot--;
            curKeycards[keycardSlot].highlightImage.color = curKeycards[keycardSlot].highlightColor;
        }
        else
        {
            curKeycards[keycardSlot].highlightImage.color = curKeycards[keycardSlot].unhighlightColor;
            keycardSlot = curKeycards.Length - 1;
            curKeycards[keycardSlot].highlightImage.color = curKeycards[keycardSlot].highlightColor;
        }
    }

    public void NextImage()
    {
        curKeycards[keycardSlot].NextImage();
    }

    public void ConfirmGuess()
    {
        switch(curKeycard)
        {
            case 0:
                for (int i = 0; i < curKeycards.Length; i++)
                {
                    if (GameManager.instance.finalCode1[i] == curKeycards[i].keyImage.sprite)
                    {
                        curKeycards[i].CorrectImage();
                    }
                    else curKeycards[i].IncorrectImage();
                }
                break;

        }
    }

    public void QuitToMenu()
    {
        GameManager.instance.ResetGame();
    }
}
