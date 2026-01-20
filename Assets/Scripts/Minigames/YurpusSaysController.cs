using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//public class YurpusSaysController : IPlayerOut
public class YurpusSaysController : MonoBehaviour
{
    //public string[] commands = { "Up", "Down", "Left", "Right", "Not" };
    //public string[] yurpusCommands = { "Glorp", "Norf", "Mim", "Farn", "Zerp" };
    public List<string> fullCommands = new List<string>();
    public List<string> fullYurpusCommands = new List<string>();
    public List<Sprite> fullCommandImages = new List<Sprite>();
    Vector2 commandDirection;

    public GameObject hintObj;
    public TMP_Text commandText;
    public TMP_Text commandText2;
    public TMP_Text yurpusCommandText;

    public Image commandImage;
    public Image commandImage2;

    public GameObject playerHudText;

    public bool multiRound;

    public bool[] buzzerInputs;
    public int[] playerPoints;
    public Vector2[] joystickInputs;
    //public float[] checkedAnswer;
    public bool[] checkedAnswer;
    //public float checkTime = 0.2f;
    int players;
    //public Vector2[] playerCurAnswer = { };
    //public Image[] playerCurQuestion = { };

    float curTime;
    bool countdown = false;

    float curReflexTime = 0f;
    public float reflexTime = 1f;

    public float roundTimeSmall, roundTimeBig;
    public int pointsToEnd = 5;
    public bool end = false;

    private void Awake()
    {
        players = GameManager.instance.players;
        System.Array.Resize(ref joystickInputs, players);
        System.Array.Resize(ref playerPoints, players);
        //ListExtensions.Shuffle(fullCommands);
    }

    private void Start()
    {
        //curPlace = GameManager.instance.players;
        multiRound = true;
    }

    public void NextRound()
    {
        countdown = true;
        curTime = Random.Range(roundTimeSmall, roundTimeBig);
        //Need to keep a list of all playerscorebuttons currently clicked
        //Update all their places to be plus 1 of current included players(curPlace)
        //IE instead of 3 8th place players they become 3 5th place ones
        //Clear the list after we update them
        //Also have a bool in the playerscorebutton that prevents us from unclicking after we have gone to the next round

        //round++;
        //if (round == 3 && hintObj != null) hintObj.SetActive(false);
        //if (eliminatedPlayer > 0) curPlace -= eliminatedPlayer;
        // eliminatedPlayer = 0;

        //if (curPlace <= 1)
        //{
        //    playerHudText.SetActive(false);
        //    GameManager.instance.ShowEndgame();
        //}
    }

    private void Update()
    {
        //Countdown to new direction
        if (countdown && curTime > 0) curTime -= Time.deltaTime;

        //Check for player inputs in here
        for (int i = 0; i < players; i++)
        {
            joystickInputs[i] = new Vector2(Mathf.RoundToInt(Input.GetAxisRaw("Horizontal" + (i + 1).ToString())), Mathf.RoundToInt(Input.GetAxisRaw("Vertical" + (i + 1).ToString())));
            //Debug.Log(joystickInputs[i]);
        }

        if (!end)
        {
            //Get and show new direction
            //if (countdown && curTime <= 0)
            //{
            //    countdown = false;
            //    GetCommand();
            //}

            //Round is over, check for any player that hasnt input yet and take away points
            if (curTime <= 0 && curReflexTime < 0)
            {
                //commandImage.sprite = blankSprite;
                //commandImage2.sprite = blankSprite;

                //Deduct points from no inputs from players
                for (int i = 0; i < players; i++)
                {
                    if (!checkedAnswer[i]) IncorrectAnswer(i);

                    checkedAnswer[i] = false;
                }

                //Start new round
                countdown = true;
                curTime = Random.Range(roundTimeSmall, roundTimeBig);
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

        //During the round
        if (curTime <= 0 && curReflexTime > 0)
        {
            curReflexTime -= Time.deltaTime;

            for (int i = 0; i < players; i++)
            {
                //if (joystickInputs[i] != Vector2.zero && checkedAnswer[i] <= 0f)
                if (joystickInputs[i] != Vector2.zero && !checkedAnswer[i])
                {
                    //checkedAnswer[i] = checkTime;

                    //First half is regular inputs, other half is NOT inputs
                    if (!notCommand)
                    {
                        if (joystickInputs[i] == commandDirection)
                        {
                            CorrectAnswer(i);
                        }
                        else
                        {
                            IncorrectAnswer(i);
                        }
                    }
                    else
                    {
                        if (joystickInputs[i] == commandDirection)
                        {
                            IncorrectAnswer(i);
                        }
                        else
                        {
                            CorrectAnswer(i);
                        }
                    }
                }
            }
        }
    }


    void CorrectAnswer(int p)
    {
        checkedAnswer[p] = true;
        GameManager.instance.playerScores[p]++;
        playerPoints[p]++;

        if (playerPoints[p] > 2) hintObj.SetActive(false);
        if (playerPoints[p] >= pointsToEnd) end = true;

    }
    
    void IncorrectAnswer(int p)
    {
        checkedAnswer[p] = true;
        if (GameManager.instance.playerScores[p] > 0) GameManager.instance.playerScores[p]--;
        if (playerPoints[p] > 0) playerPoints[p]--;
    }

    int index;
    bool notCommand;

    public void GetCommand()
    {
        if (fullCommands.Count < 1)
        {
            end = true;
            return;
        }

        curReflexTime = reflexTime;

        index = Random.Range(0, fullCommands.Count);
        //int index = Random.Range(0, commands.Length - 1);

        commandText.text = "Current command: " + fullCommands[index];
        commandText2.text = fullYurpusCommands[index];
        yurpusCommandText.text = fullYurpusCommands[index];
        notCommand = fullCommands[index].Contains("Not");
        commandImage.sprite = fullCommandImages[index];
        commandImage2.sprite = fullCommandImages[index];

        if (fullCommands[index].Contains("Up"))
        {
            commandDirection = Vector2.up;
        }
        if (fullCommands[index].Contains("Down"))
        {
            commandDirection = Vector2.down;
        }
        if (fullCommands[index].Contains("Left"))
        {
            commandDirection = Vector2.left;
        }
        if (fullCommands[index].Contains("Right"))
        {
            commandDirection = Vector2.right;
        }

        //Remove both commands from the list
        fullCommands.RemoveAt(index);
        fullYurpusCommands.RemoveAt(index);

        //float ran = Random.value;
        //if (ran > 0.5f)
        //{
        //    commandText.text = "Current command: Not " + commands[index];
        //    commandText2.text = "Zerp " + yurpusCommands[index];
        //    yurpusCommandText.text = "Zerp " + yurpusCommands[index];
        //}
        //else
        //{
        //    commandText.text = "Current command: " + commands[index];
        //    commandText2.text = yurpusCommands[index];
        //    yurpusCommandText.text = yurpusCommands[index];
        //}
    }
}
