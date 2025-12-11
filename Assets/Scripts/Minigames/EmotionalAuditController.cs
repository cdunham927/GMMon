using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EmotionalAuditController : MonoBehaviour
{
    public string[] prompt = { "You have just found out your zipple has flammamed the Nerbus, as it were.",
        "You are not sure if your Zoom meeting audio is muted, you just farted quite loudly, and you are too scared to check if you were muted."
    };

    public TMP_Text commandText;
    public TMP_Text yurpusCommandText;

    public float minigameTime = 30f;
    float curTime;
    public TMP_Text timeRemaining;
    public TMP_Text timeRemaining2;

    bool chosePlayer = false;
    public int selectedPlayer;

    public bool multiRound;

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
    }
    public void GetRandomPlayer()
    {
        selectedPlayer = Random.Range(0, GameManager.instance.players);
    }

    public void GetInstruction()
    {
        curTime = minigameTime;

        int index = Random.Range(0, prompt.Length);

        commandText.text = prompt[index];
        yurpusCommandText.text = prompt[index];
        yurpusCommandText.gameObject.SetActive(true);

        //gameObject.SetActive(false);
    }
}
