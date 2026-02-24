using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfiniteQueriesController : MonoBehaviour
{
    public string[] objects = { "Rubber Duck", "Paperclip", "'Stop' Sign", "Key", "Birthday Candle", "Toothbrush", "Ice Cube" };
    public string[] hintsGood1 = { "Rubber Duck", "Paperclip", "'Stop' Sign", "Key", "Birthday Candle", "Toothbrush", "Ice Cube" };
    public string[] hintsGood2 = { "Rubber Duck", "Paperclip", "'Stop' Sign", "Key", "Birthday Candle", "Toothbrush", "Ice Cube" };
    public string[] hintsBad = { "Rubber Duck", "Paperclip", "'Stop' Sign", "Key", "Birthday Candle", "Toothbrush", "Ice Cube" };
    bool hintShown1, hintShown2, hintBadShown = false;
    public string currentObject;
    public TMP_Text commandText;
    public TMP_Text hintText;

    float buzzCools;
    public float buzzInCooldown;
    public TMP_Text buzzInText;

    public bool multiRound;
    public GameObject roundButton;

    public bool[] buzzerInputs;
    int players;
    public bool end = false;
    public int pointsToWin = 1;

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
        roundButton.SetActive(false);
    }

    public void ShowHint()
    {
        if (index == -1) return;
        if (hintShown1 && hintShown2 && hintBadShown) return;

        if (hintShown1 && hintShown2)
        {
            AddHint(hintsBad[index]);
            hintBadShown = true;
            return;
        }

        if (hintShown2 && hintBadShown)
        {
            AddHint(hintsGood1[index]);
            hintShown1 = true;
            return;
        }

        if (hintShown1 && hintBadShown)
        {
            AddHint(hintsGood2[index]);
            hintShown2 = true;
            return;
        }

        float r = Random.value;
        if (r < 0.33f && !hintBadShown)
        {
            AddHint(hintsBad[index]);
            hintBadShown = true;
        }
        else if (r < 0.66f && !hintShown1)
        {
            AddHint(hintsGood1[index]);
            hintShown1 = true;
        }
        else if (!hintShown2)
        {
            AddHint(hintsGood2[index]);
            hintShown2 = true;
        }
    }

    void AddHint(string s)
    {
        hintText.text += s + "\n";
    }

    private void Update()
    {
        //Check for player inputs in here
        for (int i = 0; i < players; i++)
        {
            buzzerInputs[i] = Input.GetButtonDown("Buzz" + (i + 1).ToString());

            if (buzzerInputs[i] && buzzCools <= 0)
            {
                //Play buzz sound
                GameManager.instance.PlaySound(GameManager.instance.buzzSnd, 0.6f, true);
                buzzCools = buzzInCooldown;
                buzzInText.text = (i + 1).ToString() + " BUZZED IN!";
            }

            if (GameManager.instance.playerScores[i] >= pointsToWin && !end)
            {
                end = true;
                GameManager.instance.playerFunctions.playerScoresText[i].Winner();
                GameManager.instance.menuFunctions.playerScores[i].Winner();
                //GameManager.instance.ShowEndgame();
            }
        }

        if (buzzCools <= 0) buzzInText.text = "";
        if (buzzCools > 0) buzzCools -= Time.deltaTime;
    }

    int index = -1;

    public void GetInstruction()
    {
        index = Random.Range(0, objects.Length - 1);
        currentObject = objects[index];

        commandText.text = currentObject;

        //gameObject.SetActive(false);
    }
}
