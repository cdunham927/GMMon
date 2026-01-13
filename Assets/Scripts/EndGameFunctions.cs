using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGameFunctions : MonoBehaviour
{
    public PlayerScoreButton[] playerScores;

    //public int curKeycard = 0;
    public List<EndGameImageController> player1Keycards;
    public List<EndGameImageController> player2Keycards;
    public List<EndGameImageController> player3Keycards;
    public List<EndGameImageController> player4Keycards;
    public List<EndGameImageController> player5Keycards;
    public List<EndGameImageController> player6Keycards;
    public int[] keycardSlot;
    public bool finished = true;

    public bool[] buzzerInputs;
    public Vector2[] joystickInputs;
    int players;
    float[] selectCools;
    public float timeBetweenSelects = 0.2f;

    //public TMP_Text currentPlayerText;
    //public TMP_Text currentPlayerText2;
    public TMP_Text timerText;
    public TMP_Text timerText2;
    public float keycardSelectTime;
    float timer;

    public enum endgameFlow { intro, assign, rules, hints, start, finalHelp, end };
    public endgameFlow endPosition;

    public GameObject introText;
    public GameObject assignText;
    public GameObject rulesText;
    public GameObject hintsText;
    public GameObject startText;
    public GameObject finalHelpText;
    public GameObject endText;
    public GameObject endButton;

    void Start()
    {
        players = GameManager.instance.players;
        System.Array.Resize(ref selectCools, players);
        //GameManager.instance.ShowEndgame();
        GameManager.instance.endgame = this;
    }

    void ActivateKeycards()
    {
        //Activate random keycards
        int rand1 = Random.Range(0, player1Keycards.Count);
        int rand2 = Random.Range(0, player2Keycards.Count);
        int rand3 = Random.Range(0, player3Keycards.Count);
        int rand4 = Random.Range(0, player4Keycards.Count);
        int rand5 = Random.Range(0, player5Keycards.Count);
        int rand6 = Random.Range(0, player6Keycards.Count);

        player1Keycards[rand1].highlightImage.color = player1Keycards[rand1].highlightColor;
        player1Keycards[rand1].ActivateArrows();
        player2Keycards[rand2].highlightImage.color = player2Keycards[rand2].highlightColor;
        player2Keycards[rand2].ActivateArrows();
        player3Keycards[rand3].highlightImage.color = player3Keycards[rand3].highlightColor;
        player3Keycards[rand3].ActivateArrows();
        player4Keycards[rand4].highlightImage.color = player4Keycards[rand4].highlightColor;
        player4Keycards[rand4].ActivateArrows();
        player5Keycards[rand5].highlightImage.color = player5Keycards[rand5].highlightColor;
        player5Keycards[rand5].ActivateArrows();
        player6Keycards[rand6].highlightImage.color = player6Keycards[rand6].highlightColor;
        player6Keycards[rand6].ActivateArrows();

        keycardSlot[0] = rand1;
        keycardSlot[1] = rand2;
        keycardSlot[2] = rand3;
        keycardSlot[3] = rand4;
        keycardSlot[4] = rand5;
        keycardSlot[5] = rand6;
    }

    public void Next()
    {
        endPosition++;

        switch (endPosition)
        {
            case endgameFlow.intro:
                break;
            case endgameFlow.assign:
                ShowAssign();
                break;
            case endgameFlow.rules:
                ShowRules();
                break;
            case endgameFlow.hints:
                ShowHints();
                break;
            case endgameFlow.start:
                ShowStart();
                break;
            case endgameFlow.finalHelp:
                ShowFinalHelp();
                break;
            case endgameFlow.end:
                ShowEnd();
                break;
        }
    }

    public void ShowAssign()
    {
        introText.SetActive(false);
        assignText.SetActive(true);
    }

    public void ShowRules()
    {
        assignText.SetActive(false);
        rulesText.SetActive(true);
    }

    public void ShowHints()
    {
        rulesText.SetActive(false);
        hintsText.SetActive(true);
    }
    public void ShowStart()
    {
        hintsText.SetActive(false);
        StartTimer();
        ActivateKeycards();
    }

    public void ShowFinalHelp()
    {
        finalHelpText.SetActive(true);
    }

    public void ShowEnd()
    {
        finalHelpText.SetActive(false);
        endText.SetActive(true);
        endButton.SetActive(true);
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
                ConfirmGuessAll();
            }
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;

            //Show time remaining
            int seconds = ((int)timer % 60);
            int minutes = ((int)timer / 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            timerText2.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            //Check for player inputs in here
            for (int i = 0; i < players; i++)
            {
                joystickInputs[i] = new Vector2(Mathf.RoundToInt(Input.GetAxisRaw("Horizontal" + (i + 1).ToString())), Mathf.RoundToInt(Input.GetAxisRaw("Vertical" + (i + 1).ToString())));
                //Debug.Log(joystickInputs[i]);


                //If the current player pushes up or down, change the image
                if (joystickInputs[i].y != 0 && selectCools[i] <= 0)
                {
                    NextImage(i);
                    selectCools[i] = timeBetweenSelects;
                }

                //Move left or right to select different keycards
                if (joystickInputs[i].x > 0 && selectCools[i] <= 0)
                {
                    NextKeycard(i);
                    selectCools[i] = timeBetweenSelects;

                }
                if (joystickInputs[i].x < 0 && selectCools[i] <= 0)
                {
                    PreviousKeycard(i);
                    selectCools[i] = timeBetweenSelects;
                }


                if (selectCools[i] > 0) selectCools[i] -= Time.deltaTime;
            }

            //Check for player inputs in here
            for (int i = 0; i < players; i++)
            {
                buzzerInputs[i] = Input.GetButtonDown("Buzz" + (i + 1).ToString());

                if (buzzerInputs[i])
                {
                    //Play buzz sound
                    GameManager.instance.PlaySound(GameManager.instance.buzzSnd, 0.6f, true);

                    ConfirmGuess(i);
                }
            }

            //currentPlayerText.text = "Player " + (curKeycard + 1).ToString() + " Turn";
            //currentPlayerText2.text = "Player " + (curKeycard + 1).ToString() + " Turn";
        }
        else
        {
            //currentPlayerText.text = "";
            //currentPlayerText2.text = "";
            //timerText.text = "";
            //timerText2.text = "";
        }
    }

    //public void NextPlayer()
    //{
    //    if (!finished)
    //    {
    //        if (curKeycards == player1Keycards)
    //        {
    //            curKeycards[keycardSlot].highlightImage.color = curKeycards[keycardSlot].unhighlightColor;
    //            keycardSlot = 0;
    //            curKeycards = player2Keycards;
    //            curKeycard = 1;
    //        }
    //        else if (curKeycards == player2Keycards)
    //        {
    //            curKeycards[keycardSlot].highlightImage.color = curKeycards[keycardSlot].unhighlightColor;
    //            keycardSlot = 0;
    //            curKeycards = player3Keycards;
    //            curKeycard = 2;
    //        }
    //        else if (curKeycards == player3Keycards)
    //        {
    //            curKeycards[keycardSlot].highlightImage.color = curKeycards[keycardSlot].unhighlightColor;
    //            keycardSlot = 0;
    //            curKeycards = player4Keycards;
    //            curKeycard = 3;
    //        }
    //        else if (curKeycards == player4Keycards)
    //        {
    //            curKeycards[keycardSlot].highlightImage.color = curKeycards[keycardSlot].unhighlightColor;
    //            keycardSlot = 0;
    //            curKeycards = player5Keycards;
    //            curKeycard = 4;
    //        }
    //        else if (curKeycards == player5Keycards)
    //        {
    //            curKeycards[keycardSlot].highlightImage.color = curKeycards[keycardSlot].unhighlightColor;
    //            keycardSlot = 0;
    //            curKeycards = player6Keycards;
    //            curKeycard = 5;
    //        }
    //        else
    //        {
    //            curKeycards[keycardSlot].highlightImage.color = curKeycards[keycardSlot].unhighlightColor;
    //            keycardSlot = 0;
    //            curKeycards = player1Keycards;
    //            curKeycard = 0;
    //            finished = true;
    //        }
    //    }
    //}

    public void NextKeycard(int p)
    {
        switch(p)
        {
            case 0:
                if (keycardSlot[p] < player1Keycards.Count - 1)
                {
                    player1Keycards[keycardSlot[p]].highlightImage.color = player1Keycards[keycardSlot[p]].unhighlightColor;
                    player1Keycards[keycardSlot[p]].DeactivateArrows();
                    keycardSlot[p]++;
                    player1Keycards[keycardSlot[p]].highlightImage.color = player1Keycards[keycardSlot[p]].highlightColor;
                    player1Keycards[keycardSlot[p]].ActivateArrows();
                }
                else
                {
                    player1Keycards[keycardSlot[p]].highlightImage.color = player1Keycards[keycardSlot[p]].unhighlightColor;
                    player1Keycards[keycardSlot[p]].DeactivateArrows();
                    keycardSlot[p] = 0;
                    player1Keycards[keycardSlot[p]].highlightImage.color = player1Keycards[keycardSlot[p]].highlightColor;
                    player1Keycards[keycardSlot[p]].ActivateArrows();
                }
                break;
            case 1:
                if (keycardSlot[p] < player2Keycards.Count - 1)
                {
                    player2Keycards[keycardSlot[p]].highlightImage.color = player2Keycards[keycardSlot[p]].unhighlightColor;
                    player2Keycards[keycardSlot[p]].DeactivateArrows();
                    keycardSlot[p]++;
                    player2Keycards[keycardSlot[p]].highlightImage.color = player2Keycards[keycardSlot[p]].highlightColor;
                    player2Keycards[keycardSlot[p]].ActivateArrows();
                }
                else
                {
                    player2Keycards[keycardSlot[p]].highlightImage.color = player2Keycards[keycardSlot[p]].unhighlightColor;
                    player2Keycards[keycardSlot[p]].DeactivateArrows();
                    keycardSlot[p] = 0;
                    player2Keycards[keycardSlot[p]].highlightImage.color = player2Keycards[keycardSlot[p]].highlightColor;
                    player2Keycards[keycardSlot[p]].ActivateArrows();
                }
                break;
            case 2:
                if (keycardSlot[p] < player3Keycards.Count - 1)
                {
                    player3Keycards[keycardSlot[p]].highlightImage.color = player3Keycards[keycardSlot[p]].unhighlightColor;
                    player3Keycards[keycardSlot[p]].DeactivateArrows();
                    keycardSlot[p]++;
                    player3Keycards[keycardSlot[p]].highlightImage.color = player3Keycards[keycardSlot[p]].highlightColor;
                    player3Keycards[keycardSlot[p]].ActivateArrows();
                }
                else
                {
                    player3Keycards[keycardSlot[p]].highlightImage.color = player3Keycards[keycardSlot[p]].unhighlightColor;
                    player3Keycards[keycardSlot[p]].DeactivateArrows();
                    keycardSlot[p] = 0;
                    player3Keycards[keycardSlot[p]].highlightImage.color = player3Keycards[keycardSlot[p]].highlightColor;
                    player3Keycards[keycardSlot[p]].ActivateArrows();
                }
                break;
            case 3:
                if (keycardSlot[p] < player4Keycards.Count - 1)
                {
                    player4Keycards[keycardSlot[p]].highlightImage.color = player4Keycards[keycardSlot[p]].unhighlightColor;
                    player4Keycards[keycardSlot[p]].DeactivateArrows();
                    keycardSlot[p]++;
                    player4Keycards[keycardSlot[p]].highlightImage.color = player4Keycards[keycardSlot[p]].highlightColor;
                    player4Keycards[keycardSlot[p]].ActivateArrows();
                }
                else
                {
                    player4Keycards[keycardSlot[p]].highlightImage.color = player4Keycards[keycardSlot[p]].unhighlightColor;
                    player4Keycards[keycardSlot[p]].DeactivateArrows();
                    keycardSlot[p] = 0;
                    player4Keycards[keycardSlot[p]].highlightImage.color = player4Keycards[keycardSlot[p]].highlightColor;
                    player4Keycards[keycardSlot[p]].ActivateArrows();
                }
                break;
            case 4:
                if (keycardSlot[p] < player5Keycards.Count - 1)
                {
                    player5Keycards[keycardSlot[p]].highlightImage.color = player5Keycards[keycardSlot[p]].unhighlightColor;
                    player5Keycards[keycardSlot[p]].DeactivateArrows();
                    keycardSlot[p]++;
                    player5Keycards[keycardSlot[p]].highlightImage.color = player5Keycards[keycardSlot[p]].highlightColor;
                    player5Keycards[keycardSlot[p]].ActivateArrows();
                }
                else
                {
                    player5Keycards[keycardSlot[p]].highlightImage.color = player5Keycards[keycardSlot[p]].unhighlightColor;
                    player5Keycards[keycardSlot[p]].DeactivateArrows();
                    keycardSlot[p] = 0;
                    player5Keycards[keycardSlot[p]].highlightImage.color = player5Keycards[keycardSlot[p]].highlightColor;
                    player5Keycards[keycardSlot[p]].ActivateArrows();
                }
                break;
            case 5:
                if (keycardSlot[p] < player6Keycards.Count - 1)
                {
                    player6Keycards[keycardSlot[p]].highlightImage.color = player6Keycards[keycardSlot[p]].unhighlightColor;
                    player6Keycards[keycardSlot[p]].DeactivateArrows();
                    keycardSlot[p]++;
                    player6Keycards[keycardSlot[p]].highlightImage.color = player6Keycards[keycardSlot[p]].highlightColor;
                    player6Keycards[keycardSlot[p]].ActivateArrows();
                }
                else
                {
                    player6Keycards[keycardSlot[p]].highlightImage.color = player6Keycards[keycardSlot[p]].unhighlightColor;
                    player6Keycards[keycardSlot[p]].DeactivateArrows();
                    keycardSlot[p] = 0;
                    player6Keycards[keycardSlot[p]].highlightImage.color = player6Keycards[keycardSlot[p]].highlightColor;
                    player6Keycards[keycardSlot[p]].ActivateArrows();
                }
                break;
        }
    }

    public void PreviousKeycard(int p)
    {
        switch (p)
        {
            case 0:
                if (keycardSlot[p] > 0)
                {
                    player1Keycards[keycardSlot[p]].highlightImage.color = player1Keycards[keycardSlot[p]].unhighlightColor;
                    player1Keycards[keycardSlot[p]].DeactivateArrows();
                    keycardSlot[p]--;
                    player1Keycards[keycardSlot[p]].highlightImage.color = player1Keycards[keycardSlot[p]].highlightColor;
                    player1Keycards[keycardSlot[p]].ActivateArrows();
                }
                else
                {
                    player1Keycards[keycardSlot[p]].highlightImage.color = player1Keycards[keycardSlot[p]].unhighlightColor;
                    player1Keycards[keycardSlot[p]].DeactivateArrows();
                    keycardSlot[p] = player1Keycards.Count - 1;
                    player1Keycards[keycardSlot[p]].highlightImage.color = player1Keycards[keycardSlot[p]].highlightColor;
                    player1Keycards[keycardSlot[p]].ActivateArrows();
                }
                break;
            case 1:
                if (keycardSlot[p] > 0)
                {
                    player2Keycards[keycardSlot[p]].highlightImage.color = player2Keycards[keycardSlot[p]].unhighlightColor;
                    player2Keycards[keycardSlot[p]].DeactivateArrows();
                    keycardSlot[p]--;
                    player2Keycards[keycardSlot[p]].highlightImage.color = player2Keycards[keycardSlot[p]].highlightColor;
                    player2Keycards[keycardSlot[p]].ActivateArrows();
                }
                else
                {
                    player2Keycards[keycardSlot[p]].highlightImage.color = player2Keycards[keycardSlot[p]].unhighlightColor;
                    player2Keycards[keycardSlot[p]].DeactivateArrows();
                    keycardSlot[p] = player2Keycards.Count - 1;
                    player2Keycards[keycardSlot[p]].highlightImage.color = player2Keycards[keycardSlot[p]].highlightColor;
                    player2Keycards[keycardSlot[p]].ActivateArrows();
                }
                break;
            case 2:
                if (keycardSlot[p] > 0)
                {
                    player3Keycards[keycardSlot[p]].highlightImage.color = player3Keycards[keycardSlot[p]].unhighlightColor;
                    player3Keycards[keycardSlot[p]].DeactivateArrows();
                    keycardSlot[p]++;
                    player3Keycards[keycardSlot[p]].highlightImage.color = player3Keycards[keycardSlot[p]].highlightColor;
                    player3Keycards[keycardSlot[p]].ActivateArrows();
                }
                else
                {
                    player3Keycards[keycardSlot[p]].highlightImage.color = player3Keycards[keycardSlot[p]].unhighlightColor;
                    player3Keycards[keycardSlot[p]].DeactivateArrows();
                    keycardSlot[p] = player3Keycards.Count - 1;
                    player3Keycards[keycardSlot[p]].highlightImage.color = player3Keycards[keycardSlot[p]].highlightColor;
                    player3Keycards[keycardSlot[p]].ActivateArrows();
                }
                break;
            case 3:
                if (keycardSlot[p] > 0)
                {
                    player4Keycards[keycardSlot[p]].highlightImage.color = player4Keycards[keycardSlot[p]].unhighlightColor;
                    player4Keycards[keycardSlot[p]].DeactivateArrows();
                    keycardSlot[p]++;
                    player4Keycards[keycardSlot[p]].highlightImage.color = player4Keycards[keycardSlot[p]].highlightColor;
                    player4Keycards[keycardSlot[p]].ActivateArrows();
                }
                else
                {
                    player4Keycards[keycardSlot[p]].highlightImage.color = player4Keycards[keycardSlot[p]].unhighlightColor;
                    player4Keycards[keycardSlot[p]].DeactivateArrows();
                    keycardSlot[p] = player4Keycards.Count - 1;
                    player4Keycards[keycardSlot[p]].highlightImage.color = player4Keycards[keycardSlot[p]].highlightColor;
                    player4Keycards[keycardSlot[p]].ActivateArrows();
                }
                break;
            case 4:
                if (keycardSlot[p] > 0)
                {
                    player5Keycards[keycardSlot[p]].highlightImage.color = player5Keycards[keycardSlot[p]].unhighlightColor;
                    player5Keycards[keycardSlot[p]].DeactivateArrows();
                    keycardSlot[p]++;
                    player5Keycards[keycardSlot[p]].highlightImage.color = player5Keycards[keycardSlot[p]].highlightColor;
                    player5Keycards[keycardSlot[p]].ActivateArrows();
                }
                else
                {
                    player5Keycards[keycardSlot[p]].highlightImage.color = player5Keycards[keycardSlot[p]].unhighlightColor;
                    player5Keycards[keycardSlot[p]].DeactivateArrows();
                    keycardSlot[p] = player5Keycards.Count - 1;
                    player5Keycards[keycardSlot[p]].highlightImage.color = player5Keycards[keycardSlot[p]].highlightColor;
                    player5Keycards[keycardSlot[p]].ActivateArrows();
                }
                break;
            case 5:
                if (keycardSlot[p] > 0)
                {
                    player6Keycards[keycardSlot[p]].highlightImage.color = player6Keycards[keycardSlot[p]].unhighlightColor;
                    player6Keycards[keycardSlot[p]].DeactivateArrows();
                    keycardSlot[p]++;
                    player6Keycards[keycardSlot[p]].highlightImage.color = player6Keycards[keycardSlot[p]].highlightColor;
                    player6Keycards[keycardSlot[p]].ActivateArrows();
                }
                else
                {
                    player6Keycards[keycardSlot[p]].highlightImage.color = player6Keycards[keycardSlot[p]].unhighlightColor;
                    player6Keycards[keycardSlot[p]].DeactivateArrows();
                    keycardSlot[p] = player6Keycards.Count - 1;
                    player6Keycards[keycardSlot[p]].highlightImage.color = player6Keycards[keycardSlot[p]].highlightColor;
                    player6Keycards[keycardSlot[p]].ActivateArrows();
                }
                break;
        }
    }

    public void NextImage(int p)
    {
        switch(p)
        {
            case 0:
                player1Keycards[keycardSlot[p]].NextImage();
                break;
            case 1:
                player2Keycards[keycardSlot[p]].NextImage();
                break;
            case 2:
                player3Keycards[keycardSlot[p]].NextImage();
                break;
            case 3:
                player4Keycards[keycardSlot[p]].NextImage();
                break;
            case 4:
                player5Keycards[keycardSlot[p]].NextImage();
                break;
            case 5:
                player6Keycards[keycardSlot[p]].NextImage();
                break;
        }
    }

    public void ConfirmGuessAll()
    {
        for (int i = 0; i < 6; i++)
        {
            ConfirmGuess(i);
        }
    }

    public void ConfirmGuess(int p)
    {
        switch (p)
        {
            case 0:
                for (int i = 0; i < player1Keycards.Count; i++)
                {
                    if (GameManager.instance.finalCodes[p].keys.Contains(player1Keycards[i].keyImage.sprite))
                    {
                        if (GameManager.instance.finalCodes[p].keys.IndexOf(player1Keycards[i].keyImage.sprite) == i)
                        {
                            player1Keycards[i].CorrectImage();
                        }
                        else player1Keycards[i].WrongSpotImage();
                    }
                    else player1Keycards[i].IncorrectImage();
                }
                break;
            case 1:
                for (int i = 0; i < player2Keycards.Count; i++)
                {
                    if (GameManager.instance.finalCodes[p].keys.Contains(player2Keycards[i].keyImage.sprite))
                    {
                        if (GameManager.instance.finalCodes[p].keys.IndexOf(player2Keycards[i].keyImage.sprite) == i)
                        {
                            player2Keycards[i].CorrectImage();
                        }
                        else player2Keycards[i].WrongSpotImage();
                    }
                    else player2Keycards[i].IncorrectImage();
                }
                break;
            case 2:
                for (int i = 0; i < player3Keycards.Count; i++)
                {
                    if (GameManager.instance.finalCodes[p].keys.Contains(player3Keycards[i].keyImage.sprite))
                    {
                        if (GameManager.instance.finalCodes[p].keys.IndexOf(player3Keycards[i].keyImage.sprite) == i)
                        {
                            player3Keycards[i].CorrectImage();
                        }
                        else player3Keycards[i].WrongSpotImage();
                    }
                    else player3Keycards[i].IncorrectImage();
                }
                break;
            case 3:
                for (int i = 0; i < player4Keycards.Count; i++)
                {
                    if (GameManager.instance.finalCodes[p].keys.Contains(player4Keycards[i].keyImage.sprite))
                    {
                        if (GameManager.instance.finalCodes[p].keys.IndexOf(player4Keycards[i].keyImage.sprite) == i)
                        {
                            player4Keycards[i].CorrectImage();
                        }
                        else player4Keycards[i].WrongSpotImage();
                    }
                    else player4Keycards[i].IncorrectImage();
                }
                break;
            case 4:
                for (int i = 0; i < player5Keycards.Count; i++)
                {
                    if (GameManager.instance.finalCodes[p].keys.Contains(player5Keycards[i].keyImage.sprite))
                    {
                        if (GameManager.instance.finalCodes[p].keys.IndexOf(player5Keycards[i].keyImage.sprite) == i)
                        {
                            player5Keycards[i].CorrectImage();
                        }
                        else player5Keycards[i].WrongSpotImage();
                    }
                    else player5Keycards[i].IncorrectImage();
                }
                break;
            case 5:
                for (int i = 0; i < player6Keycards.Count; i++)
                {
                    if (GameManager.instance.finalCodes[p].keys.Contains(player6Keycards[i].keyImage.sprite))
                    {
                        if (GameManager.instance.finalCodes[p].keys.IndexOf(player6Keycards[i].keyImage.sprite) == i)
                        {
                            player6Keycards[i].CorrectImage();
                        }
                        else player6Keycards[i].WrongSpotImage();
                    }
                    else player6Keycards[i].IncorrectImage();
                }
                break;
        }
}

    public void QuitToMenu()
    {
        GameManager.instance.ResetGame();
    }
}
