using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class YurpusSaysController : MonoBehaviour
{
    public string[] commands = { "Up", "Down", "Left", "Right", "Not" };
    public string[] yurpusCommands = { "Glorp", "Norf", "Mim", "Farn", "Zerp" };

    public int round;
    public GameObject hintObj;
    public TMP_Text commandText;
    public TMP_Text yurpusCommandText;

    public void NextRound()
    {
        round++;
        if (round == 3 && hintObj != null) hintObj.SetActive(false);

        //Get new command
        GetCommand();
    }

    public void GetCommand()
    {
        int index = Random.Range(0, commands.Length - 1);

        commandText.text = commands[index];
        commandText.text = yurpusCommands[index];
    }
}
