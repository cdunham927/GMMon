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
    public TMP_Text yurpusCommandText;

    public GameObject playerHudText;

    private void Start()
    {
        curPlace = GameManager.instance.players;
    }

    public void NextRound()
    {
        round++;
        if (round == 3 && hintObj != null) hintObj.SetActive(false);
        if (eliminatedPlayer > 0) curPlace-= eliminatedPlayer;
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
            yurpusCommandText.text = "Zerp " + yurpusCommands[index];
        }
        else
        {
            commandText.text = "Current command: " + commands[index];
            yurpusCommandText.text = yurpusCommands[index];
        }
    }
}
