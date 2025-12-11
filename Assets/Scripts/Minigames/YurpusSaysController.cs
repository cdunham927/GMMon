using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class YurpusSaysController : IPlayerOut
{
    public string[] commands = { "Up", "Down", "Left", "Right", "Not" };
    public string[] yurpusCommands = { "Glorp", "Norf", "Mim", "Farn", "Zerp" };

    public GameObject hintObj;
    public TMP_Text commandText;
    public TMP_Text commandText2;
    public TMP_Text yurpusCommandText;

    public GameObject playerHudText;

    public bool multiRound;

    private void Start()
    {
        curPlace = GameManager.instance.players;
        multiRound = true;
    }

    public void NextRound()
    {
        //Need to keep a list of all playerscorebuttons currently clicked
        //Update all their places to be plus 1 of current included players(curPlace)
            //IE instead of 3 8th place players they become 3 5th place ones
        //Clear the list after we update them
        //Also have a bool in the playerscorebutton that prevents us from unclicking after we have gone to the next round

        round++;
        if (round == 3 && hintObj != null) hintObj.SetActive(false);
        if (eliminatedPlayer > 0) curPlace -= eliminatedPlayer;
        eliminatedPlayer = 0;

        //Get new command
        GetCommand();

        if (curPlace <= 1)
        {
            playerHudText.SetActive(false);
            GameManager.instance.ShowEndgame();
        }
    }

    public void GetCommand()
    {
        int index = Random.Range(0, commands.Length - 1);

        float ran = Random.value;
        if (ran > 0.5f)
        {
            commandText.text = "Current command: Not " + commands[index];
            commandText2.text = "Zerp " + yurpusCommands[index];
            yurpusCommandText.text = "Zerp " + yurpusCommands[index];
        }
        else
        {
            commandText.text = "Current command: " + commands[index];
            commandText2.text = yurpusCommands[index];
            yurpusCommandText.text = yurpusCommands[index];
        }
    }
}
