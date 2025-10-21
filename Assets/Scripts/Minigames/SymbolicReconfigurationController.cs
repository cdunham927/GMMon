using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class SymbolicReconfigurationController : MonoBehaviour
{
    public int curRound = 0;
    public string[] words1 = { "BOT", "ION", "LAB", "RAY", "POD" };
    public string unshuffledWord;
    public string shuffledWord;

    public TMP_Text commandText;
    public TMP_Text yurpusCommandText;


    public void StartRound()
    {
        curRound++;
        //Get new command
        GetWord();
    }

    public void ShuffleWord()
    {

        //Use switch case to get a harder word each round
        switch(curRound)
        {
            case 0:

                break;
            case 1:

                break;
        }
        unshuffledWord = words1[Random.Range(0, words1.Length)];
        shuffledWord = new string(unshuffledWord.OrderBy(x => Random.value).ToArray());
    }

    public void GetWord()
    {
        commandText.text = shuffledWord;
        yurpusCommandText.text = "Word: " + unshuffledWord;

        //gameObject.SetActive(false);
    }
}
