using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGameFunctions : MonoBehaviour
{
    public PlayerScoreButton[] playerScores;

    public int curKeycard = 0;
    public List<EndGameImageController> player1Keycards;
    public List<EndGameImageController> player2Keycards;
    public List<EndGameImageController> player3Keycards;
    public List<EndGameImageController> player4Keycards;
    public List<EndGameImageController> player5Keycards;
    public List<EndGameImageController> player6Keycards;
    public List<EndGameImageController> curKeycards;
    public int keycardSlot = 0;
    public bool finished = true;

    public bool[] buzzerInputs;
    public Vector2[] joystickInputs;
    int players;
    float selectCools;
    public float timeBetweenSelects = 0.2f;

    public TMP_Text timerText;
    public float keycardSelectTime;
    float timer;

    void Start()
    {
        players = GameManager.instance.players;
        //GameManager.instance.ShowEndgame();
        GameManager.instance.endgame = this;
        curKeycards = player1Keycards;
        curKeycards[keycardSlot].highlightImage.color = curKeycards[keycardSlot].highlightColor;
        curKeycards[keycardSlot].ActivateArrows();
    }

    public void StartTimer()
    {
        timer = keycardSelectTime;
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

        if (timer > 0)
        {
            timer -= Time.deltaTime;

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
        if (keycardSlot < curKeycards.Count - 1)
        {
            curKeycards[keycardSlot].highlightImage.color = curKeycards[keycardSlot].unhighlightColor;
            curKeycards[keycardSlot].DeactivateArrows();
            keycardSlot++;
            curKeycards[keycardSlot].highlightImage.color = curKeycards[keycardSlot].highlightColor;
            curKeycards[keycardSlot].ActivateArrows();
        }
        else
        {
            curKeycards[keycardSlot].DeactivateArrows();
            curKeycards[keycardSlot].highlightImage.color = curKeycards[keycardSlot].unhighlightColor;
            keycardSlot = 0;
            curKeycards[keycardSlot].ActivateArrows();
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
            keycardSlot = curKeycards.Count - 1;
            curKeycards[keycardSlot].highlightImage.color = curKeycards[keycardSlot].highlightColor;
        }
    }

    public void NextImage()
    {
        curKeycards[keycardSlot].NextImage();
    }

    public void ConfirmGuess()
    {
        for (int i = 0; i < curKeycards.Count; i++)
        {
            if (GameManager.instance.finalCodes[curKeycard].keys.Contains(curKeycards[i].keyImage.sprite))
            {
                if (GameManager.instance.finalCodes[curKeycard].keys.IndexOf(curKeycards[i].keyImage.sprite) == i)
                {
                    curKeycards[i].CorrectImage();
                }
                else curKeycards[i].WrongSpotImage();
            }
            else curKeycards[i].IncorrectImage();
        }
    }

    public void QuitToMenu()
    {
        GameManager.instance.ResetGame();
    }
}
