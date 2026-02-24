using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubordinationTrainingController : MonoBehaviour
{
    public string[] principles = { 
        "When the timer's auditory signal has concluded, you are to perform the absolute logical antithesis of the following directive: " +
        "it is imperative that you avoid failing to abstain from initiating physical contact with Yurpus.",
        
        "Be it enacted and understood that the Operator, hereafter referred to as 'the Participant,' is contractually obligated to " +
        "perform a series of actions within the prescribed temporal period, said period commencing immediately and expiring upon " +
        "the issuance of the terminal auditory notification by the timing mechanism. The Participant's primary obligation during this period " +
        "is to engage with the signal buzzer apparatus. This engagement shall consist of a sequence of distinct, non-simultaneous manual " +
        "depressions, the total sum of which must be no less than and no greater than the number of appendages on a standard human hand. " +
        "Failure to complete the full quintuple sequence prior to the temporal expiration event will constitute a material breach of protocol.",

        "Say Yurpus." };

    public string[] hints = { "Are you smarter than a biggusglorbo?", "The fractal trees on Yagrina 5 are purple.", "You are dumb, but perfect for being pets." };

    public List<string> newPrinciples;
    public List<string> newHints;

    public TMP_Text commandText;
    public TMP_Text yurpusCommandText;

    public float minigameTime = 30f;
    float curTime;
    public TMP_Text timeRemaining;
    public TMP_Text timeRemaining2;

    public bool multiRound;
    public int curRound = 0;
    public int totalRounds = 5;
    public GameObject roundButton;

    public bool[] buzzerInputs;
    int players;

    public int pointsToWin = 3;
    public bool end = false;

    private void Awake()
    {
        players = GameManager.instance.players;

        foreach (PlayerScoreButton ps in FindObjectOfType<MenuFunctions>().playerScores)
        {
            ps.maxScore = pointsToWin;
        }
    }

    private void Start()
    {
        multiRound = true;
    }

    public void StartRound()
    {
        //Get new command
        GetInstruction();
        //roundButton.SetActive(false);
    }

    private void Update()
    {
        if (curTime > 0) curTime -= Time.deltaTime;

        //Show time remaining
        int seconds = ((int)curTime % 60);
        int minutes = ((int)curTime / 60);
        timeRemaining.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timeRemaining2.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        //Check for player inputs in here
        for (int i = 0; i < players; i++)
        {
            buzzerInputs[i] = Input.GetButtonDown("Buzz" + (i + 1).ToString());

            if (buzzerInputs[i])
            {
                //Play buzz sound
                GameManager.instance.PlaySound(GameManager.instance.buzzSnd, 0.6f, true);
            }

            if (GameManager.instance.playerScores[i] >= pointsToWin && !end)
            {
                end = true;
                GameManager.instance.playerFunctions.playerScoresText[i].Winner();
                GameManager.instance.menuFunctions.playerScores[i].Winner();
                //GameManager.instance.ShowEndgame();
            }
        }
    }

    public void GetInstruction()
    {
        if (curRound < totalRounds - 1)
        {
            curRound++;
            curTime = minigameTime;

            int index = Random.Range(0, newPrinciples.Count);

            commandText.text = newPrinciples[index];
            yurpusCommandText.text = "Hint: " + newHints[index];

            //gameObject.SetActive(false);
        }
        else { 
            roundButton.SetActive(false); 
        }
    }
}
