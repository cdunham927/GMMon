using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class SymbolicReconfigurationController : MonoBehaviour
{
    public int curRound = 0;
    public string[] words1 = { "BOT", "ION", "LAB", "RAY", "POD" };
    public string[] words2= { "BEAM", "CORE", "DATA", "MOON", "STAR" };
    public string[] words3 = { "ALIEN", "COMET", "LASER", "LUNAR", "PROBE" };
    public string[] words4 = { "GALAXY", "METEOR", "ORBIT", "PLASMA", "SYSTEM" };
    public string[] words5 = { "GRAVITY", "QUANTUM", "REACTOR", "SCANNER", "STATION" };
    public string[] words6 = { "ASTEROID", "NEBULA", "QUADRANT", "TERRAFORM", "UNIVERSE" };
    public string[] words7 = { "DIMENSION", "DISCOVERY", "HYPERDRIVE", "ORGANISM", "TELEPORT" };
    public string[] words8 = { "CONSTELLATION", "EXOPLANET", "GENERATION", "WORMHOLE", "TECHNOLOGY" };
    public string[] words9 = { "COLONIZATION", "EXPLORATION", "INTERSTELLAR", "SINGULARITY", "XENOBIOLOGY" };
    public string[] words10 = { "ASTROPHYSICS", "CYBERNETICS", "EXTRATERRESTRIAL", "INTERDIMENSIONAL" };
    public string unshuffledWord;
    public string shuffledWord;

    public TMP_Text commandText;
    public TMP_Text yurpusCommandText;


    public void StartRound()
    {
        if (curRound < 9) curRound++;
        //Get new command
        GetWord();
    }

    public void ShuffleWord()
    {
        //Use switch case to get a harder word each round
        switch(curRound)
        {
            case 0:
                unshuffledWord = words1[Random.Range(0, words1.Length)];
                break;
            case 1:
                unshuffledWord = words2[Random.Range(0, words2.Length)];
                break;
            case 2:
                unshuffledWord = words3[Random.Range(0, words3.Length)];
                break;
            case 3:
                unshuffledWord = words4[Random.Range(0, words4.Length)];
                break;
            case 4:
                unshuffledWord = words5[Random.Range(0, words5.Length)];
                break;
            case 5:
                unshuffledWord = words6[Random.Range(0, words6.Length)];
                break;
            case 6:
                unshuffledWord = words7[Random.Range(0, words7.Length)];
                break;
            case 7:
                unshuffledWord = words8[Random.Range(0, words8.Length)];
                break;
            case 8:
                unshuffledWord = words9[Random.Range(0, words9.Length)];
                break;
            case 9:
                unshuffledWord = words10[Random.Range(0, words10.Length)];
                break;
        }
        shuffledWord = new string(unshuffledWord.OrderBy(x => Random.value).ToArray());

        if (shuffledWord == unshuffledWord)
        {
            shuffledWord = new string(unshuffledWord.OrderBy(x => Random.value).ToArray());
        }
    }

    public void GetWord()
    {
        ShuffleWord();
        commandText.text = shuffledWord;
        yurpusCommandText.text = "Word: " + unshuffledWord;
        //gameObject.SetActive(false);
    }
}
