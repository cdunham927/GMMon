using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class SymbolicReconfigurationController : MonoBehaviour
{
    public int curRound = 0;
    public string[] words1 = { "BOT", "ION", "LAB", "RAY", "POD" };
    public string[] words2 = { "BEAM", "CORE", "DATA", "MOON", "STAR" };
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

    public bool multiRound;

    public bool[] buzzerInputs;
    int players;

    public int roundsToChange = 3;
    int curRoundChange = 0;

    bool started = false;

    public float initialScrambleTime;
    public TMP_Text unscrambleHintText;
    public string unshuffledHint;
    public int unscramblePosition = 0;
    public float timeToUnscramble;
    float curUnscrambleTime;

    bool end = false;
    public int pointsToWin = 5;

    private void Awake()
    {
        players = GameManager.instance.players;

        ShuffleWord(words1);
        ShuffleWord(words2);
        ShuffleWord(words3);
        ShuffleWord(words4);
        ShuffleWord(words5);
        ShuffleWord(words6);
        ShuffleWord(words7);
        ShuffleWord(words8);
        ShuffleWord(words9);
        ShuffleWord(words10);
    }

    string[] RemoveWord(string[] arr, int index)
    {
        for (int a = index; a < arr.Length - 1; a++)
        {
            // moving elements downwards, to fill the gap at [index]
            arr[a] = arr[a + 1];
        }
        // finally, let's decrement Array's size by one
        System.Array.Resize(ref arr, arr.Length - 1);
        return arr;
    }

    void ShuffleWord(string[] arr)
    {
        //Shuffle question and answer arrays once before the game starts
        int count = arr.Length;
        for (int i = count - 1; i > 0; --i)
        {
            int randIndex = Random.Range(0, i);

            string temp = arr[i];
            arr[i] = arr[randIndex];
            arr[randIndex] = temp;
        }
    }

    private void Start()
    {
        multiRound = true;
    }

    private void Update()
    {
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
                GameManager.instance.ShowEndgame();
            }
        }

        if (started)
        {
            if (curUnscrambleTime > 0) curUnscrambleTime -= Time.deltaTime;
            if (curUnscrambleTime <= 0 && unscramblePosition < unshuffledWord.Length)
            {
                unshuffledHint += unshuffledWord[unscramblePosition];
                curUnscrambleTime = timeToUnscramble;
                unscramblePosition++;
            }
        }
        unscrambleHintText.text = unshuffledHint;
    }

    public void StartRound()
    {
        unshuffledHint = "";
        curUnscrambleTime = initialScrambleTime;
        started = true;
        unscramblePosition = 0;

        //Get new command
        GetWord();

        if (curRound < 9 && curRoundChange >= roundsToChange)
        {
            curRoundChange = 0;
            curRound++;
        }
    }

    public void ShuffleWord()
    {
        //Use switch case to get a harder word each round
        int ind;
        switch (curRound)
        {
            case 0:
                ind = Random.Range(0, words1.Length);
                unshuffledWord = words1[ind];
                words1 = RemoveWord(words1, ind);
                break;
            case 1:
                ind = Random.Range(0, words2.Length);
                unshuffledWord = words2[ind];
                words2 = RemoveWord(words2, ind);
                break;
            case 2:
                ind = Random.Range(0, words3.Length);
                unshuffledWord = words3[ind];
                words3 = RemoveWord(words3, ind);
                break;
            case 3:
                ind = Random.Range(0, words4.Length);
                unshuffledWord = words4[ind];
                words4 = RemoveWord(words4, ind);
                break;
            case 4:
                ind = Random.Range(0, words5.Length);
                unshuffledWord = words5[ind];
                words5 = RemoveWord(words5, ind);
                break;
            case 5:
                ind = Random.Range(0, words6.Length);
                unshuffledWord = words6[ind];
                words6 = RemoveWord(words6, ind);
                break;
            case 6:
                ind = Random.Range(0, words7.Length);
                unshuffledWord = words7[ind];
                words7 = RemoveWord(words7, ind);
                break;
            case 7:
                ind = Random.Range(0, words8.Length);
                unshuffledWord = words8[ind];
                words8 = RemoveWord(words8, ind);
                break;
            case 8:
                ind = Random.Range(0, words9.Length);
                unshuffledWord = words9[ind];
                words9 = RemoveWord(words9, ind);
                break;
            case 9:
                if (words10.Length > 0)
                {
                    ind = Random.Range(0, words10.Length);
                    unshuffledWord = words10[ind];
                    words10 = RemoveWord(words10, ind);
                }
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
        curRoundChange++;
        ShuffleWord();
        commandText.text = shuffledWord;
        yurpusCommandText.text = "Word: " + unshuffledWord;
        //gameObject.SetActive(false);
    }
}
