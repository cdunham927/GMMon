using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EmotionalAuditController : MonoBehaviour
{
    public string[] prompt = { "You have just found out your zipple has flammamed the Nerbus, as it were.",
        "You are not sure if your Zoom meeting audio is muted, you just farted quite loudly, and you are too scared to check if you were muted."
    };

    public List<string> prompts;

    public TMP_Text selectedPlayerText;
    public TMP_Text commandText;
    public TMP_Text yurpusCommandText;

    public float minigameTime = 30f;
    float curTime;
    public TMP_Text timeRemaining;
    public TMP_Text timeRemaining2;

    bool chosePlayer = false;
    public int selectedPlayer;

    public bool multiRound;
    public GameObject roundButton;

    public Image dirImg;
    public Sprite lArrow;
    public Sprite rArrow;

    public bool[] buzzerInputs;
    int players;

    public int curRound = 0;
    public int totalRounds;

    private void Awake()
    {
        players = GameManager.instance.players;
    }

    private void Start()
    {
        multiRound = true;
    }

    public void StartRound()
    {
        chosePlayer = false;
        //Get new command
        GetInstruction();
    }

    private void Update()
    {
        if (curTime > 0) curTime -= Time.deltaTime;

        //Show time remaining
        int seconds = ((int)curTime % 60);
        int minutes = ((int)curTime / 60);
        timeRemaining.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timeRemaining2.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (curTime <= 0 && !chosePlayer)
        {
            chosePlayer = true;
            yurpusCommandText.gameObject.SetActive(false);
            GetRandomPlayer();
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
    public void GetRandomPlayer()
    {
        selectedPlayer = Random.Range(0, GameManager.instance.players);

        dirImg.sprite = (Random.value > 0.5f) ? lArrow : rArrow;
    }

    public void GetInstruction()
    {
        if (curRound < totalRounds - 1)
        {
            curRound++;

            GetRandomPlayer();
            selectedPlayerText.text = "Player: " + (selectedPlayer + 1).ToString();

            curTime = minigameTime;

            int index = Random.Range(0, prompts.Count);

            commandText.text = prompts[index];
            yurpusCommandText.text = prompts[index];
            yurpusCommandText.gameObject.SetActive(true);

            prompts.RemoveAt(index);

            //gameObject.SetActive(false);
        }
        else
        {
            roundButton.SetActive(false);
        }
    }
}
